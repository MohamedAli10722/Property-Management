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
    public class EmployeesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public EmployeesController(AppDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeDto dto)
        {
            var employee = new Employee
            {
                Code = dto.Code,
                Name = dto.Name,
                PropertyValues = new List<EmployeePropertyValue>()
            };

            foreach (var input in dto.Properties)
            {
                var definition = await _context.PropertyDefinitions
                    .Include(d => d.DropdownOptions)
                    .FirstOrDefaultAsync(d => d.Id == input.PropertyDefinitionId);

                if (definition == null)
                    return BadRequest($"Invalid property id: {input.PropertyDefinitionId}");

                if (definition.IsRequired && string.IsNullOrEmpty(input.Value))
                    return BadRequest($"{definition.Name} is required.");

                if (definition.Type == PropertyType.Integer && !int.TryParse(input.Value, out _))
                    return BadRequest($"{definition.Name} must be an integer.");

                if (definition.Type == PropertyType.Date && !DateTime.TryParse(input.Value, out _))
                    return BadRequest($"{definition.Name} must be a date.");

                if (definition.Type == PropertyType.Dropdown &&
                    !definition.DropdownOptions.Any(opt => opt.Value == input.Value))
                    return BadRequest($"{input.Value} is not a valid option for {definition.Name}.");

                employee.PropertyValues.Add(new EmployeePropertyValue
                {
                    PropertyDefinitionId = input.PropertyDefinitionId,
                    Value = input.Value
                });
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                employee.Id,
                employee.Code,
                employee.Name,
                Properties = employee.PropertyValues.Select(pv => new
                {
                    pv.PropertyDefinitionId,
                    PropertyName = _context.PropertyDefinitions.First(d => d.Id == pv.PropertyDefinitionId).Name,
                    pv.Value
                }).ToList()
            });

        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var propertyDefinitions = await _context.PropertyDefinitions.ToListAsync();

            var employees = await _context.Employees
                .Include(e => e.PropertyValues)
                    .ThenInclude(v => v.PropertyDefinition)
                .ToListAsync();

            var result = employees.Select(e => new
            {
                e.Id,
                e.Code,
                e.Name,
                Properties = propertyDefinitions.Select(pd =>
                {
                    var value = e.PropertyValues.FirstOrDefault(v => v.PropertyDefinitionId == pd.Id)?.Value;
                    return new
                    {
                        Name = pd.Name,
                        Type = pd.Type,
                        Value = value 
                    };
                }).ToList()
            });

            return Ok(result);
        }
    }
}
