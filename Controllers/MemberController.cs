using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using dotnet_sp_api.Models.DTOs;
using dotnet_sp_api.Services.Interfaces;

namespace dotnet_sp_api.Controllers
{
    [Route("api/member")]
    [SwaggerTag("Contains member management API functionalities.")]
    public class MemberController(IMemberService mbrService) : ControllerBase
    {

        /// <summary>
        /// Creates the member post.
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="postMsg"></param>
        [HttpPost]
        [Authorize]
        [Route("create-post/{memberID}")]
        public IActionResult CreateMemberPosts([FromRoute] int memberID, [FromQuery] string postMsg)
        {
            if (ModelState.IsValid)
            {
                mbrService.CreateMemberPost(memberID, postMsg);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Creates the member post response.
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="postID"></param>
        /// <param name="postMsg"></param>
        [HttpPost]
        [Authorize]
        [Route("create-post-response/{memberID}/{postID}")]
        public IActionResult CreatePostResponse([FromRoute] int memberID, [FromRoute] int postID, [FromQuery] string postMsg)
        {
            if (ModelState.IsValid)
            {
                mbrService.CreateMemberPostResponse(memberID, postID, postMsg);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// This endpoint returns the list of member's recent post responses listing.
        /// </summary>
        /// <returns>The recent post responses.</returns>
        /// <param name="postID">Post identifier.</param>
        [Authorize]
        [HttpGet]
        [Route("post-responses/{postID}")]
        public ActionResult<List<PostResponse>> GetRecentPostResponses([FromRoute] int postID)
        {
            if (ModelState.IsValid)
            {
                return Ok(mbrService.GetMemberPostResponses(postID));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// This endpoint returns the list of member's recent posts listing.
        /// </summary>
        /// <returns>The recent posts.</returns>
        /// <param name="memberID">Member identifier.</param>
        [Authorize]
        [HttpGet]
        [Route("posts/{memberID}")]
        public ActionResult<List<Posts>> GetRecentPosts([FromRoute] int memberID)
        {
            if (ModelState.IsValid)
            {
                return Ok(mbrService.GetMemberPosts(memberID));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Gets the member general info v2.
        /// </summary>
        /// <returns>The member general info v2.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("general-info/{memberID}")]
        public ActionResult<MemberProfileGenInfo> GetMemberGeneralInfoV2([FromRoute] int memberID)
        {
            if (ModelState.IsValid)
            {
                return Ok(mbrService.GetMemberGeneralInfo(memberID));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Gets member contact information.
        /// </summary>
        /// <returns>The member contact info.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("contact-info/{memberID}")]
        public ActionResult<MemberContactInfo> GetMemberContactInfo([FromRoute] int memberID)
        {
            if (ModelState.IsValid)
            {
                return Ok(mbrService.GetMemberContactInfo(memberID));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Gets member education information.
        /// </summary>
        /// <returns>The member education info.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("education-info/{memberID}")]
        public ActionResult<List<MemberProfileEducation>> GetMemberEducationInfo([FromRoute] int memberID)
        {
            if (ModelState.IsValid)
            {
                return Ok(mbrService.GetMemberEducationInfo(memberID));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Saves or update the member general info.
        /// </summary>
        /// <param name="body">input data of the MemberProfileGenInfo Model.</param>
        [HttpPost]
        [Authorize]
        [Route("general-info")]
        public IActionResult SaveMemberGeneralInfo([FromBody] MemberProfileGenInfo body)
        {
            if (ModelState.IsValid)
            {
                mbrService.SaveMemberGeneralInfo(body);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Saves or update the member contact information.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">model input data.</param>
        [HttpPost]
        [Authorize]
        [Route("contact-info/{memberID}")]
        public IActionResult SaveMemberContactInfo([FromRoute] int memberID, [FromBody] MemberContactInfo body)
        {
            if (ModelState.IsValid)
            {
                mbrService.SaveMemberContactInfo(memberID, body);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Adds a new member school to db for id.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">Body.</param>
        [HttpPost]
        [Authorize]
        [Route("add-school/{memberID}")]
        public IActionResult AddMemberSchool([FromRoute] int memberID, [FromBody] MemberProfileEducation body)
        {
            if (ModelState.IsValid)
            {
                mbrService.AddMemberSchool(memberID, body);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Updates the member school id info for the member id.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="body">Body.</param>
        [HttpPut]
        [Authorize]
        [Route("update-school/{memberID}")]
        public IActionResult UpdateMemberSchool([FromRoute] int memberID, [FromBody] MemberProfileEducation body)
        {
            if (ModelState.IsValid)
            {
                mbrService.UpdateMemberSchool(memberID, body);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Removes the member school id for the member id.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="instID">Inst identifier.</param>
        /// <param name="instType">Inst type.</param>
        [HttpDelete]
        [Authorize]
        [Route("remove-school")]
        public IActionResult RemoveMemberSchool([FromQuery] int memberID, [FromQuery] int instID, [FromQuery] string instType)
        {
            if (ModelState.IsValid)
            {
                mbrService.RemoveMemberSchool(memberID, instID, instType);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Gets the youtube channel id.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("youtube-channel/{memberID}")]
        public ActionResult<YoutubeChannel> GetYoutubeChannel([FromRoute] int memberID)
        {
            if (ModelState.IsValid)
            {
                return Ok(mbrService.GetYoutubeChannel(memberID));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// saves or update member youtube channel id.
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("youtube-channel")]
        public IActionResult SetYoutubeChannel([FromBody] YoutubeChannel body)
        {
            if (ModelState.IsValid)
            {
                mbrService.SetYoutubeChannel(body);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// get instagram url for a member id.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("instagram-url/{memberID}")]
        public ActionResult<InstagramURL> GetInstagramURL([FromRoute] int memberID)
        {
            if (ModelState.IsValid)
            {
                return Ok(mbrService.GetInstagramURL(memberID));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// set instagram url for the member id.
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("instagram-url")]
        public IActionResult SetInstagramURL([FromBody] InstagramURL body)
        {
            if (ModelState.IsValid)
            {
                mbrService.SetInstagramURL(body);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Get utube video playlist.
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("video-playlist/{memberId}")]
        public ActionResult<List<YoutubePlayList>> GetVideoPlayList([FromRoute] int memberId)
        {
            if (ModelState.IsValid)
            {
                return Ok(mbrService.GetVideoPlayList(memberId));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Get videos list for a player list.
        /// </summary>
        /// <param name="playListID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("youtube-videos/{playListID}")]
        public ActionResult<YoutubeVideosList> GetVideosList([FromRoute] string playListID)
        {
            if (ModelState.IsValid)
            {
                return Ok(mbrService.GetVideosList(playListID));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// increment post like counter by 1 for a post id.
        /// </summary>
        /// <param name="postID"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("increment-post-like-counter/{postID}")]
        public IActionResult IncrementPostLikeCounter([FromRoute] int postID)
        {
            if (ModelState.IsValid)
            {
                mbrService.IncrementPostLikeCounter(postID);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// checks to see if member is a friend or is in contact with contact id.
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="contactID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("is-friend-by-contact/{memberID}/{contactID}")]
        public IActionResult IsFriendByContact([FromRoute] int memberID, [FromRoute] int contactID)
        {
            if (ModelState.IsValid)
            {
                return Ok(mbrService.IsFriendByContact(memberID, contactID));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// checks if member is following contact.
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="contactID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("is-following-contact/{memberID}/{contactID}")]
        public IActionResult IsFollowingContact([FromRoute] int memberID, [FromRoute] int contactID)
        {
            if (ModelState.IsValid)
            {
                return Ok(mbrService.IsFollowingContact(memberID, contactID));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
