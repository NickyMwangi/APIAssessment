using API.Mappers.Injections;
using API.Models;
using Data.Interfaces;
using Omu.ValueInjecter;

namespace API.Mappers
{
    public class MapperConfig
    {
        public static MapperInstance CrudMapper = new MapperInstance();

        public static void Configure()
        {
            // tag is being used here to pass in the existing Entity ( pulled from the Db )
            CrudMapper.DefaultMap = (src, resType, tag) =>
            {
                //var res = tag != null && tag.GetType().IsSubclassOf(typeof(Entity)) ? tag : Activator.CreateInstance(resType);
                var res = tag != null ? tag : Activator.CreateInstance(resType);
                // handle basic properties
                res.InjectFrom(src);
                var srcType = src.GetType();

                // handle the rest Country.Id <-> CountryId; int <-> int?
                //if (srcType.IsSubclassOf(typeof(Entity)) && resType.IsSubclassOf(typeof(NavInput)))
                //{
                //    res.InjectFrom<NormalToNullables>(src)
                //       .InjectFrom<EntitiesToInts>(src);
                //}
                //else if (srcType.IsSubclassOf(typeof(NavInput)) && resType.IsSubclassOf(typeof(Entity)))
                //{
                //    res.InjectFrom<IntsToEntities>(src).
                //      InjectFrom<NullablesToNormal>(src);
                //}
                //else if (!srcType.IsSubclassOf(typeof(Entity)) && resType.IsSubclassOf(typeof(NavInput)))
                //{
                //    res.InjectFrom<NormalToNullables>(src);
                //    //.InjectFrom<EntitiesToInts>(src);
                //}
                //else if (srcType.IsSubclassOf(typeof(NavInput)) && !resType.IsSubclassOf(typeof(Entity)))
                //{
                //    //res.InjectFrom<IntsToEntities>(src)
                //    res.InjectFrom<NullablesToNormal>(src);
                //}

                return res;
            };

        }
    }
}
