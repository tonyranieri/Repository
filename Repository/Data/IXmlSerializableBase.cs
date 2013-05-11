using System.Collections.Generic;

namespace Repository.Data
{
    public interface IXmlSerializableBase<T>
    {
        List<T> Items { get; set; }
    }
}