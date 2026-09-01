using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStoreOrder
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class HandlingRequirementAttribute:Attribute
    {
        public string requirement { get; }
        public HandlingRequirementAttribute(string requirement)
        {
            this.requirement = requirement;
        }
    }
}
