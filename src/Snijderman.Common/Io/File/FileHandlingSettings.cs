using System.Collections.Generic;

namespace Snijderman.Common.Io.File;

public class FileHandlingSettings
{
   public FileHandlingSettings()
   {
      this.Settings = new Dictionary<string, string>();
   }

   public string Id { get; set; }

   public string Type { get; set; }

   public string Folder { get; set; }

   public Dictionary<string, string> Settings { get; set; }
}
