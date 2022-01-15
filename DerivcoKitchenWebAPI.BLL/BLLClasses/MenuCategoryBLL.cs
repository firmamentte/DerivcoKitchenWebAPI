using DerivcoKitchenWebAPI.BLL.DataContract;
using DerivcoKitchenWebAPI.Data.DALClasses;
using DerivcoKitchenWebAPI.Data.Entities;
namespace DerivcoKitchenWebAPI.BLL.BLLClasses
{
    public class MenuCategoryBLL
    {
        private readonly MenuCategoryDAL MenuCategoryDAL;

        public MenuCategoryBLL()
        {
            MenuCategoryDAL = new();
        }

        public async Task<List<MenuCategoryResp>> GetMenuCategories()
        {
            using DerivcoKitchenContext _dbContext = new();

            List<MenuCategoryResp> _menuCategoryResps = new();

            foreach (MenuCategory menuCategory in await MenuCategoryDAL.GetMenuCategories(_dbContext))
            {
                _menuCategoryResps.Add(FillMenuCategoryResp(menuCategory));
            }

            return _menuCategoryResps;
        }

        private MenuCategoryResp FillMenuCategoryResp(MenuCategory menuCategory)
        {
            if (menuCategory != null)
            {
                return new MenuCategoryResp()
                {
                    Name = menuCategory.Name,
                    Order = menuCategory.Order
                };
            }
            else
            {
                return null;
            }
        }
    }
}
