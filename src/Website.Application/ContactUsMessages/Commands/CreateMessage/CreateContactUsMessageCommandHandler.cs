using Website.Domain.Aggregates.ContactUsMessages;

namespace Website.Application.ContactUsMessages.Commands.CreateMessage;

public class CreateContactUsMessageCommandHandler : IRequestHandler<CreateContactUsMessageCommand, BaseResponse<Guid>>
{
    private readonly IUnitOfWork _uow;
    public CreateContactUsMessageCommandHandler(IUnitOfWork uow) =>
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    
    public async Task<BaseResponse<Guid>> Handle(CreateContactUsMessageCommand request, CancellationToken cancellationToken)
    {
        var message = ContactUsMessage.Create(request.Title, request.Content, request.FullName, request.Email,
            request.PhoneNumber);

        await _uow.ContactUsMessages.AddAsync(message);
        await _uow.CommitAsync();

        return new OkResponse<Guid>(message.Id);
    }
}