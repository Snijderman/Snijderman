using System.Collections.Generic;
using System.IO;

namespace Snijderman.Common.Io.File
{
   public interface IFileHandling
   {
      FileHandlingSettings Settings { get; set; }

      string InputFolder { get; }

      string ProcessedFolder { get; }

      string ProcessedFolderForDay { get; }

      string FailedFolder { get; }

      void ValidateFolders();

      List<string> ListExistingFiles(string inputFilesFolder, string fileMask);

      List<string> ListExistingFiles(string inputFilesFolder, string fileMask, SearchOption searchOption);

      void MoveFileToProcessedFolder(string file);

      void MoveFileToFailedFolder(string file);

      void MoveFile(string inputFile, string outputFile);
   }
}
