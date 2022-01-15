using DerivcoKitchenWebAPI.BLL.BLLClasses;
using DerivcoKitchenWebAPI.BLL.DataContract;
using DerivcoKitchenWebAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Net;

namespace DerivcoKitchenWebAPI.Controllers
{
    [Route("api/ApplicationUser")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly ApplicationUserBLL ApplicationUserBLL;
        public ApplicationUserController()
        {
            ApplicationUserBLL = new();
        }

        [Route("V1/Authenticate")]
        [HttpPost]
        public async Task<ActionResult> Authenticate()
        {
            #region RequestValidation

            #endregion

            AuthenticateResp _authenticateResp = await ApplicationUserBLL.Authenticate();

            Response.Headers.Add("AccessToken", _authenticateResp.AccessToken);
            Response.Headers.Add("AccessTokenExpiryDate", _authenticateResp.ExpiryDate.ToString());

            return Ok();
        }

        [Route("V1/SignUp")]
        [HttpPost]
        [AuthenticateAccessToken]
        public async Task<ActionResult> SignUp()
        {
            #region RequestValidation

            ModelState.Clear();

            if (!Request.Headers.TryGetValue("EmailAddress", out StringValues _emailAddress))
            {
                ModelState.AddModelError("EmailAddress", "Email Address required");
            }

            if (!Request.Headers.TryGetValue("UserPassword", out StringValues _userPassword))
            {
                ModelState.AddModelError("UserPassword", "User Password required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            await ApplicationUserBLL.SignUp(_emailAddress.FirstOrDefault(), _userPassword.FirstOrDefault());

            return StatusCode((int)HttpStatusCode.Created);
        }

        [Route("V1/SignIn")]
        [HttpPut]
        [AuthenticateAccessToken]
        public async Task<ActionResult> SignIn()
        {
            #region RequestValidation

            ModelState.Clear();

            if (!Request.Headers.TryGetValue("EmailAddress", out StringValues _emailAddress))
            {
                ModelState.AddModelError("EmailAddress", "Email Address required");
            }

            if (!Request.Headers.TryGetValue("UserPassword", out StringValues _userPassword))
            {
                ModelState.AddModelError("UserPassword", "User Password required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            await ApplicationUserBLL.SignIn(_emailAddress.FirstOrDefault(), _userPassword.FirstOrDefault());

            return Ok();
        }
    }
}
