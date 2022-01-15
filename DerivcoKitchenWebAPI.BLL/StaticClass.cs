using Microsoft.Extensions.Configuration;

namespace DerivcoKitchenWebAPI.BLL
{
    public static class StaticClass
    {
        public static void InitializeAppSettings(IConfiguration configuration)
        {
            Data.StaticClass.DatabaseHelper.ConnectionString ??= configuration["ConnectionStrings:DatabasePath"];
        }
    }
}
