using Domain;
using Domain.Models;

namespace ServerApplication.Services.Implementations;

public class CategoryService : CrudService<Category>, ICategoryService
{
    public CategoryService(ApplicationContext appCtx)
        : base(appCtx) { }
}