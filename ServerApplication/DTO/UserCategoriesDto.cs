namespace ServerApplication.DTO;

public record UserCategoriesDto(Guid UserId, IEnumerable<CategoryDto> Categories);