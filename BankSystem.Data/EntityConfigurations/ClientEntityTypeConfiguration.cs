using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityConfigurations
{
    public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("client");

            builder.HasKey(c => c.Id);

            builder.HasIndex(c => new { c.PassportSeries, c.PassportNumber })
                .IsUnique();

            builder.Property(c => c.Id)
                .HasColumnName("id");            

            builder.Property(c => c.FName)
                .HasColumnName("f_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.LName)
                .HasColumnName("l_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.MName)
                .HasColumnName("m_name")
                .HasMaxLength(50);

            builder.Property(c => c.BDate)
                .HasColumnName("bdate")
                .HasMaxLength(50);

            builder.Property(c => c.PassportNumber)
                .HasColumnName("passport_number")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(c => c.PassportSeries)
                .HasColumnName("passport_series")
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(c => c.Telephone)
                .HasColumnName("telephone")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Address)
                .HasColumnName("address")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.CreateDate)
                .HasColumnName("create_date")
                .IsRequired();

            builder.Property(c => c.Bonuses)
                .HasColumnName("bonuses");
        }
    }
}
