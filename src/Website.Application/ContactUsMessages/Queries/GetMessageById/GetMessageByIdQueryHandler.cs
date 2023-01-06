namespace Website.Application.ContactUsMessages.Queries.GetMessageById;

public class GetMessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, GetMessageByIdDto>
{
    private readonly IUnitOfWork _uow;
    public GetMessageByIdQueryHandler(IUnitOfWork uow) => _uow = uow ?? throw new ArgumentNullException(nameof(uow));

    public async Task<GetMessageByIdDto> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
    {
        var message = await _uow.ContactUsMessages.GetByIdAsync(request.Id);
        if(message == null) throw new KSNotFoundException("Message could not be found.");
        return new GetMessageByIdDto(message);
    }
}
