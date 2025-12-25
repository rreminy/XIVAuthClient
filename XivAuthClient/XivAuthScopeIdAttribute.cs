using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XivAuth
{
    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class XivAuthScopeIdAttribute : Attribute
    {
        public XivAuthScopeIdAttribute(string scopeId)
        {
            this.ScopeId = scopeId;
        }
        
        public string ScopeId { get; }
    }
}
