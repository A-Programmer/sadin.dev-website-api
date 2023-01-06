using System.Linq.Expressions;
using KSFramework.Utilities;
using Website.Domain.Aggregates.ContactUsMessages;

namespace Website.Application.ContactUsMessages.Queries.GetPagedMessages;

public class GetPagedMessagesQuery : Paginated, IRequest<PaginatedList<GetPagedMessagesDto>>
{
    public GetPagedMessagesQuery(int? pageNumber, int? pageSize, string searchString = "", string ordeBy = "", bool desc = false)
        : base(pageNumber, pageSize)
    {
        if(searchString.HasValue())
        {
            Where = x =>
                    x.Title.ToLower().Contains(searchString.ToLower()) ||
                    x.FullName.ToLower().Contains(searchString.ToLower()) ||
                    x.PhoneNumber.ToLower().Contains(searchString.ToLower()) ||
                    x.Email.ToLower().Contains(searchString.ToLower()) ||
                    x.Content.ToLower().Contains(searchString.ToLower());
        }
        OrderBy = ordeBy;
        Desc = desc;
    }

    public Expression<Func<ContactUsMessage, bool>>? Where { get; private set; }
    public string OrderBy { get; private set; }
    public bool Desc { get; private set; }
}