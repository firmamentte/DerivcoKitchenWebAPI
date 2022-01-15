using DerivcoKitchenWebAPI.BLL.BLLClasses;
using DerivcoKitchenWebAPI.BLL.DataContract;
using DerivcoKitchenWebAPI.Controllers.ControllerHelpers;
using DerivcoKitchenWebAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DerivcoKitchenWebAPI.Controllers
{
    [Route("api/MenuItem")]
    [ApiController]
    [AuthenticateAccessToken]
    public class MenuItemController : ControllerBase
    {
        private readonly IWebHostEnvironment WebHostEnvironment;
        private readonly MenuItemBLL MenuItemBLL;
        private readonly MenuItemControllerHelper MenuItemControllerHelper;

        public MenuItemController(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
            MenuItemBLL = new();
            MenuItemControllerHelper = new();
        }

        [Route("V1/GetMenuItemByMenuItemId")]
        [HttpGet]
        public async Task<ActionResult> GetMenuItemByMenuItemId([FromQuery] Guid menuItemId)
        {
            #region RequestValidation

            ModelState.Clear();

            if (menuItemId == Guid.Empty)
            {
                ModelState.AddModelError("menuItemId", "Menu Item Id must be a globally unique identifier and not empty");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            MenuItemResp _menuItemResp = await MenuItemBLL.GetMenuItemByMenuItemId(menuItemId);

            await MenuItemControllerHelper.GetItemPicturesBase64String(WebHostEnvironment, _menuItemResp);

            return Ok(_menuItemResp);
        }

        [Route("V1/GetMenuItemsByCriteria")]
        [HttpGet]
        public async Task<ActionResult> GetMenuItemsByCriteria
        ([FromQuery] string menuCategoryName, [FromQuery] string? name, [FromQuery] int skip = 0, [FromQuery] int take = 15)
        {
            #region RequestValidation

            #endregion

            MenuItemPaginationResp _menuItemPaginationResp = await MenuItemBLL.GetMenuItemsByCriteria(menuCategoryName, name, skip, take);

            await MenuItemControllerHelper.GetItemPicturesBase64String(WebHostEnvironment, _menuItemPaginationResp.MenuItems);

            return Ok(_menuItemPaginationResp);
        }
    }
}
