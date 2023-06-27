using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIVAuth
{
    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class XIVAuthScopeIdAttribute : Attribute
    {
        public XIVAuthScopeIdAttribute(string scopeId)
        {
            this.ScopeId = scopeId;
        }
        
        public string ScopeId { get; }
    }
}
