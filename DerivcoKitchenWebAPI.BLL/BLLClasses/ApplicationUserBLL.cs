using DerivcoKitchenWebAPI.BLL.DataContract;
using DerivcoKitchenWebAPI.Data.DALClasses;
using DerivcoKitchenWebAPI.Data.Entities;

namespace DerivcoKitchenWebAPI.BLL.BLLClasses
{
    public class ApplicationUserBLL : SharedBLL
    {
        private readonly ApplicationUserDAL ApplicationUserDAL;

        public ApplicationUserBLL()
        {
            ApplicationUserDAL = new();
        }

        public async Task<AuthenticateResp> Authenticate()
        {
            using DerivcoKitchenContext _dbContext = new();

            AccessToken _accessToken = new()
            {
                AccessTokenValue = CreateAccessToken(),
                ExpiryDate = DateTime.Now.AddMonths(1).Date,
            };

            await _dbContext.AddAsync(_accessToken);
            await _dbContext.SaveChangesAsync();

            return FillAuthenticateResp(_accessToken);
        }

        public async Task SignUp(string emailAddress, string userPassword)
        {
            using DerivcoKitchenContext _dbContext = new();

            if (await ApplicationUserDAL.IsUsernameExisting(_dbContext, emailAddress))
            {
                RaiseServerError("Email Address already existing");
            }

            ApplicationUser _applicationUser = new()
            {
                ApplicationUserId = Guid.NewGuid(),
                Username = emailAddress,
                UserPassword = userPassword
            };

            await _dbContext.AddAsync(_applicationUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SignIn(string emailAddress, string userPassword)
        {
            using DerivcoKitchenContext _dbContext = new();

            ApplicationUser _applicationUser = await ApplicationUserDAL.GetApplicationUserByUsernameAndUserPassword(_dbContext, emailAddress, userPassword);

            if (_applicationUser is null)
            {
                RaiseServerError("Invalid Username or Password");
            }

            if (!string.Equals(_applicationUser.Username, emailAddress, StringComparison.CurrentCulture))
            {
                RaiseServerError("Invalid Username or Password");
            }

            if (!string.Equals(_applicationUser.UserPassword, userPassword, StringComparison.CurrentCulture))
            {
                RaiseServerError("Invalid Username or Password");
            }
        }

        public async Task<bool> IsAccessTokenValid(string accessToken)
        {
            using DerivcoKitchenContext _dbContext = new();

            return await ApplicationUserDAL.IsAccessTokenValid(_dbContext, accessToken);
        }

        private string CreateAccessToken()
        {
            return $"{ Guid.NewGuid().ToString().Replace("-", "")}{ Guid.NewGuid().ToString().Replace("-", "")}{ Guid.NewGuid().ToString().Replace("-", "")}{ Guid.NewGuid().ToString().Replace("-", "")}{ Guid.NewGuid().ToString().Replace("-", "")}{ Guid.NewGuid().ToString().Replace("-", "")}";
        }

        private AuthenticateResp FillAuthenticateResp(AccessToken accessToken)
        {
            if (accessToken != null)
            {
                return new AuthenticateResp()
                {
                    AccessToken = accessToken.AccessTokenValue,
                    ExpiryDate = accessToken.ExpiryDate
                };
            }
            else
            {
                return null;
            }
        }
    }
}
