using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using dotnet_sp_api.Models.DTOs;
using dotnet_sp_api.Services.Interfaces;

namespace dotnet_sp_api.Controllers
{
    [Route("api/message")]
    [SwaggerTag("Contains API functionalities for messaging or communication between members.")]
    public class MessageController(IMessageService msgSvc) : ControllerBase
    {
        /// <summary>
        /// Gets the member messages.
        /// </summary>
        /// <returns>The member messages.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="showType">Show type.</param>
        [HttpGet]
        [Authorize]
        [Route("messages/{memberID}")]
        public IActionResult GetMemberMessages([FromRoute] int memberID, [FromQuery] string showType)
        {
            if (ModelState.IsValid)
            {
                return Ok(msgSvc.GetMemberMessages(memberID, showType));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Gets the total unread messages.
        /// </summary>
        /// <returns>The total unread messages.</returns>
        /// <param name="memberID">Member identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("total-unread-messages/{memberID}")]
        public IActionResult GetTotalUnreadMessages([FromRoute] int memberID)
        {
            if (ModelState.IsValid)
            {
                return Ok(msgSvc.GetTotalUnreadMessages(memberID));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Creates a message.
        /// </summary>
        /// <param name="body">Body</param>
        [HttpPost]
        [Authorize]
        [Route("send-message")]
        public IActionResult CreateMessage([FromBody] MessageInputs body)
        {
            if (ModelState.IsValid)
            {
                msgSvc.CreateMessage(body);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Toggles the state of the message.
        /// </summary>
        /// <param name="status">Status.</param>
        /// <param name="msgID">Message identifier.</param>
        [HttpPut]
        [Route("toggle-message-state")]
        public IActionResult ToggleMessageState([FromQuery] int status, [FromQuery] int msgID)
        {
            if (ModelState.IsValid)
            {
                msgSvc.ToggleMessageState(status, msgID);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Gets the message info by identifier.
        /// </summary>
        /// <returns>The message info by identifier.</returns>
        /// <param name="msgID">Message identifier.</param>
        [HttpGet]
        [Authorize]
        [Route("message-info/{msgID}")]
        public ActionResult<List<MessageInfo>> GetMessageInfoByID([FromRoute] int msgID)
        {
            if (ModelState.IsValid)
            {
                return Ok(msgSvc.GetMessageInfoByID(msgID));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="msgID">Message identifier.</param>
        [HttpDelete]
        [Authorize]
        [Route("delete/{msgID}")]
        public IActionResult DeleteMessage([FromRoute] int msgID)
        {
            if (ModelState.IsValid)
            {
                msgSvc.DeleteMessage(msgID);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Searchs messages given search key for member id.
        /// </summary>
        /// <returns>The messages.</returns>
        /// <param name="memberID">Member identifier.</param>
        /// <param name="searchKey">Search key.</param>
        [HttpGet]
        [Authorize]
        [Route("search-messages/{memberID}")]
        public ActionResult<List<SearchMessages>> SearchMessages([FromRoute] int memberID, [FromQuery] string searchKey)
        {
            if (ModelState.IsValid)
            {
                return Ok(msgSvc.SearchMessages(memberID, searchKey));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
