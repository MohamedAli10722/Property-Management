namespace Properties_Creation_Management.Models
{
    public class EmployeePropertyValue
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int PropertyDefinitionId { get; set; }

        public string Value { get; set; }  // Always stored as string for flexibility

        public Employee Employee { get; set; }
        public PropertyDefinition PropertyDefinition { get; set; }
    }
}
