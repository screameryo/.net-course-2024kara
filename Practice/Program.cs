using BankSystem.App.Services;
using BankSystem.Domain.Models;
using System.Diagnostics;

namespace Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PracticeOneRefValType();

            PracticeTwoListDict();

            Console.ReadKey();
        }

        private static void ChangeContract(ref Employee employee)
        {
            employee.Contract = $"Contract to {DateTime.Now.AddMonths(36).ToString("d")}";
        }

        private static void ChangeCurrency(ref Currency currency)
        {
            currency.NumCode = "840";
        }

        private static void PracticeOneRefValType()
        {
            var person = new Person() { FName = "John", LName = "Doe", BDate = new DateOnly(2020, 1, 1), PassportNumber = "56564", PassportSeries = "AA", Telephone = "77712345", Address = "str. 1" };
            Console.WriteLine($"Creating a person object {person.FName} {person.LName}{Environment.NewLine}");

            var employee = new Employee() { FName = "John", LName = "Doe", Address = "str. 1", BDate = new DateOnly(2020, 1, 1), PassportNumber = "56564", PassportSeries = "AA", Telephone = "77712345" };
            employee.Position = "Backend";
            employee.Salary = 100000;
            employee.Department = "Develop";
            Console.WriteLine($"Creating a employee object {employee.FName} {employee.LName}{Environment.NewLine}");

            employee.Contract = $"Contract to {DateTime.Now.AddMonths(24).ToString("d")}";
            Console.WriteLine($"Creating a contract for the employee {employee.Contract}{Environment.NewLine}");

            ChangeContract(ref employee);
            Console.WriteLine($"Changing the contract of the employee {employee.Contract}{Environment.NewLine}");

            var currency = new Currency();//840
            currency.Name = "US Dollar";
            currency.Symbol = "$";
            currency.NumCode = "830";
            Console.WriteLine($"Creating a currency object {currency.Name} with Code: {currency.NumCode} Symbol: {currency.Symbol} NumCode: {currency.NumCode}{Environment.NewLine}");

            ChangeCurrency(ref currency);
            Console.WriteLine($"Changing the NumCode of the currency to {currency.NumCode}{Environment.NewLine}");
            Console.WriteLine($"Object currency is {currency.Name} with Code: {currency.NumCode} Symbol: {currency.Symbol} NumCode: {currency.NumCode}{Environment.NewLine}");

            List<Employee> employees = new List<Employee>
            {
                new Employee() { FName = "John", LName = "Doe", Position = "Owner", Salary = 1000000 },
                new Employee() { FName = "Jane", LName = "Doe", Position = "Owner", Salary = 500000 },
                new Employee() { FName = "Janes", LName = "Doe", Position = "Backend", Salary = 100000 },
            };

            var bankService = new BankService();
            var ownerSalary = bankService.CalculateOwnerSalary(1000000, 500000, employees);
            Console.WriteLine($"Owner salary is {ownerSalary}{Environment.NewLine}");

            var client = new Client() { FName = "John", LName = "Doe", BDate = new DateOnly(2020, 1, 1), PassportSeries = "AA", PassportNumber = "14124", Address = "str. 1", Telephone = "123456789" };
            Console.WriteLine($"Creating a client object {client.FName} {client.LName}{Environment.NewLine}");
            var newemployee = bankService.ConvertClientToEmployee(client, "Backend", "Develop", 100000);
            Console.WriteLine($"Client now is jbject employee {Environment.NewLine}");
        }

        private static void PracticeTwoListDict()
        {
            var testDataGenerator = new TestDataGenerator();
            List<Employee> employeesList = testDataGenerator.GenerateEmployee();
            Console.WriteLine($"Generating 10000 fake employee data{Environment.NewLine}");

            List<Client> clientsList = testDataGenerator.GenerateClient();
            Console.WriteLine($"Generating 10000 fake client data{Environment.NewLine}");

            Dictionary<string, Client> clientsDictionary = testDataGenerator.GenerateClientAsDictionary();
            Console.WriteLine($"Generating 10000 fake client data as dictionary{Environment.NewLine}");

            Stopwatch watch = new Stopwatch();

            string findtelephoneforList = clientsList[new Random().Next(0, 1000)].Telephone;
            string findtelephonefordictionary = clientsDictionary.ElementAt(new Random().Next(0, 1000)).Key;

            watch.Restart();
            var clientSearch = clientsList.FirstOrDefault(x => x.Telephone == findtelephoneforList);
            watch.Stop();
            Console.WriteLine($"Search client by num telephone in list clients: {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks){Environment.NewLine}");

            watch.Restart();
            var clientSearched = clientsDictionary[findtelephonefordictionary];
            watch.Stop();
            Console.WriteLine($"Search client by num telephone in dictionary clientsDictionary: {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks){Environment.NewLine}");

            watch.Restart();
            var clientsAge = clientsList.Where(x => x.BDate.Year > DateTime.Now.Year - 30);
            watch.Stop();
            Console.WriteLine($"Search clients where age < 30: {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks). Counts: {clientsAge.Count()}{Environment.NewLine}");

            watch.Restart();
            var employeeMinSalary = employeesList.OrderBy(x => x.Salary).FirstOrDefault();
            watch.Stop();
            Console.WriteLine($"Search employee with min salary: {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks). {Environment.NewLine}");

            watch.Restart();
            var clientSearchDictionaryLastOrDefault = clientsDictionary.LastOrDefault();
            watch.Stop();
            Console.WriteLine($"Search client in clientsDictionary by lastOrDefault: {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks){Environment.NewLine}");

            watch.Restart();
            var clientsearchbykey = clientsDictionary[findtelephonefordictionary];
            watch.Stop();
            Console.WriteLine($"Search client in clientsDictionary by key: {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks){Environment.NewLine}");
        }
    }
}
