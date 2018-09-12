using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure
{
    public static class MyExtendMethods
    {
        public static void SetObject<T>(this T Obj, T Source)
        {
            var properties = Obj.GetType().GetProperties();
            foreach (var propery in properties)
            {
                string Name = propery.Name;
                var value = propery.GetValue(Obj, null);
                var propertySource = Source.GetType().GetProperty(Name);
                var valueSource = propertySource.GetValue(Source, null);
                if (value == null) propery.SetValue(Obj, valueSource, null);
                else
                if (!value.Equals(valueSource))
                    propery.SetValue(Obj, valueSource, null);
            }
        }
    }
}
