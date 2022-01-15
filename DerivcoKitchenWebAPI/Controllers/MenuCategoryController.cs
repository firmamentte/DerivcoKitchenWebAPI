using DerivcoKitchenWebAPI.BLL.BLLClasses;
using DerivcoKitchenWebAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DerivcoKitchenWebAPI.Controllers
{
    [Route("api/MenuCategory")]
    [ApiController]
    [AuthenticateAccessToken]
    public class MenuCategoryController : ControllerBase
    {
        private readonly MenuCategoryBLL MenuCategoryBLL;

        public MenuCategoryController()
        {
            MenuCategoryBLL = new();
        }

        [Route("V1/GetMenuCategories")]
        [HttpGet]
        public async Task<ActionResult> GetMenuCategories()
        {
            #region RequestValidation
  
            #endregion

            return Ok(await MenuCategoryBLL.GetMenuCategories());
        }
    }
}
