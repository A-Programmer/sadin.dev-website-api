using Website.Domain.Aggregates.ContactUsMessages;

namespace Website.Application.ContactUsMessages.Queries.GetPagedMessages;

public class GetPagedMessagesDto
{
    public GetPagedMessagesDto(ContactUsMessage message)
    {
        Id = message.Id;
        FullName = message.FullName;
        Title = message.Title;
        CreatedDate = message.CreatedDate;
        CheckedDate = message.CheckedDate;
        IsChecked = message.IsChecked;
    }
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Title { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }
    public DateTimeOffset? CheckedDate { get; private set; }
    public bool IsChecked { get; private set; }
}
