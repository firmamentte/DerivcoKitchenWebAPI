using DerivcoKitchenWebAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DerivcoKitchenWebAPI.Data.DALClasses
{
    public class MenuItemDAL
    {
        public async Task<MenuItem> GetMenuItemByMenuItemId(DerivcoKitchenContext dbContext, Guid menuItemId)
        {
            return await (from menuItem in dbContext.MenuItems.Cast<MenuItem>()
                          where menuItem.MenuItemId == menuItemId
                          select menuItem).
                          FirstOrDefaultAsync();
        }

        public async Task<List<MenuItem>> GetMenuItemsByCriteria(DerivcoKitchenContext dbContext, string menuCategoryName, string name, int skip, int take)
        {
            name ??= string.Empty;

            return await (from menuItem in dbContext.MenuItems.Cast<MenuItem>()
                          join menuCategory in dbContext.MenuCategories.Cast<MenuCategory>()
                          on menuItem.MenuCategoryId equals menuCategory.MenuCategoryId
                          where menuCategory.Name == menuCategoryName &&
                                menuItem.Name.Contains(name) &&
                                menuItem.Visible == true
                          select menuItem).
                          OrderBy(menuItem => menuItem.Order).
                          Skip(skip).
                          Take(take).
                          ToListAsync();
        }
    }
}
