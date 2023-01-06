namespace Website.Application.ContactUsMessages.Queries.GetPagedMessages;

public class GetPagedMessagesQueryHandler : IRequestHandler<GetPagedMessagesQuery, PaginatedList<GetPagedMessagesDto>>
{
    private readonly IUnitOfWork _uow;
    public GetPagedMessagesQueryHandler(IUnitOfWork uow) => _uow = uow ?? throw new ArgumentNullException(nameof(uow));

    public async Task<PaginatedList<GetPagedMessagesDto>> Handle(GetPagedMessagesQuery request, CancellationToken cancellationToken)
    {
        var pagedItems = await _uow.ContactUsMessages.GetPagedAsync(request.PageIndex, request.PageSize, request.Where, request.OrderBy, request.Desc);
        return new PaginatedList<GetPagedMessagesDto>(pagedItems.Select(x => new GetPagedMessagesDto(x)).ToList(), pagedItems.TotalItems, pagedItems.PageIndex, request.PageSize);
    }
}
