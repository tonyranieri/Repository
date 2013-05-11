using System.IO;
using System.Xml.Serialization;

namespace Repository.Serialization
{
    public class XmlSerializationService<T> : IXmlSerializationService<T> where T : new()
    {
        public T ReadXml(string filename)
        {
            if (!File.Exists(filename))
                return new T();

            var serializer = new XmlSerializer(typeof(T));

            using (var myWriter = new StreamReader(filename))
            {
                var value = (T)serializer.Deserialize(myWriter);
                myWriter.Close();
                return value;
            }
        }

        public void WriteXml(string filename, T data)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var myWriter = new StreamWriter(filename))
            {
                serializer.Serialize(myWriter, data);
                myWriter.Close();
            }
        }
    }
}
