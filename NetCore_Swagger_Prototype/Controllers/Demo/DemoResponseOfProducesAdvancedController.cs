using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore_Swagger_Prototype.Models.DTO.Demo;

/// <summary>
///     原生ProducesResponseType的進階功能展示
///     在回傳狀態中帶入指定的資料
/// </summary>
namespace NetCore_Swagger_Prototype.Controllers.Demo
{
    [Route("Demo/api/[controller]")]
    [ApiController]
    public class DemoResponseOfProducesAdvancedController : ControllerBase
    {
        /// <summary>
        ///     ResponseType 範例
        /// </summary>
        /// <param name="StatusCodes">Http狀態碼</param>
        /// <returns>HttpStatus</returns>
        /// <remarks>注意事項</remarks>
        [HttpGet("{StatusCodes}")]
        [ProducesResponseType(typeof(ProducesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProducesResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProducesResponse), StatusCodes.Status400BadRequest)]
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
                    response.Status = "回應正常";
                    return Ok(response);

                case 201:
                    response.Status = "新建成功";
                    return Created("Demo", response);
                case 400:
                    response.Status = "錯誤請求";
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
