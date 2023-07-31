using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApplication.DTO;
using ServerApplication.Mappers;
using ServerApplication.Models;
using ServerApplication.Services;

namespace ServerApplication.Controllers;

[ApiController]
// [Authorize(Roles = Roles.Admin)]
[AllowAnonymous]
[Route("api/place")]
public class PlaceController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public PlaceController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] UnregisteredPlaceDto dto)
    {
        try
        {
            var place = await _serviceManager.PlaceService.Add(dto.AsEntity());

            return Ok(place.AsDto());
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] PlaceDto dto)
    {
        try
        {
            var place = await _serviceManager.PlaceService.Update(dto.AsEntity());

            return Ok(place.AsDto());
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPost]
    [Route("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var place = await _serviceManager.PlaceService.DeleteById(id);

            return Ok(place.AsDto());
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var place = await _serviceManager.PlaceService.Get(id);

            return Ok(place.AsDto());
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var places = await _serviceManager.PlaceService.GetAll();

            return Ok(places.Select(x => x.AsDto()));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("all/{categoryId}")]
    public async Task<IActionResult> GetAllByCategory(Guid categoryId)
    {
        try
        {
            var places = await _serviceManager.PlaceService.GetAllByCategory(categoryId);

            return Ok(places.Select(x => x.AsDto()));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}