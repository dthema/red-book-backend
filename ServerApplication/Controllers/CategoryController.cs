using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApplication.DTO;
using ServerApplication.Mappers;
using ServerApplication.Models;
using ServerApplication.Services;

namespace ServerApplication.Controllers;

[ApiController]
[Authorize(Roles = Roles.Admin)]
[Route("api/category")]
public class CategoryController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public CategoryController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] UnregisteredCategoryDto dto)
    {
        try
        {
            var category = await _serviceManager.CategoryService.Add(dto.AsEntity());

            return Ok(category.AsDto());
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] CategoryDto dto)
    {
        try
        {
            var category = await _serviceManager.CategoryService.Update(dto.AsEntity());

            return Ok(category.AsDto());
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
            var category = await _serviceManager.CategoryService.DeleteById(id);

            return Ok(category.AsDto());
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
            var category = await _serviceManager.CategoryService.Get(id);

            return Ok(category.AsDto());
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
            var categories = await _serviceManager.CategoryService.GetAll();

            return Ok(categories.Select(x => x.AsDto()));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}