namespace Website.Application.ContactUsMessages.Commands.MarkMessageAsUnchecked;

public record MarkMessageAsUncheckedCommand(Guid Id) : IRequest<BaseResponse<Guid>>;