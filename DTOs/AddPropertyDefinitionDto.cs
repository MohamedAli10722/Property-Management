using Properties_Creation_Management.Enum;

namespace Properties_Creation_Management.DTOs
{
    public class AddPropertyDefinitionDto
    {
        public string Name { get; set; }
        public PropertyType Type { get; set; }
        public bool IsRequired { get; set; }
        public List<string> DropdownOptions { get; set; }  // Only for Dropdown
    }
}
