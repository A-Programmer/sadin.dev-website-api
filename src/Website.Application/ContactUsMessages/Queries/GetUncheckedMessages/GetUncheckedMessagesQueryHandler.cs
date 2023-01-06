namespace Website.Application.ContactUsMessages.Queries.GetUncheckedMessages;

public class GetUncheckedMessagesQueryHandler : IRequestHandler<GetUncheckedMessagesQuery, PaginatedList<GetUncheckedMessagesDto>>
{
    private readonly IUnitOfWork _uow;
    public GetUncheckedMessagesQueryHandler(IUnitOfWork uow) => _uow = uow ?? throw new ArgumentNullException(nameof(uow));

    public async Task<PaginatedList<GetUncheckedMessagesDto>> Handle(GetUncheckedMessagesQuery request, CancellationToken cancellationToken)
    {
        var pagedItems = await _uow.ContactUsMessages.GetUncheckedPagedMessagesAsync(request.PageIndex, request.PageSize, request.Where, request.OrderBy, request.Desc);
        return new PaginatedList<GetUncheckedMessagesDto>(pagedItems.Select(x => new GetUncheckedMessagesDto(x)).ToList(), pagedItems.Count, pagedItems.PageIndex, request.PageSize);
    }
}
