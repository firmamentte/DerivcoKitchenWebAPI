using System;
using System.Collections.Generic;

namespace DerivcoKitchenWebAPI.Data.Entities
{
    public partial class ApplicationUser
    {
        public ApplicationUser()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }

        public Guid ApplicationUserId { get; set; }
        public string Username { get; set; } = null!;
        public string UserPassword { get; set; } = null!;

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
