namespace API.Mappers
{
    public interface IProMapper
    {
        TResult Map<TSource, TResult>(TSource src, object tag = null);
    }
}
