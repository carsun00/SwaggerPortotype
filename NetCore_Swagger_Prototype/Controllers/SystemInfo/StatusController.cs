using Microsoft.AspNetCore.Mvc;

namespace NetCore_Swagger_Prototype.Controllers.SystemInfo
{
    [Route("api/SystemInfo/Status/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        /// <summary>
        ///     Supply clinet Test Server Status
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string ApiServericStatus()
        {
            return "Server is Working.";
        }
    }
}
