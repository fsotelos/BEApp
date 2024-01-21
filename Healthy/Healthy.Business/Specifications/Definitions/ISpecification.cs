namespace Healthy.Business.Specifications.Definitions
{
    public interface ISpecification<T>
    {
        IEnumerable<string> Valid(T item);
    }
}