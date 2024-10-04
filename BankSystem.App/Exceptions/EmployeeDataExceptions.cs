namespace BankSystem.App.Exceptions
{
    public class EmployeeDataExceptions : Exception
    {
        public static readonly string UnderageEmployeeMessage = "Сотрудник должен быть старше 18 лет.";
        public static readonly string NoPassportDataMessage = "У сотрудника отсутствуют паспортные данные.";
        public static readonly string NoEmployeeMessage = "Сотрудник не найден.";
        public static readonly string EmployeeAlreadyExistsMessage = "Сотрудник уже существует.";

        public EmployeeDataExceptions(string message) : base(message)
        {
        }
    }
}
