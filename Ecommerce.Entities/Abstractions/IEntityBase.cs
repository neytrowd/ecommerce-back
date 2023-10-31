namespace Ecommerce.Entities.Abstractions
{
    public interface IEntityBase<TKey>
    {
        TKey Id { get; }
    }
}