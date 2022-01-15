namespace DerivcoKitchenWebAPI.BLL
{
    public class DerivcoKitchenWebAPIException : Exception
    {
        public DerivcoKitchenWebAPIException(string errorMessage) : base(errorMessage)
        { }
    }
}
