using Website.Domain.Aggregates.ContactUsMessages;

namespace Website.Domain;

public interface IUnitOfWork : IDisposable
{
    public  IContactUsRepository ContactUsMessages { get; }
    Task<int> CommitAsync();
}