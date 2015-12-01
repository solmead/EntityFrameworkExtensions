using System;
using System.ComponentModel;
using System.Diagnostics;
using EntityFrameworkExtensions.Context;
using EntityFrameworkExtensions.Entities;

namespace EntityFrameworkExtensions
{
    [AttributeUsage(AttributeTargets.Property ,Inherited=true)]
    public class DisplayNameDbAttribute:DisplayNameAttribute
    {
        public string PropertyName { get; set; }

        private string dbName = "";

        public DisplayNameDbAttribute(string defaultName):base(defaultName)
        {
            
        }

        public ItemNameMap NameMap
        {
            get
            {
                var kName = PropertyName;
                if (kName == "")
                {
                    kName = DisplayNameValue;
                }
                var inm = ItemNameMap.LoadByName(kName, Helpers.LinkId);
                if (inm == null)
                {
                    inm = new ItemNameMap
                    {
                        DefaultDescription = DisplayNameValue,
                        Name = kName,
                        CurrentDescription = DisplayNameValue,
                        IsShown = true
                    };
                    inm.Save(ExtensionContext.Current);
                    ExtensionContext.Current.ClearCache();
                }
                return inm;
            }
        }

        public override string DisplayName
        {
            get
            {
                //dbName = DisplayNameValue;
                if (dbName == "")
                {
                    try
                    {
                        return NameMap.CurrentDescription;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                        return DisplayNameValue;
                    }
                }
                return dbName;
                
            }
        }
    }
}
