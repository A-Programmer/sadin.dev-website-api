namespace Website.Application.ContactUsMessages.Commands.RemoveMessage;

public record RemoveMessageCommand(Guid Id) : IRequest<BaseResponse<Guid>>;