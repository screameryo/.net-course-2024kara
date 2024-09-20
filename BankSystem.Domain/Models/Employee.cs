namespace Models
{
    public class Employee : Person
    {
        public Employee(string FName, string LName, DateOnly BDate, string? MName = null) : base(FName, LName, BDate, MName) { }

        /// <summary>
        /// Position of the employee in the bank
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Salary of the employee in the bank
        /// </summary>
        public int Salary { get; set; }

        /// <summary>
        /// Department of the employee in the bank
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Get the full name, position, and department of the employee
        /// </summary>
        /// <returns></returns>
        public string GetEmployeeInfo()
        {
            return $"{GetFullName()} is a {Position} in the {Department} department with contract \"{GetContract()}\"";
        }

        /// <summary>
        /// Get the contract of the employee
        /// </summary>
        /// <returns></returns>
        public string GetContract()
        {
            return Contract is null ? "No contract" : Contract;
        }

        /// <summary>
        /// Get the full name of the employee
        /// </summary>
        /// <returns>The full name of the client</returns>
        public string GetFullName()
        {
            return base.GetFullName();
        }

        /// <summary>
        /// Get the age of the employee
        /// </summary>
        /// <returns>The age of the client</returns>
        public int GetAge()
        {
            return base.GetAge();
        }

        /// <summary>
        /// Contract of the employee in the bank
        /// </summary>
        public string? Contract { get; set; }
    }
}
