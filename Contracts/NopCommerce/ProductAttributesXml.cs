using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Contracts.NopCommerce
{
    [XmlRoot(ElementName = "Attributes")]
    public class ProductAttributesXml
    {
        [XmlElement(ElementName = "ProductAttribute")]
        public List<ProductAttributeXml> ProductAttributeXml { get; set; }
    }
     
    [XmlRoot(ElementName = "ProductAttribute")]
    public class ProductAttributeXml
    {
        [XmlElement(ElementName = "ProductAttributeValue")]
        public ProductAttributeValueXml ProductAttributeValueXml { get; set; }

        [XmlAttribute(AttributeName = "ID")]
        public int ID { get; set; }

        [XmlText]
        public string Text { get; set; }
    }


    [XmlRoot(ElementName = "ProductAttributeValue")]
    public class ProductAttributeValueXml
    {

        [XmlElement(ElementName = "Value")]
        public int Value { get; set; }
    }
    public static class XmlExtension
    {
        public static ProductAttributesXml ConvertProductAttributesXml(this string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProductAttributesXml));

            return (ProductAttributesXml)serializer.Deserialize(new StringReader(xml));

        }

    }
}
