using BankSystem.App.Services;
using BankSystem.Domain.Models;

namespace Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a person object
            var person = new Person() { FName = "John", LName = "Doe", BDate = new DateOnly(2020, 1, 1), Passport = "PMR56564", Telephone = "77712345", Address = "str. 1" };
            Console.WriteLine($"Creating a person object {person.FName} {person.LName}{Environment.NewLine}");

            //Create a employee object with no contract
            var employee = new Employee() { FName = "John", LName = "Doe", Address = "str. 1", BDate = new DateOnly(2020, 1, 1), Passport = "PMR56564", Telephone = "77712345" };
            employee.Position = "Backend";
            employee.Salary = 100000;
            employee.Department = "Develop";
            Console.WriteLine($"Creating a employee object {employee.FName} {employee.LName}{Environment.NewLine}");

            //Create employee contract
            employee.Contract = $"Contract to {DateTime.Now.AddMonths(24).ToString("d")}";
            Console.WriteLine($"Creating a contract for the employee {employee.Contract}{Environment.NewLine}");

            //Change employee contract
            ChangeContract(ref employee);
            Console.WriteLine($"Changing the contract of the employee {employee.Contract}{Environment.NewLine}");

            //Create currency object
            var currency = new Currency();//840
            currency.Name = "US Dollar";
            currency.Code = "USD";
            currency.Symbol = "$";
            currency.NumCode = 830;
            Console.WriteLine($"Creating a currency object {currency.Name} with Code: {currency.Code} Symbol: {currency.Symbol} NumCode: {currency.NumCode}{Environment.NewLine}");

            //Change currency NumCode
            ChangeCurrency(ref currency);
            Console.WriteLine($"Changing the NumCode of the currency to {currency.NumCode}{Environment.NewLine}");
            Console.WriteLine($"Object currency is {currency.Name} with Code: {currency.Code} Symbol: {currency.Symbol} NumCode: {currency.NumCode}{Environment.NewLine}");

            //Create a List of employees with positions Owner
            List<Employee> employees = new List<Employee>
            {
                new Employee() { FName = "John", LName = "Doe", Position = "Owner", Salary = 1000000 },
                new Employee() { FName = "Jane", LName = "Doe", Position = "Owner", Salary = 500000 },
                new Employee() { FName = "Janes", LName = "Doe", Position = "Backend", Salary = 100000 },
            };

            //Get the owner salary
            var bankService = new BankService();
            var ownerSalary = bankService.CalculateOwnerSalary(1000000, 500000, employees);
            Console.WriteLine($"Owner salary is {ownerSalary}{Environment.NewLine}");

            //Create a client object
            var client = new Client() { FName = "John", LName = "Doe", BDate = new DateOnly(2020, 1, 1), Passport = "PMR56564", Address = "str. 1", Telephone = "123456789" };
            Console.WriteLine($"Creating a client object {client.FName} {client.LName}{Environment.NewLine}");
            var newemployee = bankService.ConvertClientToEmployee(client, "Backend", "Develop", 100000);
            Console.WriteLine($"Client now is jbject employee {Environment.NewLine}");
        }

        private static void ChangeContract(ref Employee employee)
        {
            employee.Contract = $"Contract to {DateTime.Now.AddMonths(36).ToString("d")}";
        }

        private static void ChangeCurrency(ref Currency currency)
        {
            currency.NumCode = 840;
        }
    }
}
