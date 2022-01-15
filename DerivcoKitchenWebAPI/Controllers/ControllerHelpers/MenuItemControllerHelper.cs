using DerivcoKitchenWebAPI.BLL.DataContract;

namespace DerivcoKitchenWebAPI.Controllers.ControllerHelpers
{
    public class MenuItemControllerHelper
    {
        public async Task<string> GetItemPictureBase64String(IWebHostEnvironment webHostEnvironment, string pictureName)
        {
            string _path = Path.Combine(webHostEnvironment.ContentRootPath, "Content", "MenuItemPictures", pictureName);

            return File.Exists(_path) ? Convert.ToBase64String(await File.ReadAllBytesAsync(_path)) : null;
        }

        public async Task GetItemPicturesBase64String(IWebHostEnvironment webHostEnvironment, MenuItemResp menuItemResp)
        {
            if (menuItemResp != null)
            {
                menuItemResp.PictureBase64String = await GetItemPictureBase64String(webHostEnvironment, menuItemResp.PictureFileName);
            }
        }

        public async Task GetItemPicturesBase64String(IWebHostEnvironment webHostEnvironment, List<MenuItemResp> menuItemResps)
        {
            foreach (var itemResp in menuItemResps)
            {
                await GetItemPicturesBase64String(webHostEnvironment, itemResp);
            }
        }
    }
}
