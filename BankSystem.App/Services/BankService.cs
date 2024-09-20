using BankSystem.Domain.Models;

namespace BankSystem.App.Services
{
    public class BankService
    {
        /// <summary>
        /// Calculate the owner's salary based on the profit, expenses, and employees
        /// </summary>
        /// <param name="profit"></param>
        /// <param name="expenses"></param>
        /// <param name="employees"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert a client to an employee
        /// </summary>
        /// <param name="client"></param>
        /// <param name="position"></param>
        /// <param name="department"></param>
        /// <param name="salary"></param>
        /// <returns></returns>
        public Employee ConvertClientToEmployee(Client client, string position, string department, int salary)
        {
            return new Employee(client.FName, client.LName, client.BDate, client.Passport, client.Address, position, salary, department, client.Telephone, null, client.MName);
        }
    }
}
