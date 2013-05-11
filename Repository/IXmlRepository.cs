using Repository.Data;
using System.Collections.Generic;

namespace Repository
{
    public interface IXmlRepository<TWrapper, TItem> 
        where TWrapper : IXmlSerializableBase<TItem>, new() 
        where TItem : IXmlSerializableItem, new()
    {
        TItem Get(int id);
        IEnumerable<TItem> All();
        void Insert(TItem entity);
        void Remove(TItem entity);
        void SaveChanges();
    }
}