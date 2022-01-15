using DerivcoKitchenWebAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DerivcoKitchenWebAPI.Data.DALClasses
{
    public class MenuCategoryDAL
    {
        public async Task<MenuCategory> GetMenuCategoryByName(DerivcoKitchenContext dbContext, string menuCategoryName)
        {
            return await (from menuCategory in dbContext.MenuCategories.Cast<MenuCategory>()
                          where menuCategory.Name == menuCategoryName
                          select menuCategory).
                          FirstOrDefaultAsync();
        }

        public async Task<List<MenuCategory>> GetMenuCategories(DerivcoKitchenContext dbContext)
        {
            return await (from menuCategory in dbContext.MenuCategories.Cast<MenuCategory>()
                          select menuCategory).
                          ToListAsync();
        }
    }
}
