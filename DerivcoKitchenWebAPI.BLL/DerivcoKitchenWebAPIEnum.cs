using System.ComponentModel;

namespace DerivcoKitchenWebAPI.BLL
{
    public class DerivcoKitchenWebAPIEnum
    {
        public enum Status
        {
            [Description("Pending")]
            Pending,
            [Description("Paid")]
            Paid,
            [Description("Shipping")]
            Shipping,
            [Description("Delivered")]
            Delivered,
            [Description("Refund")]
            Refund,
            [Description("Refunded")]
            Refunded,
            [Description("Failed")]
            Failed
        }
    }
}
