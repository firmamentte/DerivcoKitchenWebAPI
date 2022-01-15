namespace DerivcoKitchenWebAPI.BLL.BLLClasses
{
    public class SharedBLL
    {
        public void RaiseServerError(string errorMessage)
        {
            try
            {
                throw new DerivcoKitchenWebAPIException(errorMessage);
            }
            catch (DerivcoKitchenWebAPIException)
            {
                throw;
            }
        }

        public string GetEnumDescription(Enum enumValue)
        {
            return FirmamentUtilities.Utilities.GetEnumDescription(enumValue);
        }

        public bool IsEmailAddress(string emailAddress)
        {
            return FirmamentUtilities.Utilities.EmailHelper.IsEmailAddress(emailAddress);
        }
    }
}
