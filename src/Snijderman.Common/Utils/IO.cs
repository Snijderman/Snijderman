using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Snijderman.Common.Utils;

public static class Io
{
   public static FileStream OpenFileWithoutLocking(string path)
   {
      return OpenFileWithoutLocking(path, 4096);
   }

   public static FileStream OpenFileWithoutLocking(string path, int bufferSize)
   {
      return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, bufferSize);
   }

   public static void MoveFile(string inputFile, string outputFile)
   {
      MoveFile(inputFile, outputFile, false);
   }

   public static void MoveFile(string inputFile, string outputFile, bool always)
   {
      if (!File.Exists(inputFile))
      {
         throw new FileNotFoundException($"Input file {inputFile} could not be found");
      }

      var fileName = GetNameOfOutputFile(outputFile, always);

      CheckDirectoryForFile(outputFile);

      ExecuteMoveFile(inputFile, outputFile, fileName);
   }

   private static void ExecuteMoveFile(string inputFile, string outputFile, string fileName)
   {
      WaitForFileReady(inputFile);
      File.Move(inputFile, fileName);
      WaitForFileReady(outputFile);
   }

   private static string GetNameOfOutputFile(string outputFile, bool always)
   {
      if (!File.Exists(outputFile))
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
   public static bool WaitForFileReady(string fullName)
   {
      return WaitForFileReady(fullName, new TimeSpan(1, 0, 0));
   }

   /// <summary>
   /// Functie wacht tot het bestand beschikbaar is voor verdere verwerking
   /// </summary>
   /// <param name="fullName">Fully qualified bestandsnaam</param>
   /// <param name="timeout">Maximale wachttijd</param>
   /// <returns></returns>
   public static bool WaitForFileReady(string fullName, TimeSpan timeout)
   {
      var _ = CheckFileExists(fullName);

      var sw = new Stopwatch();
      sw.Start();

      // test timeout
      while (true)
      {
         try
         {
            if (CheckIfFileIsAccesable(fullName))
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

         Thread.Sleep(250);
      }
   }

   private static FileInfo CheckFileExists(string fullName)
   {
      var fi = new FileInfo(fullName);
      if (!fi.Exists)
      {
         throw new FileNotFoundException($"Unable to find file '{fullName}' ");
      }

      return fi;
   }

   private static bool CheckIfFileIsAccesable(string fullName)
   {
      // accesable?
      using (File.OpenRead(fullName))
      {
         return true;
         // do nothing, OpenRead is only interesting action
      }
   }

   private static void CheckDirectoryForFile(string file)
   {
      var fi = new FileInfo(file);
      if (Directory.Exists(fi.DirectoryName))
      {
         return;
      }

      Directory.CreateDirectory(fi.Directory.FullName);
   }

   public static string GetDirectoryOfExecutingAssembly()
   {
      return Path.GetDirectoryName(new Uri(Assembly.GetEntryAssembly().Location).LocalPath);
   }

   public static string GetDirectoryOfExecutingAssembly(Type type)
   {
      return Path.GetDirectoryName(new Uri(Assembly.GetAssembly(type).Location).LocalPath);
   }

   public static void RecursiveCopyFolder(string sourceFolder, string destFolder)
   {
      if (!Directory.Exists(destFolder))
      {
         Directory.CreateDirectory(destFolder);
      }

      foreach (var file in Directory.GetFiles(sourceFolder))
      {
         var name = Path.GetFileName(file);
         var dest = Path.Combine(destFolder, name);
         File.Copy(file, dest);
      }

      foreach (var folder in Directory.GetDirectories(sourceFolder))
      {
         var name = Path.GetFileName(folder);
         var dest = Path.Combine(destFolder, name);

         RecursiveCopyFolder(folder, dest);
      }
   }

   public static void RecursiveDeleteFolder(string sourceFolder)
   {
      if (!Directory.Exists(sourceFolder))
      {
         return;
      }

      foreach (var folder in Directory.EnumerateDirectories(sourceFolder))
      {
         RecursiveDeleteFolder(folder);
      }

      Directory.Delete(sourceFolder, true);
   }
}
