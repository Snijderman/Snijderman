using System;
using System.Diagnostics;
using System.IO;

namespace Snijderman.Common.Io.File;

public class FileIO : IFileIO
{
   public FileStream OpenFileWithoutLocking(string path) => this.OpenFileWithoutLocking(path, 4096);

   public FileStream OpenFileWithoutLocking(string path, int bufferSize) => new(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, bufferSize);

   public void MoveFile(string inputFile, string outputFile) => this.MoveFile(inputFile, outputFile, false);

   public void MoveFile(string inputFile, string outputFile, bool always)
   {
      if (!System.IO.File.Exists(inputFile))
      {
         throw new FileNotFoundException($"Input file {inputFile} could not be found");
      }

      var fileName = this.GetNameOfOutputFile(outputFile, always);

      this.CheckDirectoryForFile(outputFile);

      this.ExecuteMoveFile(inputFile, outputFile, fileName);
   }

   private void ExecuteMoveFile(string inputFile, string outputFile, string fileName)
   {
      this.WaitForFileReady(inputFile);
      System.IO.File.Move(inputFile, fileName);
      this.WaitForFileReady(outputFile);
   }

   private string GetNameOfOutputFile(string outputFile, bool always)
   {
      if (!System.IO.File.Exists(outputFile))
      {
         return outputFile;
      }

      if (!always)
      {
         throw new IOException($"Outputfile {outputFile} is already present");
      }

      return $"{outputFile}{Guid.NewGuid()}";
   }

   /// <summary>
   /// Functie wacht tot het bestand beschikbaar is voor verdere verwerking
   /// Default timeout op 1 uur.
   /// </summary>
   /// <param name="fullName">Fully qualified bestandsnaam</param>
   /// <returns></returns>
   public bool WaitForFileReady(string fullName) => this.WaitForFileReady(fullName, new TimeSpan(1, 0, 0));

   /// <summary>
   /// Functie wacht tot het bestand beschikbaar is voor verdere verwerking
   /// </summary>
   /// <param name="fullName">Fully qualified bestandsnaam</param>
   /// <param name="timeout">Maximale wachttijd</param>
   /// <returns></returns>
   public bool WaitForFileReady(string fullName, TimeSpan timeout)
   {
      var _ = this.CheckFileExists(fullName);

      var sw = new Stopwatch();
      sw.Start();

      // test timeout
      while (true)
      {
         try
         {
            if (this.CheckIfFileIsAccesable(fullName))
            {
               return true;
            }

         }
         catch (Exception e1) when (e1.Message.Contains("being used", StringComparison.OrdinalIgnoreCase))
         {
         }
         if (sw.ElapsedMilliseconds > timeout.TotalMilliseconds)
         {
            return false;
         }

         System.Threading.Thread.Sleep(250);
      }
   }

   private FileInfo CheckFileExists(string fullName)
   {
      var fi = new FileInfo(fullName);
      if (!fi.Exists)
      {
         throw new FileNotFoundException($"Unable to find file '{fullName}' ");
      }

      return fi;
   }

   private bool CheckIfFileIsAccesable(string fullName)
   {
      // accesable?
      using (System.IO.File.OpenRead(fullName))
      {
         return true;
         // do nothing, OpenRead is only interesting action
      }
   }

   private void CheckDirectoryForFile(string file)
   {
      var fi = new FileInfo(file);
      if (Directory.Exists(fi.DirectoryName))
      {
         return;
      }

      Directory.CreateDirectory(fi.Directory.FullName);
   }
}
