using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using dotnet_sp_api.Models.DTOs;
using dotnet_sp_api.Services.Interfaces;

namespace dotnet_sp_api.Controllers
{
    [Route("api/setting")]
    [SwaggerTag("This is a list of interfaces to manage application settings and user preferences.")]
    public class SettingController(IWebHostEnvironment hostingEnvironment, ISettingService setSvc) : ControllerBase
    {
        //private readonly ISettingService _setSvc = setSvc;

        /// <summary>
        /// Gets the member name information.
        /// </summary>
        /// <returns>The member name info.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("name-info/{memberID}")]
        public IActionResult GetMemberNameInfo([FromRoute] int memberID)
        {
            if (ModelState.IsValid)
            {
                return Ok(setSvc.GetMemberNameInfo(memberID));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Saves the member name info.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="fName">First name.</param>
        /// <param name="mName">Middle name.</param>
        /// <param name="lName">Last name.</param>
        [HttpPut]
        [Authorize]
        [Route("update-name-info/{memberID}")]
        public IActionResult SaveMemberNameInfo([FromRoute] int memberID, [FromQuery] string fName, [FromQuery] string mName, [FromQuery] string lName)
        {
            if (ModelState.IsValid)
            {
                setSvc.SaveMemberNameInfo(memberID, fName, mName, lName);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Saves the member email information.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="email">Email.</param>
        [HttpPut]
        [Authorize]
        [Route("update-email-info/{memberID}")]
        public IActionResult SaveMemberEmailInfo([FromRoute] int memberID, [FromQuery] string email)
        {
            if (ModelState.IsValid)
            {
                setSvc.SaveMemberEmailInfo(memberID, email);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Saves the password information.
        /// </summary>
        /// <param name="body"></param>
        [HttpPut]
        [Authorize]
        [Route("update-password_info")]
        public IActionResult SavePasswordInfo([FromBody] PasswordData body)
        {
            if (ModelState.IsValid)
            {
                setSvc.SavePasswordInfo(body);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Saves the security question info.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="questionID">Question identifier.</param>
        /// <param name="answer">Answer.</param>
        [HttpPut]
        [Authorize]
        [Route("save-security_question/{memberID}")]
        public IActionResult SaveSecurityQuestionInfo([FromRoute] int memberID, [FromQuery] int questionID, [FromQuery] string answer)
        {
            if (ModelState.IsValid)
            {
                setSvc.SaveSecurityQuestionInfo(memberID, questionID, answer);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Deactivates the account.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="reason">Reason.</param>
        /// <param name="explanation">Explanation.</param>
        /// <param name="futureEmail">Future email.</param>
        [HttpPost]
        [Authorize]
        [Route("deactivate-account/{memberID}")]
        public IActionResult DeactivateAccount([FromRoute] int memberID, [FromQuery] int reason, [FromQuery] string explanation, [FromQuery] int? futureEmail)
        {
            if (ModelState.IsValid)
            {
                setSvc.DeactivateAccount(memberID, reason, explanation, futureEmail);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Gets the member notifications.
        /// </summary>
        /// <returns>The member notifications.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("notifications/{memberID}")]
        public IActionResult GetMemberNotifications([FromRoute] int memberID)
        {
            if (ModelState.IsValid)
            {
                return Ok(setSvc.GetMemberNotifications(memberID));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Saves the notification settings.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">Body.</param>
        [HttpPut]
        [Authorize]
        [Route("update-notifications/{memberID}")]
        public IActionResult SaveNotificationSettings([FromRoute] int memberID, [FromBody] NotificationsSetting body)
        {
            if (ModelState.IsValid)
            {
                setSvc.SaveNotificationSettings(memberID, body);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Gets the profile settings.
        /// </summary>
        /// <returns>The profile settings.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("profile-settings/{memberID}")]
        public IActionResult GetProfileSettings([FromRoute] int memberID)
        {
            if (ModelState.IsValid)
            {
                return Ok(setSvc.GetProfileSettings(memberID));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Saves the profile settings.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">Body.</param>
        [HttpPut]
        [Authorize]
        [Route("save-profile-settings/{memberID}")]
        public IActionResult SaveProfileSettings([FromRoute] int memberID, [FromBody] PrivacySearchSettings body)
        {
            if (ModelState.IsValid)
            {
                setSvc.SaveProfileSettings(memberID, body);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Gets the privacy search settings.
        /// </summary>
        /// <returns>The privacy search settings.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("privacy-search-settings/{memberID}")]
        public IActionResult GetPrivacySearchSettings([FromRoute] int memberID)
        {
            if (ModelState.IsValid)
            {
                return Ok(setSvc.GetPrivacySearchSettings(memberID));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Saves the privacy search settings.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="visibility">Visibility.</param>
        /// <param name="viewProfilePicture">If set to <c>true</c> view profile picture.</param>
        /// <param name="viewFriendsList">If set to <c>true</c> view friends list.</param>
        /// <param name="viewLinkToRequestAddingYouAsFriend">If set to <c>true</c> view link to request adding you as friend.</param>
        /// <param name="viewLinkToSendYouMsg">If set to <c>true</c> view link to send you message.</param>
        [HttpPut]
        [Authorize]
        [Route("save-privacy-search-settings/{memberID}")]
        public IActionResult SavePrivacySearchSettings(
              [FromRoute] int memberID,
              [FromQuery] int visibility,
              [FromQuery] int viewProfilePicture,
              [FromQuery] int viewFriendsList,
              [FromQuery] int viewLinkToRequestAddingYouAsFriend,
              [FromQuery] int viewLinkToSendYouMsg)
        {
            if (ModelState.IsValid)
            {
                setSvc.SavePrivacySearchSettings(memberID, visibility, viewProfilePicture, viewFriendsList, viewLinkToRequestAddingYouAsFriend,
                                                 viewLinkToSendYouMsg);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Uploads the profile photo.
        /// </summary>
        /// <returns>The profile photo.</returns>
        /// <param name="memberId">Member identifier.</param>
        [HttpPost]
        [Authorize]
        [Route("upload-photo/{memberID}")]
        public void UploadProfilePhoto([FromRoute] string memberId)
        {
            var file = Request.Form.Files[0];
            var ext = file.FileName.Split(".")[1];

            string folderName = "images/members/";
            string webRootPath = hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            if (file.Length > 0)
            {
                string fileName = memberId + "." + ext;
                string fullPath = Path.Combine(newPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                //update table with file name
                setSvc.UpdateProfilePicture(Convert.ToInt32(memberId), fileName);
            }
        }
    }
}
