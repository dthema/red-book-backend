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
[Route("api/settings")]
public class SettingsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IServiceManager _serviceManager;

    public SettingsController(
        IConfiguration configuration,
        IServiceManager serviceManager)
    {
        _configuration = configuration;
        _serviceManager = serviceManager;
    }
    
    [HttpPost]
    [Route("check-around")]
    public async Task<IActionResult> SetCheckAround([FromBody] UserCheckAroundDto dto)
    {
        try
        {
            await _serviceManager.SettingsService.SetCheckAround(dto.userId, dto.checkAround);
            
            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPost]
    [Route("categories")]
    public async Task<IActionResult> SetInterestingCategories([FromBody] UserCategoriesDto dto)
    {
        try
        {
            await _serviceManager.SettingsService.SetInterestingCategories(dto.userId, dto.categoriesIds);
            
            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpGet]
    [Route("check-around/{userId}")]
    public async Task<IActionResult> GetCheckAround(Guid userId)
    {
        try
        {
            var checkAround = await _serviceManager.SettingsService.GetCheckAround(userId);
            
            return Ok(new
            {
                id = userId,
                checkAround
            });
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpGet]
    [Route("categories/{userId}")]
    public async Task<IActionResult> GetInterestingCategories(Guid userId)
    {
        try
        {
            var categories = await _serviceManager.SettingsService.GetInterestingCategories(userId);
            
            return Ok(new
            {
                id = userId,
                categories
            });
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}