using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApplication.DTO;
using ServerApplication.Mappers;
using ServerApplication.Services;

namespace ServerApplication.Controllers;

[ApiController]
[Authorize]
[Route("api/settings")]
public class SettingsController : AIdentifyingController
{
    private readonly IServiceManager _serviceManager;

    public SettingsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    [Route("check-around")]
    public async Task<IActionResult> SetCheckAround([FromBody] UserCheckAroundDto dto)
    {
        try
        {
            IdentifyUserId(dto.UserId);

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
    public async Task<IActionResult> SetInterestingCategories([FromBody] UserCategoriesIdsDto dto)
    {
        try
        {
            IdentifyUserId(dto.UserId);

            await _serviceManager.SettingsService.SetInterestingCategories(dto.UserId, dto.CategoriesIds);

            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost]
    [Route("add-favorite-place")]
    public async Task<IActionResult> AddFavoritePlace([FromBody] UserFavoritePlaceIdDto dto)
    {
        try
        {
            IdentifyUserId(dto.UserId);

            await _serviceManager.SettingsService.AddFavoritePlace(dto.UserId, dto.PlaceId);

            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost]
    [Route("remove-favorite-place")]
    public async Task<IActionResult> RemoveFavoritePlace([FromBody] UserFavoritePlaceIdDto dto)
    {
        try
        {
            IdentifyUserId(dto.UserId);

            await _serviceManager.SettingsService.RemoveFavoritePlace(dto.UserId, dto.PlaceId);

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
            IdentifyUserId(userId);

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
            IdentifyUserId(userId);

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
            IdentifyUserId(userId);
            
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
            IdentifyUserId(userId);
            
            var settings = await _serviceManager.SettingsService.GetUserSettings(userId);

            return Ok(settings.AsDto());
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}