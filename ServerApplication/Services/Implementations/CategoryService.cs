using Domain;
using Domain.Models;

namespace ServerApplication.Services.Implementations;

public class CategoryService : ACrudService<Category>, ICategoryService
{
    public CategoryService(ApplicationContext appCtx)
        : base(appCtx) { }
}