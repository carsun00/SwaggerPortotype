using Microsoft.AspNetCore.Mvc;

namespace NetCore_Swagger_Prototype.Controllers.Test
{
    [Route("api/SystemStatus/[controller]")]
    [ApiController]
    public class SystemStatusController : ControllerBase
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
