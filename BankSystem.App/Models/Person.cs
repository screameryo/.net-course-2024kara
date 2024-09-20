namespace Models
{
    public class Person
    {
        public Person(string FName, string LName, DateOnly BDate, string? MName = null)
        {
            this.FName = FName;
            this.LName = LName;
            this.MName = MName;
            this.BDate = BDate;
        }

        /// <summary>
        /// First Name of the person
        /// </summary>
        public string FName { get; set; }

        /// <summary>
        /// Last Name of the person
        /// </summary>
        public string LName { get; set; }

        /// <summary>
        /// Middle Name of the person (optional)
        /// </summary>
        public string? MName { get; set; }

        /// <summary>
        /// Birth Date of the person
        /// </summary>
        public DateOnly BDate { get; set; }

        /// <summary>
        /// Passport of the person
        /// </summary>
        public string Passport { get; set; }

        /// <summary>
        /// Telephone of the person
        /// </summary>
        public string? Telephone { get; set; }

        /// <summary>
        /// Adress of the person
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Get the full name of the person
        /// </summary>
        /// <returns>The full name of the person</returns>
        public string GetFullName()
        {
            if (MName != null)
            {
                return $"{FName} {MName} {LName}";
            }
            else
            {
                return $"{FName} {LName}";
            }
        }

        /// <summary>
        /// Get the age of the person
        /// </summary>
        /// <returns>The age of the person</returns>
        public int GetAge()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - BDate.Year;
            if (BDate > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
}
