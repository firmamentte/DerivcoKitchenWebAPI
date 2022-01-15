using System;
using System.Collections.Generic;

namespace DerivcoKitchenWebAPI.Data.Entities
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            MenuItemPurchaseOrders = new HashSet<MenuItemPurchaseOrder>();
        }

        public Guid PurchaseOrderId { get; set; }
        public Guid ApplicationUserId { get; set; }
        public string PurchaseOrderNumber { get; set; } = null!;
        public string PaymentStatus { get; set; } = null!;
        public string ShippingStatus { get; set; } = null!;
        public DateTime CreationDate { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
        public virtual ICollection<MenuItemPurchaseOrder> MenuItemPurchaseOrders { get; set; }
    }
}
