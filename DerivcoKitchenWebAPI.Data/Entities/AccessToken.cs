﻿using System;
using System.Collections.Generic;

namespace DerivcoKitchenWebAPI.Data.Entities
{
    public partial class AccessToken
    {
        public Guid AccessTokenId { get; set; }
        public string AccessTokenValue { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }
    }
}
