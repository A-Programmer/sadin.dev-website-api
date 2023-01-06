using Website.Domain;
using Website.Domain.Aggregates.ContactUsMessages;
using Website.Infrastructure.Data;
using Website.Infrastructure.Repositories;

namespace Website.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly WebsiteDbContext _db;
    public UnitOfWork(WebsiteDbContext db) => _db = db ?? throw new ArgumentNullException(nameof(db));
    
    private ContactUsRepository _contactUsMessages;
    public IContactUsRepository ContactUsMessages => _contactUsMessages ??= new ContactUsRepository(_db);
    public async Task<int> CommitAsync()
    {
        return await _db.SaveChangesAsync();
    }
    
    
    public void Dispose() => _db.Dispose();
}