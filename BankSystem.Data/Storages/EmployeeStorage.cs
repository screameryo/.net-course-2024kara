using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public enum EmployeeMethod
    {
        Younger,
        Older,
        Last
    }
    public class EmployeeStorage
    {
        public List<Employee> employees = new List<Employee>();

        public void AddEmployee(Employee newEmployee)
        {
            if (newEmployee == null)
            {
                throw new ArgumentNullException(nameof(newEmployee), "Сотрудник не может быть null.");
            }

            employees.Add(newEmployee);
        }

        public void AddManyEmployees(List<Employee> newEmployees)
        {
            if (newEmployees == null)
            {
                throw new ArgumentNullException(nameof(newEmployees), "Список сотрудников не может быть null.");
            }

            employees.AddRange(newEmployees);
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public Employee Get(EmployeeMethod method)
        {
            switch (method)
            {
                case EmployeeMethod.Younger:
                    return employees.OrderBy(e => e.BDate).First();
                case EmployeeMethod.Older:
                    return employees.OrderByDescending(e => e.BDate).First();
                case EmployeeMethod.Last:
                    return employees.Last();
                default:
                    throw new InvalidOperationException("Неверный метод.");
            }
        }

        public int GetAgeAverage()
        {
            return (int)employees.Average(e => DateTime.Now.Year - e.BDate.Year);
        }
    }
}
