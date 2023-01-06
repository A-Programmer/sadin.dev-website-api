namespace Website.Application.ContactUsMessages.Commands.MarkMessageAsUnchecked;

public class MarkMessageAsUncheckedCommandHandler : IRequestHandler<MarkMessageAsUncheckedCommand, BaseResponse<Guid>>
{
    private readonly IUnitOfWork _uow;
    public MarkMessageAsUncheckedCommandHandler(IUnitOfWork uow) => _uow = uow ?? throw new ArgumentNullException(nameof(uow));

    public async Task<BaseResponse<Guid>> Handle(MarkMessageAsUncheckedCommand request, CancellationToken cancellationToken)
    {
        var message = await _uow.ContactUsMessages.GetByIdAsync(request.Id);
        if(message == null)
            throw new KSNotFoundException("Message could not be found.");
        message.MarkAsUnChecked();
        await _uow.CommitAsync();
        return new OkResponse<Guid>(request.Id);
    }
}
