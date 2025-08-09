using Properties_Creation_Management.Enum;

namespace Properties_Creation_Management.Models
{
    public class PropertyDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PropertyType Type { get; set; }
        public bool IsRequired { get; set; }

        public ICollection<DropdownOption> DropdownOptions { get; set; }
    }
}
