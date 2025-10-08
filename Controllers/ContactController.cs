using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using dotnet_sp_api.Services.Interfaces;
using dotnet_sp_api.Models.DTOs;

namespace dotnet_sp_api.Controllers
{
    [Route("api/contact")]
    [SwaggerTag("Contains API functionalities to manage and control member contacts.")]
    public class ContactController(IContactService contactService) : ControllerBase
    {
        readonly IContactService _contactService = contactService;

        /// <summary>
        /// Gets a list of member contacts by the given member ID and show type.
        /// </summary>
        /// <returns>The member contacts.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="show">Show could be "requests" or "RecentlyAdded".</param>
        [HttpGet]
        [Authorize]
        [Route("contacts")]
        public ActionResult<List<MemberContacts>> Contacts([FromQuery] int memberID, [FromQuery] string show)
        {
            if (ModelState.IsValid)
            {
                return Ok(_contactService.GetMemberContacts(memberID).ToList());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Gets a list of member contact suggestoins by the given member ID and show type.
        /// </summary>
        /// <returns>The member contacts.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("suggestions")]
        public ActionResult<List<MemberContacts>> Suggestions([FromQuery] int memberID)
        {
            if (ModelState.IsValid)
            {
                return Ok(_contactService.GetMemberSuggestions(memberID).ToList());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Sends the request to contact.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        [HttpPost]
        [Authorize]
        [Route("send-request")]
        public IActionResult SendRequest([FromQuery] int memberID, [FromQuery] int contactID)
        {
            if (ModelState.IsValid)
            {
                _contactService.SendRequestContact(memberID, contactID);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Searchs and return list of contacts for a given member ID and search Text.
        /// </summary>
        /// <returns>The member contacts.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="searchText">Search text.</param>
        [HttpGet]
        [Authorize]
        [Route("search-member-contacts")]
        public ActionResult<List<MemberContacts>> SearchMemberContacts([FromQuery] int memberID, [FromQuery] string searchText)
        {
            if (ModelState.IsValid)
            {
                return Ok(_contactService.SearchMemberContacts(memberID, searchText));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Gets the requests list.
        /// </summary>
        /// <returns>The requests list.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("requests")]
        public ActionResult<List<MemberContacts>> GetRequestsList([FromQuery] int memberID)
        {
            if (ModelState.IsValid)
            {
                return Ok(_contactService.GetRequestsList(memberID).ToList());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Gets the search contacts.
        /// </summary>
        /// <returns>The search contacts.</returns>          
        /// <param name="userID">User identifier.</param>
        /// <param name="searchText">Search text.</param>
        [HttpGet]
        [Authorize]
        [Route("search-contacts")]
        public ActionResult<List<MemberContacts>> GetSearchContacts([FromQuery] int userID, [FromQuery] string searchText)
        {
            if (ModelState.IsValid)
            {
                return Ok(_contactService.GetSearchContacts(userID, searchText).ToList());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// member accepts request from contact 
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        [HttpPost]
        [Authorize]
        [Route("accept-request")]
        public IActionResult AcceptRequest([FromQuery] int memberID, [FromQuery] int contactID)
        {
            if (ModelState.IsValid)
            {
                _contactService.AcceptRequest(memberID, contactID);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        /// <summary>
        /// Member rejects the request from contact.
        /// </summary>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        [HttpPost]
        [Authorize]
        [Route("reject-request")]
        public IActionResult RejectRequest([FromQuery] int memberID, [FromQuery] int contactID)
        {
            if (ModelState.IsValid)
            {
                _contactService.RejectRequest(memberID, contactID);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Deletes the contact.
        /// </summary>  
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        [HttpDelete]
        [Authorize]
        [Route("delete-contact")]
        public IActionResult DeleteContact([FromQuery] int memberID, [FromQuery] int contactID)
        {
            if (ModelState.IsValid)
            {
                _contactService.DeleteContact(memberID, contactID);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// reuturns the list of Contacts by search text.
        /// </summary>
        /// <returns>The results.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="searchText">Search text.</param>
        [HttpGet]
        [Authorize]
        [Route("search-results")]
        public ActionResult<List<Search>> SearchResults([FromQuery] int memberID, [FromQuery] string searchText)
        {
            if (ModelState.IsValid)
            {
                return Ok(_contactService.SearchResults(memberID, searchText).ToList());

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// reuturns the list of members I am following
        /// </summary>
        /// <returns>The results.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("people-iam-following")]
        public ActionResult<List<MemberContacts>> GetPeopleIFollow([FromQuery] int memberID)
        {
            if (ModelState.IsValid)
            {
                return Ok(_contactService.GetPeopleIFollow(memberID).ToList());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// reuturns the list of whose following me.
        /// </summary>
        /// <returns>The results.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("whose-following-me")]
        public ActionResult<List<MemberContacts>> GetWhosFollowingMe([FromQuery] int memberID)
        {
            if (ModelState.IsValid)
            {
                return Ok(_contactService.GetWhosFollowingMe(memberID).ToList());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// follow member
        /// </summary>  
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        [HttpPost]
        [Authorize]
        [Route("follow-member")]
        public IActionResult FollowMember([FromQuery] int memberID, [FromQuery] int contactID)
        {
            if (ModelState.IsValid)
            {
                _contactService.FollowMember(memberID, contactID);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Unfollow member
        /// </summary>  
        /// <param name="memberID">Member identifier.</param>
        /// <param name="contactID">Contact identifier.</param>
        [HttpPost]
        [Authorize]
        [Route("unfollow-member")]
        public IActionResult UnfollowMember([FromQuery] int memberID, [FromQuery] int contactID)
        {
            if (ModelState.IsValid)
            {
                _contactService.UnfollowMember(memberID, contactID);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
