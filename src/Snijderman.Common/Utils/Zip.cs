using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Snijderman.Common.Utils;

public static class Zip
{
   private const int BufferSize = 4096;

   /// <summary>
   /// Create a zip file from the provided file
   /// </summary>
   /// <param name="inputFile">Full name of input file (including path) to add to zip</param>
   /// <param name="outputFile">Full name of output file (including path) of zip archive</param>
   public static void CreateZipArchive(string inputFile, string outputFile)
   {
      var fInfo = new FileInfo(inputFile);
      if (!fInfo.Exists)
      {
         throw new IOException($"File {inputFile} is not available to add to zip archive.");
      }

      using var fs = new FileStream(outputFile, FileMode.Create);
      using (var zipArchive = new ZipArchive(fs, ZipArchiveMode.Create, true))
      {
         zipArchive.CreateEntryFromFile(inputFile, fInfo.Name, CompressionLevel.Optimal);
      }
      fs.Flush(true);
   }

   public static MemoryStream ZipStream(System.IO.Stream source, string bestandsNaam)
   {
      if (source == null)
      {
         throw new ArgumentNullException(nameof(source));
      }

      if (bestandsNaam == null)
      {
         throw new ArgumentNullException(nameof(bestandsNaam));
      }

      // can source set at position 0?
      // if not, assume the source is set at the correct position
      if (source.CanSeek)
      {
         source.Seek(0, SeekOrigin.Begin);
      }

      var ms = new MemoryStream();
      // Build the archive
      using (var zipArchive = new ZipArchive(ms, ZipArchiveMode.Create, true))
      {

         var archiveEntry = zipArchive.CreateEntry(bestandsNaam, CompressionLevel.Optimal);
         using var entryStream = archiveEntry.Open();
         Copy(source, entryStream, new byte[BufferSize]);
      }
      return ms;
   }

   public static async Task<MemoryStream> ZipStreamAsync(System.IO.Stream source, string bestandsNaam)
   {
      if (source == null)
      {
         throw new ArgumentNullException(nameof(source));
      }

      if (bestandsNaam == null)
      {
         throw new ArgumentNullException(nameof(bestandsNaam));
      }

      // can source set at position 0?
      // if not, assume the source is set at the correct position
      if (source.CanSeek)
      {
         source.Seek(0, SeekOrigin.Begin);
      }

      var ms = new MemoryStream();
      // Build the archive
      using (var zipArchive = new ZipArchive(ms, ZipArchiveMode.Create, true))
      {

         var archiveEntry = zipArchive.CreateEntry(bestandsNaam, CompressionLevel.Optimal);
         using var entryStream = archiveEntry.Open();
         await CopyAsync(source, entryStream, new byte[BufferSize]).ConfigureAwait(false);
      }
      return ms;
   }

   private static void Copy(System.IO.Stream source, System.IO.Stream destination, byte[] buffer)
   {
      var copying = true;

      while (copying)
      {
         var bytesRead = source.Read(buffer, 0, buffer.Length);
         if (bytesRead > 0)
         {
            destination.Write(buffer, 0, bytesRead);
         }
         else
         {
            destination.Flush();
            copying = false;
         }
      }
   }

   private static async Task CopyAsync(System.IO.Stream source, System.IO.Stream destination, byte[] buffer)
   {
      var copying = true;

      while (copying)
      {
         var bytesRead = await source.ReadAsync(buffer.AsMemory(0, buffer.Length)).ConfigureAwait(false);
         if (bytesRead > 0)
         {
            await destination.WriteAsync(buffer.AsMemory(0, bytesRead)).ConfigureAwait(false);
         }
         else
         {
            destination.Flush();
            copying = false;
         }
      }
   }

   public static void Unzip(byte[] zipBytes, string bestandsNaam, bool overwrite = true)
   {
      if (zipBytes == null)
      {
         throw new ArgumentNullException(nameof(zipBytes));
      }

      if (bestandsNaam == null)
      {
         throw new ArgumentNullException(nameof(bestandsNaam));
      }

      using var ms = new MemoryStream();
      ms.Write(zipBytes, 0, zipBytes.Length);
      ms.Position = 0;
      using var zipArchive = new ZipArchive(ms, ZipArchiveMode.Read);
      var archiveEntry = zipArchive.Entries[0];
      archiveEntry.ExtractToFile(bestandsNaam, overwrite);
   }

   public static async Task UnzipAsync(System.IO.Stream zippedStream, Func<System.IO.Stream, Task> unzipCallback)
   {
      if (zippedStream == null)
      {
         throw new ArgumentNullException(nameof(zippedStream));
      }

      if (unzipCallback == null)
      {
         throw new ArgumentNullException(nameof(unzipCallback));
      }

      using var zipArchive = new ZipArchive(zippedStream, ZipArchiveMode.Read);
      var archiveEntry = zipArchive.Entries[0];
      using var unzippedEntryStream = archiveEntry.Open();
      using var ms = new MemoryStream();
      await unzippedEntryStream.CopyToAsync(ms).ConfigureAwait(false);
      ms.Position = 0;
      await unzipCallback(ms).ConfigureAwait(false);
   }
}
