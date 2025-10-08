using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using dotnet_sp_api.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using dotnet_sp_api.Models.DTOs;
using dotnet_sp_api.Models.DBContextModels;

namespace dotnet_sp_api.Controllers
{
    /// <summary>
    /// A collection of common interfaces and shared functionalities used by the SP.
    /// </summary>
    [Route("api/common")]
    [SwaggerTag("a collection of common interfaces and shared functionalities used by the SP.")]
    public class CommonController(ILogger<CommonController> log, ICommonService comService, ILoggerFactory loggerFactory, IConfiguration configuration) : ControllerBase
    {
        readonly ICommonService _comService = comService;
        readonly ILogger<CommonController> _log = log;
        readonly ILogger _loggerFactory = loggerFactory.CreateLogger("SP_APP");
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// Gets the list of states.
        /// </summary>
        /// <returns>The states.</returns>
        [HttpGet]
        [Authorize]
        [Route("states")]
        public ActionResult<List<Tbstate>> GetStates()
        {
            if (ModelState.IsValid)
            {
                return Ok(_comService.GetStates());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Logs the specified obj log model to file including error msg and stack trace.
        /// </summary>
        /// <param name="message">the error message to log.</param>
        /// <param name="stack">the errorstack to log.</param>
        [HttpGet]
        [Route("logs")]
        public IActionResult Logs([FromQuery] string message, [FromQuery] string stack)
        {
            if (ModelState.IsValid)
            {
                return Ok(_comService.Logs(message, stack));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// returns sports lists.
        /// </summary>
        /// <returns>SportsList</returns>
        [HttpGet]
        [Authorize]
        [Route("sports")]
        public ActionResult<List<Tbsport>> GetSportsList()
        {
            if (ModelState.IsValid)
            {
                return Ok(_comService.GetSportsList());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Gets school by state.
        /// </summary>
        /// <returns>The school by state.</returns>
        /// <param name="state">State.</param>
        /// <param name="institutionType">Institution type.</param>
        [HttpGet]
        [Authorize]
        [Route("schools")]
        public ActionResult<List<SchoolByState>> GetSchoolByState([FromQuery] string state, [FromQuery] string institutionType)
        {
            if (ModelState.IsValid)
            {
                return Ok(_comService.GetSchoolsByState(state, institutionType));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// returns a list of ads depending on type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("ads")]
        public ActionResult<List<Ads>> GetAds([FromQuery] string type)
        {
            if (ModelState.IsValid)
            {
                return Ok(_comService.GetAds(type));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Get the recent news.
        /// </summary>
        /// <returns>The recent news.</returns>
        [Authorize]
        [HttpGet]
        [Route("news")]
        public ActionResult<List<Tbrecentnews>> GetRecentNews()
        {
            if (ModelState.IsValid)
            {
                return Ok(_comService.GetRecentNews());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Sends the advertisement info.
        /// </summary>
        /// <returns>The advertisement info.</returns>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="company">Company.</param>
        /// <param name="email">Email.</param>
        /// <param name="phone">Phone.</param>
        /// <param name="country">Country.</param>
        /// <param name="title">Title.</param>
        [HttpPost]
        [Route("send-advertisement-info")]
        public IActionResult SendAdvertisementInfo([FromQuery] string firstName,
                                         [FromQuery] string lastName,
                                         [FromQuery] string company,
                                         [FromQuery] string email,
                                         [FromQuery] string phone,
                                         [FromQuery] string country,
                                         [FromQuery] string title)
        {
            if (ModelState.IsValid)
            {
                return Ok(_comService.SendAdvertisementInfo(firstName, lastName, company, email, phone, country, title));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
