using Account.Domain.Aggregates;
using Common.Library.Seedwork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Account.Infrastructure;

public class AccountContext : DbContext, IUnitOfWork
{
    private const string DEFAULT_CONNECTION_SECTION = "DefaultConnection";
    private readonly IConfiguration _configuration;

    public DbSet<User> Users { get; set; }

    public AccountContext(DbContextOptions<AccountContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString(DEFAULT_CONNECTION_SECTION));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AccountConfiguration());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await base.SaveChangesAsync(cancellationToken);
            return true; 
        }
        catch (Exception)
        {
            return false;
        }
    }
}
