using System;
using System.Collections.Generic;

namespace Repository.Data
{
    [Serializable]
    public class XmlDataWrapper<T> : IXmlSerializableBase<T> where T : new()
    {
        public XmlDataWrapper()
        {
            Items = new List<T>();
        }

        public List<T> Items { get; set; }
    }
}
