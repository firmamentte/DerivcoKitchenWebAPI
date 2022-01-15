using System;
using System.Collections.Generic;

namespace DerivcoKitchenWebAPI.Data.Entities
{
    public partial class MenuCategory
    {
        public MenuCategory()
        {
            MenuItems = new HashSet<MenuItem>();
        }

        public Guid MenuCategoryId { get; set; }
        public string Name { get; set; } = null!;
        public int Order { get; set; }

        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}
