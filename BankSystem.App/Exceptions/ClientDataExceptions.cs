namespace BankSystem.App.Exceptions
{
    public class ClientDataExceptions : Exception
    {
        public static readonly string UnderageClientMessage = "Клиент должен быть старше 18 лет.";
        public static readonly string NoPassportDataMessage = "У клиента отсутствуют паспортные данные.";
        public static readonly string NoClientMessage = "Клиент не найден.";
        public static readonly string ClientAlreadyExistsMessage = "Клиент уже существует.";

        public ClientDataExceptions(string message) : base(message)
        {
        }
    }
}
