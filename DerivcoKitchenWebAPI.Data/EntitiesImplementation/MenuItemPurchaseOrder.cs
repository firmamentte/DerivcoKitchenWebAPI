namespace DerivcoKitchenWebAPI.Data.Entities
{
    public partial class MenuItemPurchaseOrder
    {
        public virtual string Name
        {
            get
            {
                return MenuItem.Name;
            }
        }

        public virtual string Description
        {
            get
            {
                return MenuItem.Description;
            }
        }

        public virtual string PaymentStatus
        {
            get
            {
                return PurchaseOrder.PaymentStatus;
            }
        }

        public virtual decimal SubTotal
        {
            get
            {
                return Price * Quantity;
            }
        }
    }
}
