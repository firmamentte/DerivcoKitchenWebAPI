﻿namespace DerivcoKitchenWebAPI.BLL.DataContract
{
    public class MenuItemResp
    {
        public Guid MenuItemId { get; set; }
        public string PictureFileName { get; set; }
        public string PictureBase64String { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Order { get; set; }
    }
}
