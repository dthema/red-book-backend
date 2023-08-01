using Domain.Models;
using ServerApplication.DTO;

namespace ServerApplication.Mappers;

public static class CategoryMapper
{
    public static CategoryDto AsDto(this Category category)
        => new CategoryDto(category.Id, category.Name, category.IconFilePath);
    
    public static Category AsEntity(this CategoryDto dto)
        => new Category { Id = dto.Id, Name = dto.Name, IconFilePath = dto.IconFilePath };

    public static Category AsEntity(this UnregisteredCategoryDto dto)
        => new Category { Name = dto.Name, IconFilePath = dto.IconFilePath};
}