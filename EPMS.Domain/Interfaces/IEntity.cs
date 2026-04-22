namespace EPMS.Domain.Interfaces;

public interface IEntity<TKey>
{
    TKey Id { get; }
}
