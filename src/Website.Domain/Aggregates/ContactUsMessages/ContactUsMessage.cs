namespace Website.Domain.Aggregates.ContactUsMessages;

public class ContactUsMessage : BaseEntity<Guid>, IAggregateRoot
{
    public ContactUsMessage(Guid id, string title, string content, DateTimeOffset createdAt, bool isChecked, DateTimeOffset? checkedDate = null, string fullName = null, string email = null, string phoneNumber = null)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        Title = title ?? throw new KSArgumentNullException(nameof(title));
        Content = content ?? throw new KSArgumentNullException(nameof(content));
        CreatedDate = createdAt;
        CheckedDate = checkedDate;
        IsChecked = isChecked;
    }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }
    public DateTimeOffset? CheckedDate { get; private set; }
    public bool IsChecked { get; private set; }
    
    protected ContactUsMessage()
    { }

    public static ContactUsMessage Create(string title, string content, string fullName = null, string email = null,
        string phoneNumber = null)
    {
        var message = new ContactUsMessage(Guid.NewGuid(), title, content, DateTimeOffset.UtcNow, false, null, fullName,
            email, phoneNumber);
        // TODO: Adding Domain Event
        return message;
    }

    public void MarkAsChecked()
    {
        IsChecked = true;
        CheckedDate = DateTimeOffset.UtcNow;
        
        // TODO : Adding Domain Event
    }
    public void MarkAsUnChecked() => IsChecked = false; // TODO : Adding Domain Event

}