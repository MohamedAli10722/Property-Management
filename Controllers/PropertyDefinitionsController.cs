using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Properties_Creation_Management.DTOs;
using Properties_Creation_Management.Enum;
using Properties_Creation_Management.Models;

namespace Properties_Creation_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyDefinitionsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public PropertyDefinitionsController(AppDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddProperty(AddPropertyDefinitionDto dto)
        {
            var property = new PropertyDefinition
            {
                Name = dto.Name,
                Type = dto.Type,
                IsRequired = dto.IsRequired
            };

            if (dto.Type == PropertyType.Dropdown && dto.DropdownOptions != null)
            {
                property.DropdownOptions = dto.DropdownOptions
                    .Select(val => new DropdownOption { Value = val })
                    .ToList();
            }

            _context.PropertyDefinitions.Add(property);
            await _context.SaveChangesAsync();

            return Ok(property);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var properties = await _context.PropertyDefinitions
                .Include(p => p.DropdownOptions)
                .ToListAsync();

            return Ok(properties);
        }
    }
}
