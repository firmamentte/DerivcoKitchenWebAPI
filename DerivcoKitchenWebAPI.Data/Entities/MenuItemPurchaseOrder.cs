using System;
using System.Collections.Generic;

namespace DerivcoKitchenWebAPI.Data.Entities
{
    public partial class MenuItemPurchaseOrder
    {
        public Guid MenuItemPurchaseOrderId { get; set; }
        public Guid MenuItemId { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public string PictureFileName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual MenuItem MenuItem { get; set; } = null!;
        public virtual PurchaseOrder PurchaseOrder { get; set; } = null!;
    }
}
