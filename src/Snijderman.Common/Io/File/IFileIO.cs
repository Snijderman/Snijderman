using System;
using System.IO;

namespace Snijderman.Common.Io.File
{
   public interface IFileIO
   {
      FileStream OpenFileWithoutLocking(string path);

      FileStream OpenFileWithoutLocking(string path, int bufferSize);

      void MoveFile(string inputFile, string outputFile);

      void MoveFile(string inputFile, string outputFile, bool always);

      bool WaitForFileReady(string fullName);

      bool WaitForFileReady(string fullName, TimeSpan timeout);
   }
}
