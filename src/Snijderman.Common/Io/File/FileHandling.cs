using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Snijderman.Common.Io.File;

public abstract class FileHandling : IFileHandling
{
   protected readonly IFileIo FileIo;

   public FileHandling(IFileIo fileIo)
   {
      this.FileIo = fileIo;
   }

   public FileHandlingSettings Settings { get; set; }

   public virtual string InputFolder => Path.Combine(this.Settings.Folder);

   public virtual string ProcessedFolder => Path.Combine(this.Settings.Folder, "Processed");

   public virtual string ProcessedFolderForDay => Path.Combine(this.ProcessedFolder, $"{DateTime.Now:yyyyMMdd}");

   public virtual string FailedFolder => Path.Combine(this.Settings.Folder, "Failed");

   protected virtual void ValidateFolder(string folder)
   {
      if (!Directory.Exists(folder))
      {
         Directory.CreateDirectory(folder);
      }
   }

   public virtual void ValidateFolders()
   {
      if (this.Settings == null)
      {
         throw new InvalidOperationException("No file handling settings specified");
      }

      this.ValidateFolder(this.Settings.Folder);
      this.ValidateFolder(this.ProcessedFolder);
      this.ValidateFolder(this.FailedFolder);
   }

   /// <summary>
   /// Searches for new files
   /// Default only for current filder, no child folders are included
   /// </summary>
   /// <param name="inputFilesFolder"></param>
   /// <param name="fileMask"></param>
   /// <returns></returns>
   // only look in folder, not the child folders also
   public virtual List<string> ListExistingFiles(string inputFilesFolder, string fileMask) => this.ListExistingFiles(inputFilesFolder, fileMask, SearchOption.TopDirectoryOnly);

   public virtual List<string> ListExistingFiles(string inputFilesFolder, string fileMask, SearchOption searchOption) => Directory.GetFiles(inputFilesFolder, fileMask, searchOption).ToList();

   public virtual void MoveFileToProcessedFolder(string file)
   {
      this.FileIo.WaitForFileReady(file);
      this.MoveFile(file, Path.Combine(this.ProcessedFolderForDay, Path.GetFileName(file)));
   }

   public virtual void MoveFileToFailedFolder(string file)
   {
      this.FileIo.WaitForFileReady(file);
      this.MoveFile(file, Path.Combine(this.FailedFolder, Path.GetFileName(file)));
   }

   public virtual void MoveFile(string inputFile, string outputFile) => this.FileIo.MoveFile(inputFile, outputFile, true);
}
