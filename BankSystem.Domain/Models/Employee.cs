namespace BankSystem.Domain.Models
{
    public class Employee : Person
    {
        public string Position { get; set; }

        public int Salary { get; set; }

        public string Department { get; set; }

        public string Contract { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Employee e = (Employee)obj;
            return FName == e.FName 
                && LName == e.LName 
                && MName == e.MName 
                && BDate == e.BDate 
                && PassportNumber == e.PassportNumber
                && PassportSeries == e.PassportSeries
                && Telephone == e.Telephone 
                && Address == e.Address 
                && Position == e.Position
                && Salary == e.Salary 
                && Department == e.Department
                && Contract == e.Contract;
        }

        public override int GetHashCode()
        {
            return FName.GetHashCode() 
                + LName.GetHashCode() 
                + BDate.GetHashCode() 
                + PassportNumber.GetHashCode() 
                + PassportSeries.GetHashCode() 
                + Telephone.GetHashCode() 
                + Address.GetHashCode() 
                + Position.GetHashCode() 
                + Salary.GetHashCode() 
                + Department.GetHashCode() 
                + Contract.GetHashCode();
        }
    }
}
