using Microsoft.AspNetCore.Mvc;

namespace NetCore_Swagger_Prototype.Controllers.SystemInfo
{
    /// <summary>
    ///     Supply clinet Test Server Status
    /// </summary>
    [Route("api/SystemInfo/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        /// <summary>
        ///     Test Server Status
        /// </summary>
        /// <returns>A message about system status.</returns>
        [HttpGet]
        public string ApiServericStatus()
        {
            return "Server is Working.";
        }
    }
}
