namespace Properties_Creation_Management.Models
{
    public class DropdownOption
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int PropertyDefinitionId { get; set; }

        public PropertyDefinition PropertyDefinition { get; set; }
    }
}
