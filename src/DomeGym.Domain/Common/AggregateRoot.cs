
namespace DomeGym.Domain.Common;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id) : base(id)
    {
    }
    protected AggregateRoot() { }
}