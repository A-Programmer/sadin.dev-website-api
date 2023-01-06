namespace Website.Application.ContactUsMessages.Commands.MarkMessageAsChecked;

public record MarkMessageAsCheckedCommand(Guid Id) : IRequest<BaseResponse<Guid>>;