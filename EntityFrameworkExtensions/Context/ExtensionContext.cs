using System.Data.Entity;
using System.Linq;
using EntityFrameworkExtensions.Entities;
using HttpObjectCaching;

namespace EntityFrameworkExtensions.Context
{
    public class ExtensionContext : ActiveRecord.CodeFirst.SimpleContext<ExtensionContext>
    {

        //private static IQueryable<ItemNameMap> _itemNamesCached = null;

        public DbSet<ItemNameMap> ItemNameMaps { get; set; }

        public IQueryable<ItemNameMap> ItemNameMapsCached {
            get
            {
                var itemNamesCached = Cache.GetItem<IQueryable<ItemNameMap>>(CacheArea.Request, "ItemNameMapsCached");
                if(itemNamesCached == null)
                {
                    itemNamesCached = ItemNameMaps.ToList().AsQueryable();
                    Cache.SetItem(CacheArea.Request, "ItemNameMapsCached", itemNamesCached);
                }
                return itemNamesCached;
            }
        }
        public void ClearCache()
        {
            //_itemNamesCached = null; 
            Cache.SetItem<IQueryable<ItemNameMap>>(CacheArea.Request, "ItemNameMapsCached", null);
        }
        
        public static ExtensionContext Current
        {
            get
            {
                var context = Cache.GetItem<ExtensionContext>(CacheArea.Request, "ExtensionContext");
                if (context == null)
                {
                    context = new ExtensionContext();
                    Cache.SetItem(CacheArea.Request, "ExtensionContext", context);
                }
                return context;
            }
        }
        public ExtensionContext() : base("name=DefaultConnection")
        {

        }
       

    }
}
