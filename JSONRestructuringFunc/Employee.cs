using System;
using System.Collections.Generic;
using System.Text;

namespace JSONRestructuringFunc
{
    public class Employee
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public int Age { get; set; }
        // public string Country { get; set; }
    }

    public class Employees
    {
        public static List<Employee> employees { get; set; } = new List<Employee>();
    }

}
