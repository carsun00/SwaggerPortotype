using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/// <summary>
///     原生ProducesResponseType的功能展示
/// </summary>
namespace NetCore_Swagger_Prototype.Controllers.Demo
{
    [Route("Demo/api/[controller]")]
    [ApiController]
    public class ProducesResponseTypeController : ControllerBase
    {
        /// <summary>
        ///     ResponseType 範例
        /// </summary>
        /// <param name="StatusCodes">Http狀態碼</param>
        /// <returns>HttpStatus</returns>
        /// <remarks>注意事項</remarks>
        [HttpGet("{StatusCodes}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult BasicHttpStatus(int StatusCodes)
        {
            switch(StatusCodes)
            {
                case 200:
                    return Ok();
                case 201:
                    return Created("Demo", "Data Create.");
                case 400:
                    return BadRequest();
                case 500: 
                default:
                    return BadRequest();
                    //  其他狀態以此類推
            }
        }
    }
}
