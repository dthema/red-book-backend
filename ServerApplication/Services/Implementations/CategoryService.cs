using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ServerApplication.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ApplicationContext _appCtx;

    public CategoryService(ApplicationContext appCtx)
    {
        _appCtx = appCtx;
    }

    public async Task<Category> Add(Category entity)
    {
        var category = await _appCtx.Categories.AddAsync(entity);
        await _appCtx.SaveChangesAsync();
        return category.Entity;

    }

    public async Task<Category> Update(Category entity)
    {
        var category = _appCtx.Categories.Update(entity).Entity;
        await _appCtx.SaveChangesAsync();
        return category;
    }

    public async Task<bool> Delete(Category entity)
    {
        _appCtx.Categories.Remove(entity);
        await _appCtx.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteById(Guid id)
    {
        var category = new Category { Id = id };
        _appCtx.Categories.Attach(category);
        _appCtx.Categories.Remove(category);
        await _appCtx.SaveChangesAsync();
        return true;
    }
    
    public async Task<Category> Get(Guid id)
    {
        return await Find(id) ?? throw new InvalidOperationException();
    }

    public Task<Category?> Find(Guid id)
    {
        return _appCtx.Categories.FirstOrDefaultAsync(category => category.Id.Equals(id));
    }

    public Task<List<Category>> GetAll()
    {
        return _appCtx.Categories.ToListAsync();
    }

    public Task<List<Category>> GetAllByUser(Guid userId)
    {
        return _appCtx.Categories
            .Include(x => x.CategorySettings)
            .ThenInclude(x => x.AssociatedSettings)
            .Where(x => x.CategorySettings.Any(c => c.AssociatedSettings.UserId.Equals(userId)))
            .ToListAsync();
    }
}