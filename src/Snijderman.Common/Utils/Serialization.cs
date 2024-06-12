using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace Snijderman.Common.Utils;

public static class Serialization
{
   public static T DeserializeObjectFromXmlStream<T>(System.IO.Stream stream)
   {
      if (stream == null)
      {
         throw new ArgumentNullException(nameof(stream));
      }

      var serializer = new XmlSerializer(typeof(T));
      stream.Position = 0;
      using var xmlReader = new XmlTextReader(stream)
      {
         Namespaces = false
      };
      return (T)serializer.Deserialize(xmlReader);
   }

   public static T DeserializeObjectFromXml<T>(string xml)
   {
      T result;

      var ser = new XmlSerializer(typeof(T));
      using (var xmlReader = XmlReader.Create(new StringReader(xml)))
      {
         result = (T)ser.Deserialize(xmlReader);
      }

      return result;
   }

   public static string SerializeObjectToXml<T>(T obj)
   {
      var serializer = new XmlSerializer(obj.GetType());

      using StringWriter writer = new Utf8StringWriter();
      serializer.Serialize(writer, obj);

      return writer.ToString();
   }

   public static string SerializeObjectToXml<T>(T obj, string defaultNamespace)
   {
      var serializer = new XmlSerializer(obj.GetType(), defaultNamespace);

      using StringWriter writer = new Utf8StringWriter();
      serializer.Serialize(writer, obj);

      return writer.ToString();
   }

   public static System.IO.Stream SerializeObjectToXmlStream<T>(T obj)
   {
      var serializer = new XmlSerializer(obj.GetType());
      var stream = new MemoryStream();

      serializer.Serialize(stream, obj, null);

      return stream;
   }

   private class Utf8StringWriter : StringWriter
   {
      public override System.Text.Encoding Encoding => System.Text.Encoding.UTF8;
   }

}
