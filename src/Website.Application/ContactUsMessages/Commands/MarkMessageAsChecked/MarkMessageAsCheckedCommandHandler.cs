namespace Website.Application.ContactUsMessages.Commands.MarkMessageAsChecked;

public class MarkMessageAsCheckedCommandHandler : IRequestHandler<MarkMessageAsCheckedCommand, BaseResponse<Guid>>
{
    private readonly IUnitOfWork _uow;
    public MarkMessageAsCheckedCommandHandler(IUnitOfWork uow) => _uow = uow ?? throw new ArgumentNullException(nameof(uow));

    public async Task<BaseResponse<Guid>> Handle(MarkMessageAsCheckedCommand request, CancellationToken cancellationToken)
    {
        var message = await _uow.ContactUsMessages.GetByIdAsync(request.Id);
        if(message == null)
            throw new KSNotFoundException("Message could not be found.");
        message.MarkAsChecked();
        await _uow.CommitAsync();
        return new OkResponse<Guid>(request.Id);
    }
}
