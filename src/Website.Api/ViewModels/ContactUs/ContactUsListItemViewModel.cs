using Website.Application.ContactUsMessages.Queries.GetPagedMessages;

namespace Website.Api.ViewModels.ContactUs;

public class ContactUsListItemViewModel
{
    public ContactUsListItemViewModel(GetPagedMessagesDto message)
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