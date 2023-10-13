using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NonProfitLibrary;

namespace SQLiteApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<ContactList>? ContactLists { get; set; } = default!;
    public DbSet<TransactionType>? TransactionTypes { get; set; } = default!;
    public DbSet<PaymentMethod>? PaymentMethods { get; set; } = default!;
    public DbSet<Donations>? Donations { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ContactList>().ToTable("ContactList");
        builder.Entity<TransactionType>().ToTable("TransactionType");
        builder.Entity<PaymentMethod>().ToTable("PaymentMethod");
        builder.Entity<Donations>().ToTable("Donations");

        // seed the database
        SeedData.Seed(builder);

        // seed the users and roles
        ModelBuilderExtensions.Seed(builder);
    }
}
