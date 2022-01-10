using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Snijderman.Common.Utils
{
   public static class Hashing
   {
      public static string ComputeHash(string text)
      {
         byte[] hashBytes;
         var bytes = Encoding.UTF8.GetBytes(text);
         using (var hash = GetHashAlgorithm())
         {
            hashBytes = hash.ComputeHash(bytes);
         }

         return HashBytesToHashValue(hashBytes);
      }

      public static string HashBytesToHashValue(byte[] hashBytes)
      {
         return BitConverter.ToString(hashBytes).Replace("-", "", StringComparison.OrdinalIgnoreCase).ToUpperInvariant();
      }

      public static string ComputeHashForFile(string file)
      {
         if (file == null)
         {
            throw new ArgumentNullException(nameof(file));
         }

         if (!File.Exists(file))
         {
            throw new InvalidOperationException($"Het bestand {file} is niet op het systeem aanwezig");
         }

         byte[] hashBytes;
         using (var hash = GetHashAlgorithm())
         {
            using var stream = IO.OpenFileWithoutLocking(file, Convert.KilobytesFromMegabytes(16));
            hashBytes = hash.ComputeHash(stream);
         }

         return HashBytesToHashValue(hashBytes);
      }

      public static string ComputeHashForStream(System.IO.Stream stream)
      {
         using var hash = GetHashAlgorithm();
         return ComputeHashForStream(stream, hash);
      }

      public static string ComputeHashForStream(System.IO.Stream stream, HashAlgorithm algorithm)
      {
         if (stream == null)
         {
            throw new ArgumentNullException(nameof(stream));
         }
         if (algorithm is null)
         {
            throw new ArgumentNullException(nameof(algorithm));
         }

         if (stream.CanSeek)
         {
            stream.Position = 0;
         }

         return HashBytesToHashValue(algorithm.ComputeHash(stream));
      }

      private static HashAlgorithm GetHashAlgorithm()
      {
         return new SHA256Managed();
      }

      #region Creating stream
      public static CryptoStream CreateHashingStream(System.IO.Stream stream, HashAlgorithm transform)
      {
         return CreateHashingStream(stream, transform, CryptoStreamMode.Write, false);
      }

      public static CryptoStream CreateHashingStream(System.IO.Stream stream, HashAlgorithm transform, bool leaveOpen)
      {
         return CreateHashingStream(stream, transform, CryptoStreamMode.Write, leaveOpen);
      }

      public static CryptoStream CreateHashingStream(System.IO.Stream stream, HashAlgorithm transform, CryptoStreamMode mode)
      {
         return CreateHashingStream(stream, transform, mode, false);
      }

      public static CryptoStream CreateHashingStream(System.IO.Stream stream, HashAlgorithm transform, CryptoStreamMode mode, bool leaveOpen)
      {
         return Stream.CreateCryptoStream(stream, transform, mode, leaveOpen);
      }
      #endregion


      public static void HashStream(System.IO.Stream outputStream, HashAlgorithm transform, Action<CryptoStream> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var writeHashStream = CreateHashingStream(outputStream, transform);
         callback(writeHashStream);
      }

      public static void HashStream(System.IO.Stream outputStream, HashAlgorithm transform, bool leaveOpen, Action<CryptoStream> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var writeHashStream = CreateHashingStream(outputStream, transform, leaveOpen);
         callback(writeHashStream);
      }

      public static void HashStream(System.IO.Stream outputStream, HashAlgorithm transform, CryptoStreamMode mode, Action<CryptoStream> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var writeHashStream = CreateHashingStream(outputStream, transform, mode);
         callback(writeHashStream);
      }

      public static void HashStream(System.IO.Stream outputStream, HashAlgorithm transform, CryptoStreamMode mode, bool leaveOpen, Action<CryptoStream> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var writeHashStream = CreateHashingStream(outputStream, transform, mode, leaveOpen);
         callback(writeHashStream);
      }

      public static async Task HashStreamAsync(System.IO.Stream outputStream, HashAlgorithm transform, Func<CryptoStream, Task> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var writeHashStream = CreateHashingStream(outputStream, transform);
         await callback(writeHashStream).ConfigureAwait(false);
      }

      public static async Task HashStreamAsync(System.IO.Stream outputStream, HashAlgorithm transform, bool leaveOpen, Func<CryptoStream, Task> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var writeHashStream = CreateHashingStream(outputStream, transform, leaveOpen);
         await callback(writeHashStream).ConfigureAwait(false);
      }

      public static async Task HashStreamAsync(System.IO.Stream outputStream, HashAlgorithm transform, CryptoStreamMode mode, Func<CryptoStream, Task> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var writeHashStream = CreateHashingStream(outputStream, transform, mode);
         await callback(writeHashStream).ConfigureAwait(false);
      }

      public static async Task HashStreamAsync(System.IO.Stream outputStream, HashAlgorithm transform, CryptoStreamMode mode, bool leaveOpen, Func<CryptoStream, Task> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var writeHashStream = CreateHashingStream(outputStream, transform, mode, leaveOpen);
         await callback(writeHashStream).ConfigureAwait(false);
      }


   }
}
