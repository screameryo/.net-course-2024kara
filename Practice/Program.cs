using BankSystem.App.Services;
using BankSystem.Domain.Models;
using System.Diagnostics;

namespace Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a person object
            var person = new Person("John", "Doe", new DateOnly(2020, 1, 1), "PMR56564", "str. 1", "123456789", null);
            Console.WriteLine($"Creating a person object {person.GetFullName()}{Environment.NewLine}");

            //Create a employee object with no contract
            var employee = new Employee("Janes", "Doe", new DateOnly(2020, 1, 1), "PMR56564", "str. 1", "HR", 1000, "HR", "77712345");
            employee.Position = "Backend";
            employee.Salary = 100000;
            employee.Department = "Develop";
            Console.WriteLine($"Creating a employee object {employee.GetEmployeeInfo()}{Environment.NewLine}");

            //Create employee contract
            employee.Contract = $"Contract to {DateTime.Now.AddMonths(24).ToString("d")}";
            Console.WriteLine($"Creating a contract for the employee {employee.GetContract()}{Environment.NewLine}");

            //Change employee contract
            ChangeContract(ref employee);
            Console.WriteLine($"Changing the contract of the employee {employee.Contract}{Environment.NewLine}");
            Console.WriteLine($"Object employee is {employee.GetEmployeeInfo()}{Environment.NewLine}");

            //Create currency object
            var currency = new Currency("US Dollar", "USD", "$", 830);//840
            Console.WriteLine($"Creating a currency object {currency.Name} with Code: {currency.Code} Symbol: {currency.Symbol} NumCode: {currency.NumCode}{Environment.NewLine}");

            //Change currency NumCode
            ChangeCurrency(ref currency);
            Console.WriteLine($"Changing the NumCode of the currency to {currency.NumCode}{Environment.NewLine}");
            Console.WriteLine($"Object currency is {currency.Name} with Code: {currency.Code} Symbol: {currency.Symbol} NumCode: {currency.NumCode}{Environment.NewLine}");

            //Create a List of employees with positions Owner
            List<Employee> employees = new List<Employee>
            {
                new Employee("John", "Doe", new DateOnly(2020, 1, 1), "PMR56564", "str. 1", "Owner", 1000, "backend", "77712345", "Contract to 12.12.2024"),
                new Employee("Jane", "Doe", new DateOnly(2020, 1, 1), "PMR56564", "str. 1", "Owner", 1000, "backend", "77712345", "Contract to 12.12.2024"),
                new Employee("Jason", "Doe", new DateOnly(2020, 1, 1), "PMR56564", "str. 1", "Owner", 1000, "backend", "77712345", "Contract to 12.12.2024")
            };

            //Get the owner salary
            var bankService = new BankService();
            var ownerSalary = bankService.CalculateOwnerSalary(1000000, 500000, employees);
            Console.WriteLine($"Owner salary is {ownerSalary}{Environment.NewLine}");

            //Create a client object
            var client = new Client("John", "Doe", new DateOnly(2020, 1, 1), "PMR56564", "str. 1", "123456789", null);
            Console.WriteLine($"Creating a client object {client.GetFullName()}{Environment.NewLine}");
            var newemployee = bankService.ConvertClientToEmployee(client, "Backend", "Develop", 100000);
            Console.WriteLine($"Client now is jbject employee is {newemployee.GetEmployeeInfo()}{Environment.NewLine}");

            int generatednum = 10000;
            Stopwatch watch = new Stopwatch();

            //generate 10000 fake client data
            var clients = new List<Client>();
            var testDataGenerator = new TestDataGenerator();
            testDataGenerator.GenerateClient(generatednum, ref clients);
            Console.WriteLine($"Generate 10000 fake client data{Environment.NewLine}");

            //Create Dictionary with key telephone and value Client
            watch.Start();
            var clientsDictionary = clients.ToDictionary(x => x.Telephone);
            watch.Stop();
            Console.WriteLine($"Create Dictionary with key telephone and value Client from List Clients. Time of convert {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks)");           

            //generate 10000 fake employee data
            employees = new List<Employee>();            
            testDataGenerator.GenerateEmployee(generatednum, ref employees);
            Console.WriteLine($"Generate 10000 fake employee data{Environment.NewLine}");

            //Get existing client telephone from list clients by position 9578
            string findtelephone = clients[new Random().Next(0, generatednum)].Telephone;
            
            //searcg client by num telephone in list clients
            watch.Restart();
            var clientSearch = clients.FirstOrDefault(x => x.Telephone == findtelephone);
            watch.Stop();
            Console.WriteLine($"Search client by num telephone in list clients: {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks){Environment.NewLine}");

            //search client by num telephone in dictionary clientsDictionary
            watch.Restart();
            var clientSearchDictionary = clientsDictionary[findtelephone];
            watch.Stop();
            Console.WriteLine($"Search client by num telephone in dictionary clientsDictionary: {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks){Environment.NewLine}");

            //search clients where age < 30
            watch.Restart();
            var clientsAge = clients.Where(x => x.GetAge() < 30).ToList();
            watch.Stop();
            Console.WriteLine($"Search clients where age < 30: {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks). Counts: {clientsAge.Count}{Environment.NewLine}");

            //search employee with min salary
            watch.Restart();
            var employeeMinSalary = employees.OrderBy(x => x.Salary).FirstOrDefault();
            watch.Stop();
            Console.WriteLine($"Search employee with min salary: {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks). {employeeMinSalary.GetEmployeeInfo()}{Environment.NewLine}");

            string lasttelephone = clients.Last().Telephone;
            //search client in clientsDictionary by firstorddefault
            watch.Restart();
            var clientSearchDictionaryFirstOrDefault = clientsDictionary.FirstOrDefault(x => x.Key == lasttelephone).Value;
            watch.Stop();
            Console.WriteLine($"Search client in clientsDictionary by firstorddefault: {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks){Environment.NewLine}");

            //search client in clientsDictionary by key
            watch.Restart();
            var clientSearchDictionaryKey = clientsDictionary[lasttelephone];
            watch.Stop();
            Console.WriteLine($"Search client in clientsDictionary by key: {watch.ElapsedMilliseconds} ms ({watch.ElapsedTicks} ticks){Environment.NewLine}");

            Console.ReadKey();
        }

        //Change the contract of the employee
        private static void ChangeContract(ref Employee employee)
        {
            employee.Contract = $"Contract to {DateTime.Now.AddMonths(36).ToString("d")}";
        }

        //Change the NumCode of the currency
        private static void ChangeCurrency(ref Currency currency)
        {
            currency.NumCode = 840;
        }
    }
}
