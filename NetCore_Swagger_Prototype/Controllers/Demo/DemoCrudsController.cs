using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore_Swagger_Prototype.Models.DTO.Demo;
using NetCore_Swagger_Prototype.Models.Prototype.Demo;

namespace NetCore_Swagger_Prototype.Controllers.Demo
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoCrudsController : ControllerBase
    {
        private readonly DemoContext _context;

        public DemoCrudsController(DemoContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<DemoCrudDto>>> GetDemoCrud()
        {
            return await _context.DemoCrud
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DemoCrudDto>> GetTodoItem(long id)
        {
            var todoItem = await _context.DemoCrud.FindAsync(id);

            if(todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }

        [HttpPut("{id}")]
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




        [HttpPost]
        public async Task<ActionResult<DemoCrudDto>> CreateTodoItem(DemoCrudDto DemoCrudDto)
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

        /// <summary>
        /// 刪除特定資料
        /// </summary>
        /// <param name="id">資料ID</param>    
        [HttpDelete("{id}")]
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
    }
}
