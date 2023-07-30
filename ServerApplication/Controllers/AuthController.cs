using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServerApplication.DTO;
using ServerApplication.Models;
using ServerApplication.Services;

namespace ServerApplication.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IServiceManager _serviceManager;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        IServiceManager serviceManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _serviceManager = serviceManager;
        _tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,

            ValidAudience = _configuration["JWT:ValidAudience"],
            ValidIssuer = _configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]))
        };
    }
    
    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UnregisteredUserDto dto)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(dto.Login)
                       ?? throw new AuthenticationException();
            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new AuthenticationException("Used wrong password. Check it and try again");

            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName ?? throw new ArgumentNullException($"Cannot get {nameof(user.UserName)}")),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            claims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

            var accessToken = GenerateAccessToken(claims);
            var refreshToken = GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                refreshToken,
                expiration = accessToken.ValidTo
            });
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UnregisteredUserDto dto)
    {
        try
        {
            await _serviceManager.UserService.GetAll();
            if (await _userManager.FindByNameAsync(dto.Login) is not null)
                throw new AuthenticationException("User already exists");

            var user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = dto.Login
            };
            
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new AuthenticationException("User creation failed! Please check user details and try again");

            await InitRoles();
            
            await _userManager.AddToRoleAsync(user, Roles.User);
            
            var newUser = await _serviceManager.UserService.Add(new User { Login = dto.Login });

            return Ok(new
            {
                id = newUser.Id.ToString(),
                login = newUser.Login
            });
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] UnregisteredUserDto dto)
    {
        try
        {
            if (await _userManager.FindByNameAsync(dto.Login) is not null)
                throw new AuthenticationException("User already exists");

            var user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = dto.Login
            };
        
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                throw new AuthenticationException("User creation failed! Please check user details and try again");
        
            await InitRoles();

            await _userManager.AddToRoleAsync(user, Roles.Admin); 
            await _userManager.AddToRoleAsync(user, Roles.User);

            var newUser = await _serviceManager.UserService.Add(new User { Login = dto.Login });

            return Ok(new
            {
                id = newUser.Id.ToString(),
                login = newUser.Login
            });
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.AccessToken) || string.IsNullOrWhiteSpace(dto.AccessToken))
                throw new ArgumentException("Tokens cannot be null");

            var accessToken = dto.AccessToken;
            var refreshToken = dto.RefreshToken;

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, _tokenValidationParameters, out var validatedToken);

            // Now we need to check if the token has a valid security algorithm
            if(validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                if(result == false) {
                    return null;
                }
            }

            if (principal is null)
                throw new ArgumentException("Invalid access token or refresh token");

            string username = principal.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new ArgumentException("Invalid access token or refresh token");

            var newAccessToken = GenerateAccessToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
            
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    [Route("revoke/{login}")]
    public async Task<IActionResult> Revoke(string login)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user is null) 
                throw new ArgumentException("Invalid user login");

            await RevokeUserToken(user);
        
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    [Route("revoke-all")]
    public async Task<IActionResult> RevokeAll()
    {
        var users = _userManager.Users.ToList();
        foreach (var user in users)
            await RevokeUserToken(user);

        return Ok();
    }
    
    private async Task InitRoles()
    {
        if (!await _roleManager.RoleExistsAsync(Roles.Admin))
            await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
        if (!await _roleManager.RoleExistsAsync(Roles.User))
            await _roleManager.CreateAsync(new IdentityRole(Roles.User));
    }
    
    private JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    // private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    // {
    //     return principal;
    // }

    private async Task RevokeUserToken(ApplicationUser user)
    {
        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = DateTime.Now;
        await _userManager.UpdateAsync(user);
    }
}