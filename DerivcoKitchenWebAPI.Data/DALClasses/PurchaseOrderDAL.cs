using DerivcoKitchenWebAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DerivcoKitchenWebAPI.Data.DALClasses
{
    public class PurchaseOrderDAL
    {
        public async Task<bool> IsPurchaseOrderNumberExisting(DerivcoKitchenContext dbContext, string purchaseOrderNumber)
        {
            return await (from purchaseOrder in dbContext.PurchaseOrders.Cast<PurchaseOrder>()
                          where purchaseOrder.PurchaseOrderNumber == purchaseOrderNumber
                          select purchaseOrder).
                          AnyAsync();
        }
    }
}
