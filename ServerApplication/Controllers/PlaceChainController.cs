using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApplication.DTO;
using ServerApplication.Mappers;
using ServerApplication.Models;
using ServerApplication.Services;

namespace ServerApplication.Controllers;

[ApiController]
[Authorize(Roles = Roles.Admin)]
[Route("api/place-chain")]
public class PlaceChainController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public PlaceChainController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] UnregisteredPlacesChainDto dto)
    {
        try
        {
            var placesChain = await _serviceManager.PlacesChainService.Add(dto.AsEntity());

            return Ok(placesChain.AsDto());
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] UpdatedPlacesChainDto dto)
    {
        try
        {
            var placesChain = await _serviceManager.PlacesChainService.Update(dto.AsEntity());

            return Ok(placesChain.AsDto());
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
            var placesChain = await _serviceManager.PlacesChainService.DeleteById(id);

            return Ok(placesChain.AsDto());
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPost]
    [Route("add-place")]
    public async Task<IActionResult> AddPlace([FromBody] PlacesWithChainDto dto)
    {
        try
        {
            await _serviceManager.PlacesChainService.AddPlaceToChain(dto.ChainId, dto.PlaceId, dto.Order);

            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPost]
    [Route("remove-place")]
    public async Task<IActionResult> RemovePlace([FromBody] RemovedPlacesWithChainDto dto)
    {
        try
        {
            await _serviceManager.PlacesChainService.RemovePlaceToChain(dto.ChainId, dto.PlaceId);

            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet]
    [Authorize(Roles = Roles.User)]
    [Route("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var placesChain = await _serviceManager.PlacesChainService.Get(id);

            return Ok(placesChain.AsDto());
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpGet]
    [Authorize(Roles = Roles.User)]
    [Route("all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var placesChains = await _serviceManager.PlacesChainService.GetAll();

            return Ok(placesChains.Select(x => x.AsDto()));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}