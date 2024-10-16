using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityConfigurations
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("employee");

            builder.HasKey(e => e.Id);

            builder.HasIndex(c => new { c.PassportSeries, c.PassportNumber })
                .IsUnique();

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.FName)
                .HasColumnName("f_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.LName)
                .HasColumnName("l_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.MName)
                .HasColumnName("m_name")
                .HasMaxLength(50);

            builder.Property(e => e.BDate)
                .HasColumnName("b_date")
                .HasMaxLength(50);

            builder.Property(e => e.PassportNumber)
                .HasColumnName("passport_number")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.PassportSeries)
                .HasColumnName("passport_series")
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(e => e.Telephone)
                .HasColumnName("telephone")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Address)
                .HasColumnName("address")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.PositionId)
                .HasColumnName("position_id")
                .HasMaxLength(50);

            builder.Property(e => e.Salary)
                .HasColumnName("salary")
                .IsRequired();

            builder.Property(e => e.DepartmentId)
                .HasColumnName("department_id")
                .HasMaxLength(100);

            builder.Property(e => e.Contract)
                .HasColumnName("contract")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.CreateDate)
                .HasColumnName("create_date")
                .IsRequired();

            builder.Property(c => c.Bonuses)
                .HasColumnName("bonuses");

            builder.HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
