using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore_Swagger_Prototype.Models.DTO.Demo;
using Swashbuckle.AspNetCore.Annotations;

/// <summary>
///     需安裝套件
///     Swashbuckle.AspNetCore.Annotations
/// </summary>
namespace NetCore_Swagger_Prototype.Controllers.Demo
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoSwaggerResponseController : ControllerBase
    {   /// <summary>
        ///     SwaggerResponse 範例
        ///     SwaggerResponse中的文字
        ///     與回應的ProducesResponse.Status是不同的用途。
        /// </summary>
        /// <param name="StatusCodes">Http狀態碼</param>
        /// <returns>HttpStatus</returns>
        /// <remarks>注意事項</remarks>
        [HttpGet("{StatusCodes}")]
        [SwaggerResponse(StatusCodes.Status200OK, "成功", typeof(ProducesResponse))]
        [SwaggerResponse(StatusCodes.Status201Created, "新建成功", typeof(ProducesResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "錯誤請求", typeof(ProducesResponse))]
        public IActionResult BasicHttpStatus(int StatusCodes)
        {
            ProducesResponse response = new ProducesResponse
            {
                StatusCode = StatusCodes,
                Status = "Demo"
            };

            switch(StatusCodes)
            {
                case 200:
                    response.Status = "正常";
                    return Ok(response);

                case 201:
                    response.Status = "新建";
                    return Created("Demo", response);
                case 400:
                    response.Status = "錯誤";
                    return BadRequest(response);
                case 500:
                default:
                    response.Status = "未設定狀況";
                    return BadRequest();
                    //  其他狀態以此類推
            }
        }
    }
}
