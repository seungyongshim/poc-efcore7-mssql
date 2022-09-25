

using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

namespace Sample.EntityFramework;

public record ContactDetails
{
    public Address? Address { get; set; }
    public string? Phone { get; set; }
}

public record Address
(
    string Street,
    string City,
    string Postcode,
    string Country
);

public record Author
(
    int Id,
    string Name,
    ContactDetails Contact
);



public abstract class BlogsContext : DbContext
{
    protected BlogsContext(bool useSqlite = false)
    {
    }

    public bool UseSqlite { get; }
    public bool LoggingEnabled { get; set; }
    public virtual MappingStrategy MappingStrategy => MappingStrategy.Tph;

    public DbSet<Author> Authors => Set<Author>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(@$"Server=(Local);Database=AUMS;User Id=MT;Password=q1w2e3r4t5Y^U&I*O(P);Connection Timeout=3")
            .EnableSensitiveDataLogging()
            .LogTo(
                s =>
                {
                    if (LoggingEnabled)
                    {
                        Console.WriteLine(s);
                    }
                }, LogLevel.Information);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().OwnsOne(
            author => author.Contact, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
                ownedNavigationBuilder.OwnsOne(contactDetails => contactDetails.Address);
            });
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<List<string>>().HaveConversion<StringListConverter>();

        base.ConfigureConventions(configurationBuilder);
    }

    private class StringListConverter : ValueConverter<List<string>, string>
    {
        public StringListConverter()
            : base(v => string.Join(", ", v!), v => v.Split(',', StringSplitOptions.TrimEntries).ToList())
        {
        }
    }
}
