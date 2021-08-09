using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DotNetCoreFunc31
{
    public  static class Employee
    {
        public static List<string> firstName = new List<string>() { };
        public static List<string> lastName = new List<string>() { };

        public static List<string> CompleteAddress = new List<string>() { };
        public static List<string> Email = new List<string>() { };
        public static List<string> Phone = new List<string>() { };

        public static void Add(string FullName, string CompleteAddress, string Email)
        {
            string[] fn = FullName.Split(' ');

            string em;
            if (!Email.Contains(','))
            {
                em = Email.Insert(0, "Null ,");
            }
            else
            {
                em = Email;
            }
            string[] fn1 =  em.Split(',');
            Employee.firstName.Add(fn[0]);
            Employee.lastName.Add(fn[1]);
            Employee.CompleteAddress.Add(CompleteAddress);
            Employee.Email?.Add(fn1[1]);
            Employee.Phone?.Add(fn1[0]);

        }
        public static List<string> Show()
        {
           
            
            //foreach (string i in Employee.fullName)
            //{
            //    str.Append(i);
            //}
            return Employee.CompleteAddress;
        }

    }
}
