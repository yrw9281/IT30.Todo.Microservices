using System.Reflection;
using Common.Library.Interceptors;
using Common.Library.Seedwork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Todo.Domain.Aggregates;

namespace Todo.Infrastructure;

public class TodoContext : DbContext, IUnitOfWork
{
    private const string DEFAULT_CONNECTION_SECTION = "DefaultConnection";
    private readonly IConfiguration _configuration;
    private readonly DomainEventsInterceptor _domainEventsInterceptor;

    public DbSet<TodoList> TodoLists { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }

    public TodoContext(
        DbContextOptions<TodoContext> options,
        IConfiguration configuration,
        DomainEventsInterceptor domainEventsInterceptor
        ) : base(options)
    {
        _configuration = configuration;
        _domainEventsInterceptor = domainEventsInterceptor;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString(DEFAULT_CONNECTION_SECTION));
            optionsBuilder.AddInterceptors(_domainEventsInterceptor);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
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
