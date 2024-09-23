using BankSystem.Domain.Models;

namespace BankSystem.App.Services
{
    public class BankService
    {
        public double CalculateOwnerSalary(double profit, double expenses, List<Employee> employees)
        {
            double clearProfit = profit - expenses;

            //Bank owner counts as an employee
            int ownerCount = employees.Count(x => x.Position == "Owner");

            if(ownerCount == 0)
            {
                return 0;
            }
            else
            {
                return Math.Round(clearProfit / ownerCount, 2);
            }
        }

        public Employee ConvertClientToEmployee(Client client, string position, string department, int salary)
        {
            return new Employee()
            {
                FName = client.FName,
                LName = client.LName,
                BDate = client.BDate,
                Passport = client.Passport,
                Address = client.Address,
                Telephone = client.Telephone,
                Position = position,
                Department = department,
                Salary = salary
            };
        }
    }
}
