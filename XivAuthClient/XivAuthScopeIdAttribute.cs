using System;

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
