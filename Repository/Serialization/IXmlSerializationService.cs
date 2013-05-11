namespace Repository.Serialization
{
    public interface IXmlSerializationService<TWrapper> where TWrapper : new()
    {
        TWrapper ReadXml(string filename);
        void WriteXml(string filename, TWrapper data);
    }
}