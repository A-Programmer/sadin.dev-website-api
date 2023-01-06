using Website.Domain.Aggregates.ContactUsMessages;

namespace Website.Application.ContactUsMessages.Queries.GetMessageById;

public class GetMessageByIdDto
{
    public GetMessageByIdDto(ContactUsMessage message)
    {
        Id = message.Id;
        FullName = message.FullName;
        Email = message.Email;
        PhoneNumber = message.PhoneNumber;
        Title = message.Title;
        Content = message.Content;
        CreatedDate = message.CreatedDate;
        CheckedDate = message.CheckedDate;
        IsChecked = message.IsChecked;
    }
    
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }
    public DateTimeOffset? CheckedDate { get; private set; }
    public bool IsChecked { get; private set; }
}
