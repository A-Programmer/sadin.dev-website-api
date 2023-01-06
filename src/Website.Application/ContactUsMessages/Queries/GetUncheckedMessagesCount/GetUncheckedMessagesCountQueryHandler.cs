namespace Website.Application.ContactUsMessages.Queries.GetUncheckedMessagesCount;

public class GetUncheckedMessagesCountQueryHandler : IRequestHandler<GetUncheckedMessagesCountQuery, int>
{
    private readonly IUnitOfWork _uow;
    public GetUncheckedMessagesCountQueryHandler(IUnitOfWork uow) => _uow = uow ?? throw new ArgumentNullException(nameof(uow));

    public async Task<int> Handle(GetUncheckedMessagesCountQuery request, CancellationToken cancellationToken)
    {
        return await _uow.ContactUsMessages.GetMessagesCountByCheckStatusAsync(request.CheckStatus);
    }
}
