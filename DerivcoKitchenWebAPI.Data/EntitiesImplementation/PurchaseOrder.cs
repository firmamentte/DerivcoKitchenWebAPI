namespace DerivcoKitchenWebAPI.Data.Entities
{
    public partial class PurchaseOrder
    {
        public virtual decimal AmountDue
        {
            get
            {
                decimal _amountDue = decimal.Zero;

                foreach (MenuItemPurchaseOrder _menuItemPurchaseOrder in MenuItemPurchaseOrders)
                {
                    _amountDue += _menuItemPurchaseOrder.SubTotal;
                }

                return _amountDue;
            }
        }
    }
}
