using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityConfigurations
{
    public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("account");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("id");

            builder.Property(a => a.NameCur)
                .HasColumnName("name_cur")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(a => a.Amount)
                .HasColumnType("money")
                .HasColumnName("amount")
                .IsRequired();

            builder.Property(a => a.AccountNumber)
                .HasColumnName("account_number")
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(a => a.ClientId)
                .HasColumnName("client_id");

            builder.Property(a => a.CreateDate)
                .HasColumnName("create_date")
                .IsRequired();

            builder.Property(a => a.CurrencyId)
                .HasColumnName("currency_id");

            builder.HasOne(a => a.Client)
                .WithMany(a => a.Accounts)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Currency)
                .WithMany(a => a.Accounts)
                .HasForeignKey(a => a.CurrencyId);
        }
    }
}
