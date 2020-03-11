using System;
using System.Collections.Generic;
using System.Linq;
using WPF.Models;

namespace WPF.Data
{
    class TestData
    {


        public static List<Department> Departments { get; } = Enumerable.Range(0, 5)
            .Select(i => new Department
            {
                Id = i,
                Name = $"Департамент {i+1}"
            }).ToList();

        public static List<Employee> Employees { get; } = Enumerable.Range(1, 100)
           .Select(i => new Employee
           {
               Id = i,
               Name = $"Имя {i}",
               SurName = $"Фамилия {i}",
               Patronymic = $"Отчество {i}",
               DayOfBirth = DateTime.Now.Subtract(TimeSpan.FromDays(365 / 6 * (i + 18))),
               Department = Departments[1]
           })
           .ToList();
    }
}
