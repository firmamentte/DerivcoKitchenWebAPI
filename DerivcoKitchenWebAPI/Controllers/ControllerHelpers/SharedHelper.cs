using DerivcoKitchenWebAPI.BLL;
using Microsoft.Extensions.Primitives;

namespace DerivcoKitchenWebAPI.Controllers.ControllerHelpers
{
    public class SharedHelper
    {
        public string GetHeaderUsername(HttpRequest request)
        {
            if (request.Headers.TryGetValue("EmailAddress", out StringValues _username))
                return _username.FirstOrDefault();
            else
                return null;
        }
    }
}
