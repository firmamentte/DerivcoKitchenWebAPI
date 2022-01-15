using System;
using System.Collections.Generic;

namespace DerivcoKitchenWebAPI.Data.Entities
{
    public partial class MenuItem
    {
        public MenuItem()
        {
            MenuItemPurchaseOrders = new HashSet<MenuItemPurchaseOrder>();
        }

        public Guid MenuItemId { get; set; }
        public Guid MenuCategoryId { get; set; }
        public string PictureFileName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Visible { get; set; }
        public decimal Price { get; set; }
        public int Order { get; set; }

        public virtual MenuCategory MenuCategory { get; set; } = null!;
        public virtual ICollection<MenuItemPurchaseOrder> MenuItemPurchaseOrders { get; set; }
    }
}
