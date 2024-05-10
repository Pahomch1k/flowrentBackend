using AirbnbDiploma.Core.Entities;

namespace AirbnbDiploma.DAL.Repositories.WhishlistRepository;

public interface IWhishlistRepository
{
    void AddCategory(WhishlistCategory category);

    void AddItem(WhishlistItem item);

    Task<IEnumerable<WhishlistItem>> GetItemsAsync(int categoryId);

    Task<IEnumerable<WhishlistCategory>> GetCategoriesAsync(Guid userId);

    Task RemoveCategoryByIdAsync(int id);

    Task RemoveItemByIdAsync(int id);
}
