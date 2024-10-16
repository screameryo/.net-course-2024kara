namespace BankSystem.Domain.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string? MName { get; set; }
        public DateOnly BDate { get; set; }
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public string GetFullName()
        {
            return $"{FName} {LName} {MName}";
        }

        public List<string>? Bonuses { get; set; }
    }
}
