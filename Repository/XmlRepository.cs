using System;
using Repository.Data;
using Repository.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class XmlRepository<TWrapper, TItem> : IXmlRepository<TWrapper, TItem>
        where TWrapper : IXmlSerializableBase<TItem>, new()
        where TItem : IXmlSerializableItem, new()
    {
        #region Variables

        private readonly TWrapper _data;
        private readonly string _fileName;

        #endregion
        #region Properties

        private readonly IXmlSerializationService<TWrapper> _xmlSerializationService;
        protected IXmlSerializationService<TWrapper> XmlSerializationService
        {
            get { return _xmlSerializationService ?? new XmlSerializationService<TWrapper>(); }
            private set { }
        }

        #endregion
        #region Constructor

        public XmlRepository(string filename, IXmlSerializationService<TWrapper> xmlSerializationService)
        {
            _fileName = filename;
            _xmlSerializationService = xmlSerializationService;
            _data = XmlSerializationService.ReadXml(filename);
        }

        public XmlRepository(string filename)
            : this(filename, null)
        { }

        #endregion
        #region Methods

        public TItem Get(int id)
        {
            return _data.Items.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<TItem> All()
        {
            return _data.Items;
        }

        public void Insert(TItem entity)
        {
            if(IsNullEntity(entity))
                throw new NullReferenceException("Entity cannot be null.");

            var item = Get(entity.Id);
            if(!IsNullEntity(item))
                throw new ApplicationException("Id already exists.");

            _data.Items.Add(entity);
        }

        public void Remove(TItem entity)
        {
            if (IsNullEntity(entity))
                throw new NullReferenceException("Entity cannot be null.");

            var item = Get(entity.Id);
            if (IsNullEntity(item))
                throw new ApplicationException("Id does not exist.");

            _data.Items.Remove(entity);
        }

        public void SaveChanges()
        {
            XmlSerializationService.WriteXml(_fileName, _data);
        }

        #endregion
        #region Private Methods

        private static bool IsNullEntity(TItem entity)
        {
            return EqualityComparer<TItem>.Default.Equals(entity, default(TItem));
        }

        #endregion
    }
}
