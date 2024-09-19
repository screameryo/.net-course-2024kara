using Models;

namespace PracticeWithTypes
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create a person object
            var person = new Person("John", "Doe", new DateOnly(2020, 1, 1));
            Console.WriteLine($"Creating a person object {person.GetFullName()}{Environment.NewLine}");

            //Create a employee object with no contract
            var employee = new Employee("Jane", "Doe", new DateOnly(2020, 1, 1));
            employee.Position = "Backend";
            employee.Salary = 100000;
            employee.Department = "Develop";
            Console.WriteLine($"Creating a employee object {employee.GetEmployeeInfo()}{Environment.NewLine}");

            //Create employee contract
            employee.Contract = $"Contract to {DateTime.Now.AddMonths(24).ToString("d")}";
            Console.WriteLine($"Creating a contract for the employee {employee.GetContract()}{Environment.NewLine}");

            //Change employee contract
            ChangeContract(ref employee);
            Console.WriteLine($"Changing the contract of the employee to {employee.Contract}{Environment.NewLine}");
            Console.WriteLine($"Object employee is {employee.GetEmployeeInfo()}{Environment.NewLine}");

            //Create currency object
            var currency = new Currency("US Dollar", "USD", "$", 830);//840
            Console.WriteLine($"Creating a currency object {currency.Name} with Code: {currency.Code} Symbol: {currency.Symbol} NumCode: {currency.NumCode}{Environment.NewLine}");

            //Change currency NumCode
            ChangeCurrency(ref currency);
            Console.WriteLine($"Changing the NumCode of the currency to {currency.NumCode}{Environment.NewLine}");
            Console.WriteLine($"Object currency is {currency.Name} with Code: {currency.Code} Symbol: {currency.Symbol} NumCode: {currency.NumCode}{Environment.NewLine}");
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
