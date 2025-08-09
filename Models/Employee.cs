namespace Properties_Creation_Management.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<EmployeePropertyValue> PropertyValues { get; set; }
    }
}
