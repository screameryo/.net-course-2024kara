﻿namespace BankSystem.Domain.Models
{
    public class Position
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
