namespace Website.Application.ContactUsMessages.Queries.GetMessageById;

public record GetMessageByIdQuery(Guid Id) : IRequest<GetMessageByIdDto>;