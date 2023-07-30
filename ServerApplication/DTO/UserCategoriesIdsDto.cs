namespace ServerApplication.DTO;

public record UserCategoriesIdsDto(Guid UserId, HashSet<Guid> CategoriesIds);