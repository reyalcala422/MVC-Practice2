using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPractice2.Data;
using MyPractice2.DTO.Color;
using MyPractice2.Model;

namespace MyPractice2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly APIDbContext db;

        public ColorController(APIDbContext context) { 
        db=context;
        }

        public async Task<IActionResult> Getall() {
            var color = await db.Colors
            .Select(e => new ColorDTO {
            Id=e.Id,
            ColorName=e.ColorName,
            }).ToListAsync();
        return Ok(color);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateColorDTO dto) {
            var color = new Color
            {
                ColorName = dto.ColorName
            };
            db.Colors.Add(color);
            await db.SaveChangesAsync();
            return Ok(color);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateColorDTO dto) {
            var color = await db.Colors
            .FindAsync(id);
            if (color == null) {
                return NotFound("Color not found");
            }

            var updatedcolor = new {
            color=dto.ColorName
            };

            color.ColorName = dto.ColorName;
            await db.SaveChangesAsync();
            return Ok(new {
            Message="Color Updated",
            Data= updatedcolor
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var color =  db.Colors.Find(id);
            if (color == null)
            {
                return NotFound("Color not found");
            }
            var deletedColor = new {
            color.Id,
            color.ColorName
            };
            db.Colors.Remove(color);
            await db.SaveChangesAsync();
            return Ok(new {
            Message="Deleted",
            Data= deletedColor
            });
        }
    }
}
