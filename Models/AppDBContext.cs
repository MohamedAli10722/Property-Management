using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace Properties_Creation_Management.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<PropertyDefinition> PropertyDefinitions { get; set; }
        public DbSet<DropdownOption> DropdownOptions { get; set; }
        public DbSet<EmployeePropertyValue> EmployeePropertyValues { get; set; }
    }
}
