using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerApplication.DTO;
using ServerApplication.Mappers;
using ServerApplication.Models;
using ServerApplication.Services;

namespace ServerApplication.Controllers;

[ApiController]
[Authorize]
[Route("api/settings")]
public class SettingsController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IServiceManager _serviceManager;

    public SettingsController(
        UserManager<ApplicationUser> userManager,
        IServiceManager serviceManager)
    {
        _userManager = userManager;
        _serviceManager = serviceManager;
    }

    [HttpPost]
    [Route("check-around")]
    public async Task<IActionResult> SetCheckAround([FromBody] UserCheckAroundDto dto)
    {
        try
        {
            await _serviceManager.SettingsService.SetCheckAround(dto.UserId, dto.CheckAround);

            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost]
    [Route("categories")]
    public async Task<IActionResult> SetInterestingCategories([FromBody] UserCategoriesIdsDto idsDto)
    {
        try
        {
            await _serviceManager.SettingsService.SetInterestingCategories(idsDto.UserId, idsDto.CategoriesIds);

            return Ok(idsDto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost]
    [Route("add-favorite-place")]
    public async Task<IActionResult> AddFavoritePlace([FromBody] UserFavoritePlaceIdDto idDto)
    {
        try
        {
            await _serviceManager.SettingsService.AddFavoritePlace(idDto.UserId, idDto.PlaceId);

            return Ok(idDto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost]
    [Route("remove-favorite-place")]
    public async Task<IActionResult> RemoveFavoritePlace([FromBody] UserFavoritePlaceIdDto idDto)
    {
        try
        {
            await _serviceManager.SettingsService.RemoveFavoritePlace(idDto.UserId, idDto.PlaceId);

            return Ok(idDto);
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

            return Ok(new UserCheckAroundDto(userId, checkAround));
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

            return Ok(new UserCategoriesDto(userId, categories.Select(x => x.AsDto())));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet]
    [Route("favorite-places/{userId}")]
    public async Task<IActionResult> GetFavoritePlaces(Guid userId)
    {
        try
        {
            var places = await _serviceManager.SettingsService.GetFavoritePlaces(userId);

            return Ok(new UserFavoritePlacesDto(userId, places.Select(x => x.AsDto())));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> Get(Guid userId)
    {
        try
        {
            var settings = await _serviceManager.SettingsService.GetUserSettings(userId);

            return Ok(settings.AsDto());
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}