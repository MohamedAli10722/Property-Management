namespace Properties_Creation_Management.DTOs
{
    public class AddEmployeeDto
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public List<EmployeePropertyInputDto> Properties { get; set; }
    }
}
