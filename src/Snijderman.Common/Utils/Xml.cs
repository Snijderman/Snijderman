using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Snijderman.Common.Utils;

public static class Xml
{
   public static (bool Succes, string ErrorMessage) ValidateXml(string xmlFile, XmlSchema schema)
   {
      if (string.IsNullOrEmpty(xmlFile))
      {
         throw new ArgumentNullException(nameof(xmlFile));
      }

      var xml = LoadXml(xmlFile);

      return ValidateXml(xml, schema);
   }

   private static XDocument LoadXml(string xmlFile)
   {
      return XDocument.Load(xmlFile);
   }

   public static (bool Succes, string ErrorMessage) ValidateXml(XDocument xml, XmlSchema schema)
   {
      if (xml == null)
      {
         throw new ArgumentNullException(nameof(xml));
      }

      var sb = new StringBuilder();
      var isValid = true;
      var errorCount = 0;

      var readerSettings = GetXmlReaderSettings(schema);
      readerSettings.ValidationEventHandler += (sender, args) =>
      {
         sb.AppendLine($"Xml validation error. {args.Message}");
         isValid = false;
         errorCount++;
      };

      using (var reader = XmlReader.Create(xml.CreateReader(), readerSettings))
      {
         while (reader.Read() && errorCount < 10) //Stop after 10 errors
         {
            // keep reading untill max # of errors
         }
      }

      return (isValid, sb.Length > 0 ? sb.ToString() : null);
   }

   private static XmlReaderSettings GetXmlReaderSettings(XmlSchema schema)
   {
      var readerSettings = new XmlReaderSettings
      {
         ValidationType = ValidationType.Schema
      };
      readerSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
      readerSettings.Schemas.Add(schema);

      return readerSettings;
   }
}
