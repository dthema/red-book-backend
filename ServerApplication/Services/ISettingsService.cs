using Domain.Models;

namespace ServerApplication.Services;

public interface ISettingsService
{
    Task SetCheckAround(Guid userId, bool checkAround);
    Task SetInterestingCategories(Guid userId, ICollection<Guid> categoriesIds);
    Task<bool> GetCheckAround(Guid userId);
    Task<ICollection<Category>> GetInterestingCategories(Guid userId);
}
