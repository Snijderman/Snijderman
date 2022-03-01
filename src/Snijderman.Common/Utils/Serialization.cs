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

   public static T DeserializeObjectFromStream<T>(System.IO.Stream stream) where T : class
   {
      if (stream == null)
      {
         throw new ArgumentNullException(nameof(stream));
      }

      IFormatter formatter = new BinaryFormatter();
      stream.Seek(0, SeekOrigin.Begin);
#pragma warning disable SYSLIB0011 // Type or member is obsolete
      var objectType = formatter.Deserialize(stream);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
      return objectType as T;
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

   /// <summary>
   /// serializes the given object into memory stream
   /// </summary>
   /// <param name="objectType">the object to be serialized</param>
   /// <returns>The serialized object as memory stream</returns>
   public static MemoryStream SerializeToStream<T>(T objectType)
   {
      var stream = new MemoryStream();
      IFormatter formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
      formatter.Serialize(stream, objectType);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
      return stream;
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
