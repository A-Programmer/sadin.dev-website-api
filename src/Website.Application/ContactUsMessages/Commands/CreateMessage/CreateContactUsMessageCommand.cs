namespace Website.Application.ContactUsMessages.Commands.CreateMessage;

public record CreateContactUsMessageCommand(string Title, string Content, string FullName = null, string Email = null,
    string PhoneNumber = null) : IRequest<BaseResponse<Guid>>;