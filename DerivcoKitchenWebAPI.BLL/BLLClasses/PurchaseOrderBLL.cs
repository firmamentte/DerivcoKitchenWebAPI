using DerivcoKitchenWebAPI.BLL.DataContract;
using DerivcoKitchenWebAPI.Data.DALClasses;
using DerivcoKitchenWebAPI.Data.Entities;

namespace DerivcoKitchenWebAPI.BLL.BLLClasses
{
    public class PurchaseOrderBLL : SharedBLL
    {
        private readonly ApplicationUserDAL ApplicationUserDAL;
        private readonly PurchaseOrderDAL PurchaseOrderDAL;
        private readonly MenuItemDAL MenuItemDAL;

        public PurchaseOrderBLL()
        {
            ApplicationUserDAL = new();
            PurchaseOrderDAL = new();
            MenuItemDAL = new();
        }

        public async Task<PurchaseOrderResp> CreatePurchaseOrder(string username, List<LineItemReq> lineItems)
        {
            using DerivcoKitchenContext _dbContext = new();

            PurchaseOrder _purchaseOrder = new()
            {
                ApplicationUser = await ApplicationUserDAL.GetApplicationUserByUsername(_dbContext, username),
                PurchaseOrderNumber = await CreatePurchaseOrderNumber(),
                PaymentStatus = GetEnumDescription(DerivcoKitchenWebAPIEnum.Status.Pending),
                ShippingStatus = GetEnumDescription(DerivcoKitchenWebAPIEnum.Status.Pending),
                CreationDate = DateTime.Now.Date
            };

            foreach (LineItemReq _lineItemReq in lineItems)
            {
                MenuItem _menuItem = await MenuItemDAL.GetMenuItemByMenuItemId(_dbContext, _lineItemReq.MenuItemId);

                if (_menuItem is null)
                {
                    RaiseServerError("Invalid Item Detail Id. The resource has been removed, had its name changed, or is unavailable.");
                }

                _purchaseOrder.MenuItemPurchaseOrders.Add(new MenuItemPurchaseOrder()
                {
                    PurchaseOrder = _purchaseOrder,
                    MenuItem = _menuItem,
                    PictureFileName = _lineItemReq.PictureFileName,
                    Quantity = _lineItemReq.Quantity,
                    Price = _lineItemReq.Price,
                    CreationDate = _purchaseOrder.CreationDate
                });
            }

            await _dbContext.AddAsync(_purchaseOrder);
            await _dbContext.SaveChangesAsync();

            return FillPurchaseOrderResp(_purchaseOrder);
        }

        private async Task<string> CreatePurchaseOrderNumber()
        {
            using DerivcoKitchenContext _dbContext = new();

            Random _random = new();

            string _purchaseOrderNumber = _random.Next(10000, 2000000000).ToString();

            while (await PurchaseOrderDAL.IsPurchaseOrderNumberExisting(_dbContext, _purchaseOrderNumber))
            {
                _purchaseOrderNumber = _random.Next(10000, 2000000000).ToString();
            }

            return _purchaseOrderNumber;
        }

        private PurchaseOrderResp FillPurchaseOrderResp(PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder != null)
            {
                return new PurchaseOrderResp()
                {
                    PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                    PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber,
                    PaymentStatus = purchaseOrder.PaymentStatus,
                    ShippingStatus = purchaseOrder.ShippingStatus,
                    AmountDue = purchaseOrder.AmountDue,
                    CreationDate = purchaseOrder.CreationDate
                };
            }
            else
            {
                return null;
            }
        }

    }
}
