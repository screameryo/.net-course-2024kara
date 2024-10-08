using BankSystem.Domain.Models;

namespace BankSystem.App.Services
{
    public class BankService
    {
        private readonly List<Person> _blackList = new List<Person>();

        public double CalculateOwnerSalary(double profit, double expenses, List<Employee> employees)
        {
            double clearProfit = profit - expenses;

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
                PassportNumber = client.PassportNumber,
                PassportSeries = client.PassportSeries,
                Address = client.Address,
                Telephone = client.Telephone,
                Position = position,
                Department = department,
                Salary = salary
            };
        }

        public void AddBonus<T>(T person, string bonus) where T : Person
        {
            person.Bonuses.Add(bonus);
        }

        public void AddToBlackList<T>(T person) where T : Person
        {
            _blackList.Add(person);
        }

        public bool IsPersonInBlackList<T>(T person) where T : Person
        {
            return _blackList.Contains(person);
        }
    }
}
