using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using dotnet_sp_api.Interfaces;
using dotnet_sp_api.Models.DTOs;

namespace dotnet_sp_api.Controllers
{
    [Route("api/account")]
    [SwaggerTag("This is a list of interfaces containing member account functionalities such as registering and loging in users.")]
    public class AccountController(IAccountService accountService) : ControllerBase
    {
        /// <summary>
        /// creates JWT token if user is authenticated and allow user login.
        /// </summary>
        /// <param name="loginModel">The login credentials data.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Login loginModel)
        {
            if (ModelState.IsValid)
            {
                return Ok(accountService.AuthenticateUser(loginModel));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Login new registered user.
        /// </summary>
        /// <param name="body">The data model for the new registered user.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("login-new-registered-user")]
        public IActionResult LoginNewRegisteredUser([FromBody] NewRegisteredUser body)
        {
            if (ModelState.IsValid)
            {
                return Ok(accountService.AuthenticateNewRegisteredUser(body.Email, body.Code));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Refreshes a given token.
        /// </summary>
        /// <param name="accessToken">the old token that is to be refreshed.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("refresh-login")]
        public IActionResult RefreshToken([FromQuery] string accessToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(accountService.RefreshToken(accessToken));
        }

        /// <summary>
        /// Registers a user.
        /// </summary>
        /// <param name="body">The user's information to register.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public IActionResult RegisterUser([FromBody] Register body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(accountService.RegisterUser(body));
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <returns>The password.</returns>
        /// <param name="email">Email.</param>
        [HttpGet]
        [Route("reset-password")]
        public IActionResult ResetPassword([FromQuery] string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(accountService.ResetPassword(email));
        }

        /// <summary>
        /// Is the reset code expired, yes or no.
        /// </summary>
        /// <returns>The reset code expired.</returns>
        /// <param name="code">Code.</param>
        [HttpGet]
        [Route("is-reset-code-expired")]
        public IActionResult IsResetCodeExpired([FromQuery] string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string strCode = code.Trim();
            return Ok(accountService.CheckCodeExpired(Convert.ToInt32(strCode)));
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <returns>The password.</returns>
        /// <param name="pwd">Pwd.</param>
        /// <param name="email">Email.</param>
        /// <param name="code">Code.</param>
        [HttpGet]
        [Route("change-password")]
        public IActionResult ChangePassword([FromQuery] string pwd,
                                       [FromQuery] string email,
                                       [FromQuery] string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(accountService.ChangePassword(pwd, email, code));
        }
        /// <summary>
        /// This endpoint will set the status (active=2, deactivated=3, newly-register=1) for the member id.
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="status"></param>
        [HttpPut]
        [Route("set-member-status/{memberID}/{status}")]
        public IActionResult SetMemberStatus([FromRoute] int memberID, [FromRoute] int status)
        {
            if (ModelState.IsValid)
            {
                accountService.SetMemberStatus(memberID, status);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}