using DerivcoKitchenWebAPI.BLL.BLLClasses;
using DerivcoKitchenWebAPI.BLL.DataContract;
using DerivcoKitchenWebAPI.Controllers.ControllerHelpers;
using DerivcoKitchenWebAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DerivcoKitchenWebAPI.Controllers
{
    [Route("api/PurchaseOrder")]
    [ApiController]
    [AuthenticateAccessToken]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly PurchaseOrderBLL PurchaseOrderBLL;
        private readonly SharedHelper SharedHelper;

        public PurchaseOrderController()
        {
            PurchaseOrderBLL = new();
            SharedHelper = new();
        }

        [Route("V1/CreatePurchaseOrder")]
        [HttpPost]
        public async Task<ActionResult> CreatePurchaseOrder([FromBody] List<LineItemReq> lineItems)
        {

            #region RequestValidation

            ModelState.Clear();

            if (lineItems is null)
            {
                ModelState.AddModelError("LineItems", "Line Items request can not be null");
            }
            else
            {
                if (!lineItems.Any())
                {
                    ModelState.AddModelError("EmptyLineItems", "At least one Line Item must be supplied");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            return Created(string.Empty, await PurchaseOrderBLL.CreatePurchaseOrder(SharedHelper.GetHeaderUsername(Request), lineItems));
        }
    }
}
