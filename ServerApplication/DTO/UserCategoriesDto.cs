namespace ServerApplication.DTO;

public record UserCategoriesDto(Guid userId, HashSet<Guid> categoriesIds);