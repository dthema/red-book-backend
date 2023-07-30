using Domain.Models;
using ServerApplication.DTO;

namespace ServerApplication.Mappers;

public static class CategoryMapper
{
    public static CategoryDto AsDto(this Category category)
        => new CategoryDto(category.Id, category.Name);
}