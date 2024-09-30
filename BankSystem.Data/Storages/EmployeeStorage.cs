using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
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
    }
}
