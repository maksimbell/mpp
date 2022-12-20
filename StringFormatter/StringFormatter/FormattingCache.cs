using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatting
{
    public class FormattingCache
    {
        private ConcurrentDictionary<string, Func<object, string>> _elements;

        public IReadOnlyDictionary<string, Func<object, string>> Elements
        {
            get { return _elements; }
        }

        public FormattingCache()
        {
            _elements = new ConcurrentDictionary<string, Func<object, string>>();
        }

        public string? GetOrAdd(string memberName, object target)
        {
            string key = target.GetType().ToString() + "." + memberName;
            Func<object, string>? value;

            if(_elements.TryGetValue(key, out value))
            {
                return value(target);
            }
            else
            {
                if(target.GetType().GetProperty(memberName) != null ||
                    target.GetType().GetField(memberName) != null)
                {
                    var objParam = Expression.Parameter(typeof(object));
                    var member = Expression.PropertyOrField(Expression.TypeAs(objParam, target.GetType()), memberName);
                    var memberToString = Expression.Call(member, "ToString", null, null);
                    var toString = Expression.Lambda<Func<object, string>>(memberToString, objParam).Compile();

                    _elements.TryAdd(key, toString);

                    return toString(target);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
