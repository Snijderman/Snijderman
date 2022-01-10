using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Snijderman.Common.Utils
{
   public static class Cryptography
   {
      /// <summary>
      /// Creates a random byte array of 32 bytes
      /// </summary>
      /// <returns></returns>
      public static byte[] GenerateRandomBytes()
      {
         return GenerateRandomBytes(32);
      }

      /// <summary>
      /// Creates a random byte array
      /// </summary>
      /// <param name="byteCount">Size of the byte array</param>
      /// <returns></returns>
      public static byte[] GenerateRandomBytes(int byteCount)
      {
         var data = new byte[byteCount];

         using (var rng = new RNGCryptoServiceProvider())
         {
            for (var i = 0; i < 10; i++)
            {
               // Fille the buffer with the generated data
               rng.GetBytes(data);
            }
         }

         return data;
      }

      public static Rfc2898DeriveBytes CreateRfc2898DeriveBytes(byte[] password, byte[] salt)
      {
         return new Rfc2898DeriveBytes(password, salt, 1512);
      }

      public static RijndaelManaged CreateRijndaelManaged(Rfc2898DeriveBytes cryptoKey)
      {
         if (cryptoKey == null)
         {
            throw new ArgumentNullException(nameof(cryptoKey));
         }

         var rijndaelManaged = new RijndaelManaged
         {
            Mode = CipherMode.CBC,
            KeySize = 256,
            BlockSize = 128
         };
         // do not use this, because then a decrypted hash probably will not be the same as the original, since the sizes are different
         //rijndaelManaged.Padding = PaddingMode.Zeros;

         rijndaelManaged.Key = cryptoKey.GetBytes(rijndaelManaged.KeySize / 8); ;
         rijndaelManaged.IV = cryptoKey.GetBytes(rijndaelManaged.BlockSize / 8); ;

         return rijndaelManaged;
      }

      #region Encryption
      /// <summary>
      /// Create a writeable encryption stream with the specified algorithm
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <returns></returns>
      public static CryptoStream CreateEncryptionStream(System.IO.Stream stream, SymmetricAlgorithm algorithm)
      {
         return CreateEncryptionStream(stream, algorithm, CryptoStreamMode.Write, false);
      }

      /// <summary>
      /// Create a writeable encryption stream with the specified algorithm
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="leaveOpen">true to not close the underlying stream when the System.Security.Cryptography.CryptoStream object is disposed; otherwise, false.</param>
      /// <returns></returns>
      public static CryptoStream CreateEncryptionStream(System.IO.Stream stream, SymmetricAlgorithm algorithm, bool leaveOpen)
      {
         return CreateEncryptionStream(stream, algorithm, CryptoStreamMode.Write, leaveOpen);
      }

      /// <summary>
      /// Create a encryption stream with the specified algorithm
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="mode">The mode of the stream.</param>
      /// <returns></returns>
      public static CryptoStream CreateEncryptionStream(System.IO.Stream stream, SymmetricAlgorithm algorithm, CryptoStreamMode mode)
      {
         return CreateEncryptionStream(stream, algorithm, mode, false);
      }

      /// <summary>
      /// Create a encryption stream with the specified algorithm
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="mode">The mode of the stream.</param>
      /// <param name="leaveOpen">true to not close the underlying stream when the System.Security.Cryptography.CryptoStream object is disposed; otherwise, false.</param>
      /// <returns></returns>
      public static CryptoStream CreateEncryptionStream(System.IO.Stream stream, SymmetricAlgorithm algorithm, CryptoStreamMode mode, bool leaveOpen)
      {
         if (stream == null)
         {
            throw new ArgumentNullException(nameof(stream));
         }

         if (algorithm == null)
         {
            throw new ArgumentNullException(nameof(algorithm));
         }

         if (stream.CanSeek)
         {
            stream.Position = 0;
         }

         return Stream.CreateCryptoStream(stream, algorithm.CreateEncryptor(), mode, leaveOpen);
      }

      /// <summary>
      /// Create a writable encryption stream that can be processed in the callback action
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="callback">Callback action to process the stream</param>
      public static void EncryptStream(System.IO.Stream stream, SymmetricAlgorithm algorithm, Action<CryptoStream> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var writeCryptoStream = CreateEncryptionStream(stream, algorithm);
         callback(writeCryptoStream);
      }

      /// <summary>
      /// Create a writable encryption stream that can be processed in the callback action
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="leaveOpen">true to not close the underlying stream when the System.Security.Cryptography.CryptoStream object is disposed; otherwise, false.</param>
      /// <param name="callback">Callback action to process the stream</param>
      public static void EncryptStream(System.IO.Stream stream, SymmetricAlgorithm algorithm, bool leaveOpen, Action<CryptoStream> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var writeCryptoStream = CreateEncryptionStream(stream, algorithm, leaveOpen);
         callback(writeCryptoStream);
      }

      /// <summary>
      /// Create a encryption stream that can be processed in the callback action
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="mode">The mode of the stream.</param>
      /// <param name="callback">Callback action to process the stream</param>
      public static void EncryptStream(System.IO.Stream stream, SymmetricAlgorithm algorithm, CryptoStreamMode mode, Action<CryptoStream> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var writeCryptoStream = CreateEncryptionStream(stream, algorithm, mode, false);
         callback(writeCryptoStream);
      }

      /// <summary>
      /// Create a encryption stream that can be processed in the callback action
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="mode">The mode of the stream.</param>
      /// <param name="leaveOpen">true to not close the underlying stream when the System.Security.Cryptography.CryptoStream object is disposed; otherwise, false.</param>
      /// <param name="callback">Callback action to process the stream</param>
      public static void EncryptStream(System.IO.Stream stream, SymmetricAlgorithm algorithm, CryptoStreamMode mode, bool leaveOpen, Action<CryptoStream> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var writeCryptoStream = CreateEncryptionStream(stream, algorithm, mode, leaveOpen);
         callback(writeCryptoStream);
      }

      /// <summary>
      /// Asynchronously create a writable encryption stream that can be processed in the callback function
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="callback">Callback function to process the stream</param>
      /// <returns></returns>
      public static async Task EncryptStreamAsync(System.IO.Stream stream, SymmetricAlgorithm algorithm, Func<CryptoStream, Task> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var writeCryptoStream = CreateEncryptionStream(stream, algorithm);
         await callback(writeCryptoStream).ConfigureAwait(false);
      }

      /// <summary>
      /// Asynchronously create a writable encryption stream that can be processed in the callback function
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="leaveOpen">true to not close the underlying stream when the System.Security.Cryptography.CryptoStream object is disposed; otherwise, false.</param>
      /// <param name="callback">Callback function to process the stream</param>
      /// <returns></returns>
      public static async Task EncryptStreamAsync(System.IO.Stream stream, SymmetricAlgorithm algorithm, bool leaveOpen, Func<CryptoStream, Task> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var writeCryptoStream = CreateEncryptionStream(stream, algorithm, leaveOpen);
         await callback(writeCryptoStream).ConfigureAwait(false);
      }

      /// <summary>
      /// Asynchronously create a encryption stream that can be processed in the callback function
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="mode">The mode of the stream.</param>
      /// <param name="callback">Callback function to process the stream</param>
      /// <returns></returns>
      public static async Task EncryptStreamAsync(System.IO.Stream stream, SymmetricAlgorithm algorithm, CryptoStreamMode mode, Func<CryptoStream, Task> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var writeCryptoStream = CreateEncryptionStream(stream, algorithm, mode);
         await callback(writeCryptoStream).ConfigureAwait(false);
      }

      /// <summary>
      /// Asynchronously create a encryption stream that can be processed in the callback function
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="mode">The mode of the stream.</param>
      /// <param name="leaveOpen">true to not close the underlying stream when the System.Security.Cryptography.CryptoStream object is disposed; otherwise, false.</param>
      /// <param name="callback">Callback function to process the stream</param>
      /// <returns></returns>
      public static async Task EncryptStreamAsync(System.IO.Stream stream, SymmetricAlgorithm algorithm, CryptoStreamMode mode, bool leaveOpen, Func<CryptoStream, Task> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var writeCryptoStream = CreateEncryptionStream(stream, algorithm, mode, leaveOpen);
         await callback(writeCryptoStream).ConfigureAwait(false);
      }
      #endregion

      #region Decryption
      /// <summary>
      /// Create a readable decryption stream with the specified algorithm
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <returns></returns>
      public static CryptoStream CreateDecryptionStream(System.IO.Stream stream, SymmetricAlgorithm algorithm)
      {
         return CreateDecryptionStream(stream, algorithm, CryptoStreamMode.Read, false);
      }

      /// <summary>
      /// Create a readable decryption stream with the specified algorithm
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="leaveOpen">true to not close the underlying stream when the System.Security.Cryptography.CryptoStream object is disposed; otherwise, false.</param>
      /// <returns></returns>
      public static CryptoStream CreateDecryptionStream(System.IO.Stream stream, SymmetricAlgorithm algorithm, bool leaveOpen)
      {
         return CreateDecryptionStream(stream, algorithm, CryptoStreamMode.Read, leaveOpen);
      }

      /// <summary>
      /// Create a decryption stream with the specified algorithm
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="mode">The mode of the stream.</param>
      /// <returns></returns>
      public static CryptoStream CreateDecryptionStream(System.IO.Stream stream, SymmetricAlgorithm algorithm, CryptoStreamMode mode)
      {
         return CreateDecryptionStream(stream, algorithm, mode, false);
      }

      /// <summary>
      /// Create a decryption stream with the specified algorithm
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="mode">The mode of the stream.</param>
      /// <param name="leaveOpen">true to not close the underlying stream when the System.Security.Cryptography.CryptoStream object is disposed; otherwise, false.</param>
      /// <returns></returns>
      public static CryptoStream CreateDecryptionStream(System.IO.Stream stream, SymmetricAlgorithm algorithm, CryptoStreamMode mode, bool leaveOpen)
      {
         if (stream == null)
         {
            throw new ArgumentNullException(nameof(stream));
         }

         if (algorithm == null)
         {
            throw new ArgumentNullException(nameof(algorithm));
         }

         if (stream.CanSeek)
         {
            stream.Position = 0;
         }

         return Stream.CreateCryptoStream(stream, algorithm.CreateDecryptor(), mode, leaveOpen);
      }

      /// <summary>
      /// Create a readable decryption stream with the specified algorithm
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="callback">Callback function to process the stream</param>
      public static void DecryptStream(System.IO.Stream stream, SymmetricAlgorithm algorithm, Action<CryptoStream> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var readCryptoStream = CreateDecryptionStream(stream, algorithm);
         callback(readCryptoStream);
      }

      /// <summary>
      /// Create a readable decryption stream with the specified algorithm
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="leaveOpen">true to not close the underlying stream when the System.Security.Cryptography.CryptoStream object is disposed; otherwise, false.</param>
      /// <param name="callback">Callback function to process the stream</param>
      public static void DecryptStream(System.IO.Stream stream, SymmetricAlgorithm algorithm, bool leaveOpen, Action<CryptoStream> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var readCryptoStream = CreateDecryptionStream(stream, algorithm, leaveOpen);
         callback(readCryptoStream);
      }

      /// <summary>
      /// Create a decryption stream with the specified algorithm
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="mode">The mode of the stream.</param>
      /// <param name="callback">Callback function to process the stream</param>
      public static void DecryptStream(System.IO.Stream stream, SymmetricAlgorithm algorithm, CryptoStreamMode mode, Action<CryptoStream> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var readCryptoStream = CreateDecryptionStream(stream, algorithm, mode);
         callback(readCryptoStream);
      }

      /// <summary>
      /// Create a decryption stream with the specified algorithm
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="mode">The mode of the stream.</param>
      /// <param name="leaveOpen">true to not close the underlying stream when the System.Security.Cryptography.CryptoStream object is disposed; otherwise, false.</param>
      /// <param name="callback">Callback function to process the stream</param>
      public static void DecryptStream(System.IO.Stream stream, SymmetricAlgorithm algorithm, CryptoStreamMode mode, bool leaveOpen, Action<CryptoStream> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var readCryptoStream = CreateDecryptionStream(stream, algorithm, mode, leaveOpen);
         callback(readCryptoStream);
      }

      /// <summary>
      /// Asynchronously create a readable decryption stream that can be processed in the callback function
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="callback">Callback function to process the stream</param>
      /// <returns></returns>
      public static async Task DecryptStreamAsync(System.IO.Stream stream, SymmetricAlgorithm algorithm, Func<CryptoStream, Task> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var readCryptoStream = CreateDecryptionStream(stream, algorithm);
         await callback(readCryptoStream).ConfigureAwait(false);
      }

      /// <summary>
      /// Asynchronously create a readable decryption stream that can be processed in the callback function
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="leaveOpen">true to not close the underlying stream when the System.Security.Cryptography.CryptoStream object is disposed; otherwise, false.</param>
      /// <param name="callback">Callback function to process the stream</param>
      /// <returns></returns>
      public static async Task DecryptStreamAsync(System.IO.Stream stream, SymmetricAlgorithm algorithm, bool leaveOpen, Func<CryptoStream, Task> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var readCryptoStream = CreateDecryptionStream(stream, algorithm, leaveOpen);
         await callback(readCryptoStream).ConfigureAwait(false);
      }

      /// <summary>
      /// Asynchronously create a decryption stream that can be processed in the callback function
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="mode">The mode of the stream.</param>
      /// <param name="callback">Callback function to process the stream</param>
      /// <returns></returns>
      public static async Task DecryptStreamAsync(System.IO.Stream stream, SymmetricAlgorithm algorithm, CryptoStreamMode mode, Func<CryptoStream, Task> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var readCryptoStream = CreateDecryptionStream(stream, algorithm, mode);
         await callback(readCryptoStream).ConfigureAwait(false);
      }

      /// <summary>
      /// Asynchronously create a decryption stream that can be processed in the callback function
      /// </summary>
      /// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
      /// <param name="algorithm">The cryptographic transformation that is to be performed on the stream.</param>
      /// <param name="mode">The mode of the stream.</param>
      /// <param name="leaveOpen">true to not close the underlying stream when the System.Security.Cryptography.CryptoStream object is disposed; otherwise, false.</param>
      /// <param name="callback">Callback function to process the stream</param>
      /// <returns></returns>
      public static async Task DecryptStreamAsync(System.IO.Stream stream, SymmetricAlgorithm algorithm, CryptoStreamMode mode, bool leaveOpen, Func<CryptoStream, Task> callback)
      {
         if (callback == null)
         {
            throw new ArgumentNullException(nameof(callback));
         }

         using var readCryptoStream = CreateDecryptionStream(stream, algorithm, mode, leaveOpen);
         await callback(readCryptoStream).ConfigureAwait(false);
      }

      #endregion
   }
}
