using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore_Swagger_Prototype.Models.DTO.Demo;
using NetCore_Swagger_Prototype.Models.Prototype.Demo;
using Microsoft.AspNetCore.Http;

#pragma warning disable CS1587 // XML 註解沒有放置在有效的語言項目前
/// <summary>
///     Demo 如何撰寫註解進而產生文件
/// </summary>
#pragma warning restore CS1587 // XML 註解沒有放置在有效的語言項目前
namespace NetCore_Swagger_Prototype.Controllers.Demo
{
    //  Supply responses content type - 支援的回應類型
    [Produces("application/json")]
    //  Api route - API路由
    [Route("Demo/api/[controller]")]
    [ApiController]
    public class DemoCrudsController : ControllerBase
    {
        #region Default

        private readonly DemoContext _context;

        public DemoCrudsController(DemoContext context)
        {
            _context = context;
        }

        #endregion

        #region Create(Post)
        /// <summary>
        ///  建立項目
        /// </summary>
        /// <remarks>
        /// 範例:
        ///
        ///     Post /Demo/api​​/DemoCruds​/
        ///     {
        ///         "name": "string",
        ///         "isComplete": true
        ///     }
        /// </remarks>
        /// <param name="DemoCrudDto"></param>
        /// <returns>新建的項目</returns>
        /// <response code="201">回傳新建的項目</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<DemoCrudDto>> CreateItem(DemoCrudDto DemoCrudDto)
        {
            var todoItem = new DemoCrud
            {
                IsComplete = DemoCrudDto.IsComplete,
                Name = DemoCrudDto.Name
            };

            _context.DemoCrud.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));
        }
        #endregion

        #region Read(Get)
        /// <summary>
        ///     取得項目
        /// </summary>
        /// <param name="id">目標資料ID</param>
        /// <returns></returns>
        /// <response code="200">回傳項目</response>
        /// <response code="404">找不到項目</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<DemoCrudDto>> GetTodoItem(long id)
        {
            var todoItem = await _context.DemoCrud.FindAsync(id);

            if(todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }
        #endregion

        #region Read(Get)

        /// <summary>
        ///     取得所有資料
        /// </summary>
        /// <returns>回傳List清單</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DemoCrudDto>>> GetDemoCrud()
        {
            return await _context.DemoCrud
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }
        #endregion

        #region Update(Put)

        /// <summary>
        ///     資料更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="DemoCrudDto"></param>
        /// <returns></returns>
        /// <response code="204">更新成功</response>
        /// <response code="400">請求失敗</response>
        /// <response code="404">找不到資料</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateTodoItem(long id, DemoCrudDto DemoCrudDto)
        {
            if(id != DemoCrudDto.Id)
            {
                return BadRequest();
            }

            var todoItem = await _context.DemoCrud.FindAsync(id);
            if(todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = DemoCrudDto.Name;
            todoItem.IsComplete = DemoCrudDto.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) when(!DemoCrudExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        #endregion

        #region Delete
        /// <summary>
        ///  刪除特定資料
        /// </summary>
        /// <remarks>
        /// 範例:
        ///
        ///     Delet /api​/Demo​/DemoCruds​/{id}
        /// </remarks>
        /// <param name="id">資料ID</param>    
        /// <returns>A delete Item</returns>
        /// <response code="204">請輸入參數</response>
        /// <response code="404">目標資料不存在</response>            
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DemoDeleteItem(long id)
        {
            var demoDeleteItem = await _context.DemoCrud.FindAsync(id);

            if(demoDeleteItem == null)
            {
                return NotFound();
            }

            _context.DemoCrud.Remove(demoDeleteItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Private

        private bool DemoCrudExists(long id)
        {
            return _context.DemoCrud.Any(e => e.Id == id);
        }

        private static DemoCrudDto ItemToDTO(DemoCrud todoItem) =>
            new DemoCrudDto
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
        #endregion
    }
}
