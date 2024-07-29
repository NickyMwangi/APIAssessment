namespace API.Mappers
{
    public class ProMapper : IProMapper
    {
        public TResult Map<TSource, TResult>(TSource src, object tag = null)
        {
            // using Mapper.Map<TSource, TResult> to specify the types because EF will use a proxy type which we can't have registered
            return MapperConfig.CrudMapper.Map<TSource, TResult>(src, tag);
        }

        public ICollection<TResult> MapList<TSource, TResult>(ICollection<TSource> srcList, object tag = null)
        {

            ICollection<TResult> res = new List<TResult>();
            foreach (TSource src in srcList)
            {
                var item = MapperConfig.CrudMapper.Map<TSource, TResult>(src);
                res.Add(item);
            }

            return res;
        }
    }
}
