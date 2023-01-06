using Website.Domain.Aggregates.ContactUsMessages;

namespace Website.Application.ContactUsMessages.Queries.GetUncheckedMessages;

public class GetUncheckedMessagesDto
{
    public GetUncheckedMessagesDto(ContactUsMessage message)
    {
        Id = message.Id;
        FullName = message.FullName;
        Title = message.Title;
        CreatedDate = message.CreatedDate;
        CheckedDate = message.CheckedDate;
    }
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Title { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }
    public DateTimeOffset? CheckedDate { get; private set; }
}
