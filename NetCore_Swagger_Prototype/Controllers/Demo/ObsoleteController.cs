using System;
using Microsoft.AspNetCore.Mvc;

namespace NetCore_Swagger_Prototype.Controllers.Demo
{
    [Route("Demo/api/[controller]")]
    [ApiController]
    public class ObsoleteController : ControllerBase
    {
        /// <summary>
        ///     過時語法註記展示
        /// </summary>
        /// <returns>String</returns>
        [Obsolete("此方法已過時，請使用 /xxx/yyy/{id}")]
        [HttpGet]
        public string ApiServericStatus()
        {
            return "Some message";
        }
    }
}
