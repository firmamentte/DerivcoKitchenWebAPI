using DerivcoKitchenWebAPI.BLL.DataContract;
using DerivcoKitchenWebAPI.Data.DALClasses;
using DerivcoKitchenWebAPI.Data.Entities;

namespace DerivcoKitchenWebAPI.BLL.BLLClasses
{
    public class MenuItemBLL
    {
        private readonly MenuItemDAL MenuItemDAL;
        public MenuItemBLL()
        {
            MenuItemDAL = new();
        }

        public async Task<MenuItemResp> GetMenuItemByMenuItemId(Guid menuItemId)
        {
            using DerivcoKitchenContext _dbContext = new();

            MenuItem _menuItem = await MenuItemDAL.GetMenuItemByMenuItemId(_dbContext, menuItemId);

            return FillMenuItemResp(_menuItem);
        }

        public async Task<MenuItemPaginationResp> GetMenuItemsByCriteria(string menuCategoryName, string name, int skip, int take)
        {
            using DerivcoKitchenContext _dbContext = new();

            List<MenuItemResp> _menuItemResps = new();

            foreach (MenuItem menuItem in await MenuItemDAL.GetMenuItemsByCriteria(_dbContext, menuCategoryName, name, skip, take))
            {
                _menuItemResps.Add(FillMenuItemResp(menuItem));
            }

            return FillMenuItemPaginationResp(_menuItemResps, new PaginationMeta { OrderedBy = "Order Asc", NextSkip = skip + take, Taken = take });
        }

        private MenuItemResp FillMenuItemResp(MenuItem menuItem)
        {
            if (menuItem != null)
            {
                return new MenuItemResp()
                {
                    MenuItemId = menuItem.MenuItemId,
                    PictureFileName = menuItem.PictureFileName,
                    Name = menuItem.Name,
                    Description = menuItem.Description,
                    Order = menuItem.Order,
                    Price = menuItem.Price
                };
            }
            else
            {
                return null;
            }
        }

        private MenuItemPaginationResp FillMenuItemPaginationResp(List<MenuItemResp> menuItemResps, PaginationMeta meta)
        {
            menuItemResps ??= new List<MenuItemResp>();

            return new MenuItemPaginationResp()
            {
                Meta = meta,
                MenuItems = menuItemResps
            };
        }
    }
}
