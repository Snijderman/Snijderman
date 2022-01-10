using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Snijderman.Common.Utils
{
   public static class Stream
   {
      public static System.IO.Stream GenerateStreamFromString(string s)
      {
         var stream = new MemoryStream();
         // cannot use using here, because the output stream is then closed also
         var writer = new StreamWriter(stream);
         writer.Write(s);
         writer.Flush();
         stream.Position = 0;

         return stream;
      }

      public static byte[] ReadAllBytes(System.IO.Stream input)
      {
         if (input == null)
         {
            throw new ArgumentNullException(nameof(input));
         }

         if (input.CanSeek)
         {
            input.Position = 0;
         }
         using var ms = new MemoryStream();
         input.CopyTo(ms);
         return ms.ToArray();
      }

      public static async Task<byte[]> ReadAllBytesAsync(System.IO.Stream input)
      {
         if (input == null)
         {
            throw new ArgumentNullException(nameof(input));
         }

         if (input.CanSeek)
         {
            input.Position = 0;
         }

         using var ms = new MemoryStream();
         await input.CopyToAsync(ms).ConfigureAwait(false);
         return ms.ToArray();
      }


      /// <summary>
      /// Initializes a new instance of the System.Security.Cryptography.CryptoStream class.
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="transform">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="mode">The mode of the stream.</param>
      /// <returns></returns>
      public static CryptoStream CreateCryptoStream(System.IO.Stream stream, ICryptoTransform transform, CryptoStreamMode mode)
      {
         return CreateCryptoStream(stream, transform, mode, false);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="transform">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="mode">The mode of the stream.</param>
      /// <param name="leaveOpen"></param>
      /// <param name="leaveOpen">true to not close the underlying stream when the System.Security.Cryptography.CryptoStream object is disposed; otherwise, false.</param>
      /// <returns></returns>
      public static CryptoStream CreateCryptoStream(System.IO.Stream stream, ICryptoTransform transform, CryptoStreamMode mode, bool leaveOpen)
      {
         return new CryptoStream(stream, transform, mode, leaveOpen);
      }
   }
}
