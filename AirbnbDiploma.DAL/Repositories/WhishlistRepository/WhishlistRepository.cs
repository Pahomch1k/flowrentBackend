using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AirbnbDiploma.DAL.Repositories.WhishlistRepository;
internal class WhishlistRepository : IWhishlistRepository
{
    private readonly AppDbContext _context;

    protected WhishlistRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddCategory(WhishlistCategory category)
    {
        _context.WhishlistCategories.Add(category);
    }

    public void AddItem(WhishlistItem item)
    {
        _context.WhishlistItems.Add(item);
    }

    public async Task<IEnumerable<WhishlistItem>> GetItemsAsync(int categoryId)
    {
        return await _context.WhishlistItems.Where(item => item.CategoryId == categoryId).ToListAsync();
    }

    public async Task<IEnumerable<WhishlistCategory>> GetCategoriesAsync(int userId)
    {
        return await _context.WhishlistCategories.Where(item => item.UserId == userId).ToListAsync();
    }

    public async Task RemoveCategoryByIdAsync(int id)
    {
        var category = await _context.WhishlistCategories.FindAsync(id);
        ThrowIfNull(id, category);
        var items = await GetItemsAsync(id);
        _context.WhishlistItems.RemoveRange(items);
        _context.WhishlistCategories.Remove(category!);
    }

    public async Task RemoveItemByIdAsync(int id)
    {
        var item = await _context.WhishlistItems.FindAsync(id);
        ThrowIfNull(id, item);
        _context.WhishlistItems.Remove(item!);
    }

    protected virtual void ThrowIfNull(int id, object? entity)
    {
        if (entity is null)
        {
            throw new NotFoundException($"Item with id {id} not found");
        }
    }
}
