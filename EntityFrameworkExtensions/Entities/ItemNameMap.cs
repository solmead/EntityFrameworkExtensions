using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
using ActiveRecord.CodeFirst;
using EntityFrameworkExtensions.Context;

namespace EntityFrameworkExtensions.Entities
{
    [Bind(Exclude = "Id")]
    public class ItemNameMap : Record<ItemNameMap>
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name="Original Name")]
        public string DefaultDescription { get; set; }
        [Display(Name = "Field Name")]
        public string CurrentDescription { get; set; }
        [Display(Name = "Extra Data")]
        public string Extra { get; set; }
        [Display(Name = "Show Field")]
        public bool IsShown { get; set; }

        public int? LinkId { get; set; }


        [NotMapped]
        public string CssName
        {
            get { return Name.Replace(".", "_"); }
        }
        public static ItemNameMap LoadByName(System.Data.Entity.DbContext db, string name, int linkId)
        {
            return (from r in db.Set<ItemNameMap>() where r.Name == name && (linkId==0 || r.LinkId == linkId) select r).FirstOrDefault();
        }

        public static ItemNameMap LoadByName(string name, int linkId)
        {
            return (from r in ExtensionContext.Current.ItemNameMapsCached where r.Name == name && (linkId == 0 || r.LinkId == linkId) select r).FirstOrDefault();
        }

        public static IQueryable<ItemNameMap> GetListOrdered(System.Data.Entity.DbContext db, int linkId)
        {
            return (from u in db.Set<ItemNameMap>() where (linkId == 0 || u.LinkId == linkId) orderby u.Name select u);
        }
    }
}
