using DerivcoKitchenWebAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DerivcoKitchenWebAPI.Data.DALClasses
{
    public class ApplicationUserDAL
    {
        public async Task<bool> IsAccessTokenValid(DerivcoKitchenContext dbContext, string accessTokenValue)
        {
            return await (from accessToken in dbContext.AccessTokens.Cast<AccessToken>()
                          where accessToken.AccessTokenValue == accessTokenValue &&
                          accessToken.ExpiryDate >= DateTime.Now.Date
                          select accessToken).
                          AnyAsync();
        }

        public async Task<ApplicationUser> GetApplicationUserByUsername(DerivcoKitchenContext dbContext, string username)
        {
            return await (from applicationUser in dbContext.ApplicationUsers.Cast<ApplicationUser>()
                          where applicationUser.Username == username
                          select applicationUser).
                          FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser> GetApplicationUserByUsernameAndUserPassword(DerivcoKitchenContext dbContext, string username, string userPassword)
        {
            return await (from applicationUser in dbContext.ApplicationUsers.Cast<ApplicationUser>()
                          where applicationUser.Username == username &&
                                applicationUser.UserPassword == userPassword
                          select applicationUser).
                          FirstOrDefaultAsync();
        }

        //If change this function also change GetApplicationUserByUsername function
        public async Task<bool> IsUsernameExisting(DerivcoKitchenContext dbContext, string username)
        {
            return await (from applicationUser in dbContext.ApplicationUsers.Cast<ApplicationUser>()
                          where applicationUser.Username == username
                          select applicationUser).
                          AnyAsync();
        }
    }
}
