using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EntityFrameworkExtensions.Entities;
using HttpObjectCaching;

namespace EntityFrameworkExtensions.Context
{
    public class ExtensionContext : DbContext
    {

        //private static IQueryable<ItemNameMap> _itemNamesCached = null;

        public DbSet<ItemNameMap> ItemNameMaps { get; set; }


        private List<ItemNameMap> GetItemNameMaps()
        {
            return (from i in ItemNameMaps select i).ToList();
        }

        public List<ItemNameMap> ItemNameMapsCached {
            get
            {
                return Cache.GetItem<List<ItemNameMap>>(CacheArea.Global, "ItemNameMapsCached", 
                    () =>
                {
                    return GetItemNameMaps();
                }, 60*20);
            }
        }
        public void ClearCache()
        {
            //_itemNamesCached = null; 
            Cache.SetItem<IQueryable<ItemNameMap>>(CacheArea.Global, "ItemNameMapsCached", null);
        }
        
        public static ExtensionContext Current
        {
            get
            {
                return Cache.GetItem<ExtensionContext>(CacheArea.Request, "ExtensionContext", () =>
                {
                    return new ExtensionContext();
                });
            }
        }
        public ExtensionContext() : base("name=DefaultConnection")
        {

        }
       

    }
}
