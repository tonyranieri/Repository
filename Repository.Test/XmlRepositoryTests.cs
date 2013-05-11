using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Data;
using Repository.Serialization;
using Moq;

namespace Repository.Test
{
    [TestClass]
    public class XmlRepositoryTests
    {
        public static Mock<IXmlSerializationService<XmlDataWrapper<XmlTestDataItem>>> GetMockedXmlSerializationService()
        {
            return new Mock<IXmlSerializationService<XmlDataWrapper<XmlTestDataItem>>>();
        }

        [TestClass]
        public class GetTests
        {
            [TestMethod]
            public void NoItemFound_ReturnsNull()
            {
                var xmlSerializationSvc = GetMockedXmlSerializationService();
                xmlSerializationSvc.Setup(x => x.ReadXml(It.IsAny<string>()))
                                   .Returns(new XmlDataWrapper<XmlTestDataItem>());

                var repo = new XmlRepository<XmlDataWrapper<XmlTestDataItem>, XmlTestDataItem>("foo.bar", xmlSerializationSvc.Object);
                var result = repo.Get(1);

                Assert.IsTrue(result == null);
            }

            [TestMethod]
            public void ItemFound_ReturnItem()
            {
                var xmlSerializationSvc = GetMockedXmlSerializationService();
                xmlSerializationSvc.Setup(x => x.ReadXml(It.IsAny<string>()))
                                   .Returns(new XmlDataWrapper<XmlTestDataItem>()
                                       {
                                           Items = new List<XmlTestDataItem>()
                                               {
                                                   new XmlTestDataItem() {Id = 1, Foo = "I'm the value!"}
                                               }
                                       });

                var repo = new XmlRepository<XmlDataWrapper<XmlTestDataItem>, XmlTestDataItem>("foo.bar", xmlSerializationSvc.Object);
                var result = repo.Get(1);

                Assert.IsTrue(result != null);
                Assert.IsTrue(result.Id == 1);
                Assert.IsTrue(result.Foo == "I'm the value!");
            }
        }

        [TestClass]
        public class AllTests
        {
            [TestMethod]
            public void NoItems_ReturnsEmptyList()
            {
                var xmlSerializationSvc = GetMockedXmlSerializationService();
                xmlSerializationSvc.Setup(x => x.ReadXml(It.IsAny<string>()))
                                   .Returns(new XmlDataWrapper<XmlTestDataItem>());

                var repo = new XmlRepository<XmlDataWrapper<XmlTestDataItem>, XmlTestDataItem>
                    ("foo.bar", xmlSerializationSvc.Object);
                var result = repo.All();

                Assert.IsTrue(result != null);
                Assert.IsTrue(result.Any() == false);
            }

            [TestMethod]
            public void Items_ReturnsPopulatedList()
            {
                var xmlSerializationSvc = GetMockedXmlSerializationService();
                xmlSerializationSvc.Setup(x => x.ReadXml(It.IsAny<string>()))
                                   .Returns(new XmlDataWrapper<XmlTestDataItem>()
                                       {
                                           Items = new List<XmlTestDataItem>()
                                               {
                                                   new XmlTestDataItem() { Id = 1},
                                                   new XmlTestDataItem() { Id = 2},
                                                   new XmlTestDataItem() { Id = 3},
                                               }
                                       });

                var repo = new XmlRepository<XmlDataWrapper<XmlTestDataItem>, XmlTestDataItem>
                    ("foo.bar", xmlSerializationSvc.Object);
                var result = repo.All();

                Assert.IsTrue(result != null);
                Assert.IsTrue(result.Count() == 3);
            }
        }
    }
}
