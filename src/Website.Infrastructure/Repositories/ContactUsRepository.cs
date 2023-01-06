
using System.Linq.Expressions;
using KSFramework.Pagination;
using Microsoft.EntityFrameworkCore;
using Website.Domain.Aggregates.ContactUsMessages;
using Website.Infrastructure.Data;

namespace Website.Infrastructure.Repositories;

public class ContactUsRepository : Repository<ContactUsMessage>, IContactUsRepository
{
    public ContactUsRepository(DbContext context) : base(context)
    {
    }

    public async Task<int> GetMessagesCountByCheckStatusAsync(bool checkStatus)
    {
        return await Entity.CountAsync(x => x.IsChecked == checkStatus);
    }

    public async Task<PaginatedList<ContactUsMessage>> GetUncheckedPagedMessagesAsync(int pageIndex, int pageSize, Expression<Func<ContactUsMessage, bool>> where = null, string orderBy = "", bool desc = false)
    {
        return await PaginatedList<ContactUsMessage>.CreateAsync(Entity.Where(x => !x.IsChecked).AsQueryable(), pageIndex, pageSize, where, orderBy, desc);
    }

    public WebsiteDbContext DbContext
    {
        get
        {
            return DbContext as WebsiteDbContext;
        }
    }
}