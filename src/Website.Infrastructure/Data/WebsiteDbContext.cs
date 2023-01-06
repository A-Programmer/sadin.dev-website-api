using Microsoft.EntityFrameworkCore;
using Website.Domain.Aggregates.ContactUsMessages;

namespace Website.Infrastructure.Data;

public class WebsiteDbContext : DbContext
{
    public WebsiteDbContext(DbContextOptions<WebsiteDbContext> options)
        : base(options)
    {
    }

    public DbSet<ContactUsMessage> Messages { get; set; }
}