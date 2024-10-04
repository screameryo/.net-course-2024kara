namespace BankSystem.App.Exceptions
{
    public class AccountDataExceptions : Exception
    {
        public static readonly string NoAccountDataMessage = "У клиента отсутствуют данные о счете.";
        public static readonly string AccountIsNullMessage = "Лицевой счёт не указан.";
        public static readonly string AccountBalanceLessThanZeroMessage = "Сумма на лицевом счете меньше 0.";

        public AccountDataExceptions(string message) : base(message)
        {
        }
    }
}
