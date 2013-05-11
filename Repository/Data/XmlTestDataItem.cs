using System;

namespace Repository.Data
{
    [Serializable]
    public class XmlTestDataItem : IXmlSerializableItem
    {
        public int Id { get; set; }
        public string Foo { get; set; }
    }
}