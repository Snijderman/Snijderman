using System;
using System.IO;

namespace Snijderman.Common.Io;

public class LargeMemoryStream : Stream
{
   #region Fields

   private const int PAGE_SIZE = 1024000;
   private const int ALLOC_STEP = 1024;

   private byte[][] _streamBuffers;

   private int _pageCount;
   private long _allocatedBytes;

   private long _position;
   private long _length;

   #endregion Fields

   #region Internals

   private int GetPageCount(long length)
   {
      var pageCount = (int)(length / PAGE_SIZE) + 1;

      if ((length % PAGE_SIZE) == 0)
      {
         pageCount--;
      }

      return pageCount;
   }

   private void ExtendPages()
   {
      if (this._streamBuffers == null)
      {
         this._streamBuffers = new byte[ALLOC_STEP][];
      }
      else
      {
         var streamBuffers = new byte[this._streamBuffers.Length + ALLOC_STEP][];

         Array.Copy(this._streamBuffers, streamBuffers, this._streamBuffers.Length);

         this._streamBuffers = streamBuffers;
      }

      this._pageCount = this._streamBuffers.Length;
   }

   private void AllocSpaceIfNeeded(long value)
   {
      if (value < 0)
      {
         throw new InvalidOperationException("AllocSpaceIfNeeded < 0");
      }

      if (value == 0)
      {
         return;
      }

      var currentPageCount = this.GetPageCount(this._allocatedBytes);
      var neededPageCount = this.GetPageCount(value);

      while (currentPageCount < neededPageCount)
      {
         if (currentPageCount == this._pageCount)
         {
            this.ExtendPages();
         }

         this._streamBuffers[currentPageCount++] = new byte[PAGE_SIZE];
      }

      this._allocatedBytes = (long)currentPageCount * PAGE_SIZE;

      value = Math.Max(value, this._length);

      if (this._position > (this._length = value))
      {
         this._position = this._length;
      }
   }

   #endregion Internals

   #region Stream

   public override bool CanRead => true;

   public override bool CanSeek => true;

   public override bool CanWrite => true;

   public override long Length => this._length;

   public override long Position
   {
      get => this._position;
      set
      {
         if (value > this._length)
         {
            throw new InvalidOperationException("Position > Length");
         }
         else if (value < 0)
         {
            throw new InvalidOperationException("Position < 0");
         }
         else
         {
            this._position = value;
         }
      }
   }

   public override void Flush() { }

   public override int Read(byte[] buffer, int offset, int count)
   {
      var currentPage = (int)(this._position / PAGE_SIZE);
      var currentOffset = (int)(this._position % PAGE_SIZE);
      var currentLength = PAGE_SIZE - currentOffset;

      var startPosition = this._position;

      if (startPosition + count > this._length)
      {
         count = (int)(this._length - startPosition);
      }

      while (count != 0 && this._position < this._length)
      {
         if (currentLength > count)
         {
            currentLength = count;
         }

         Array.Copy(this._streamBuffers[currentPage++], currentOffset, buffer, offset, currentLength);

         offset += currentLength;
         this._position += currentLength;
         count -= currentLength;

         currentOffset = 0;
         currentLength = PAGE_SIZE;
      }

      return (int)(this._position - startPosition);
   }

   public override long Seek(long offset, SeekOrigin origin)
   {
      switch (origin)
      {
         case SeekOrigin.Begin:
            break;

         case SeekOrigin.Current:
            offset += this._position;
            break;

         case SeekOrigin.End:
            offset = this._length - offset;
            break;

         default:
            throw new ArgumentOutOfRangeException(nameof(origin));
      }

      return this.Position = offset;
   }

   public override void SetLength(long value)
   {
      if (value < 0)
      {
         throw new InvalidOperationException("SetLength < 0");
      }

      if (value == 0)
      {
         this._streamBuffers = null;
         this._allocatedBytes = this._position = this._length = 0;
         this._pageCount = 0;
         return;
      }

      var currentPageCount = this.GetPageCount(this._allocatedBytes);
      var neededPageCount = this.GetPageCount(value);

      // Removes unused buffers if decreasing stream length
      while (currentPageCount > neededPageCount)
      {
         this._streamBuffers[--currentPageCount] = null;
      }

      this.AllocSpaceIfNeeded(value);

      if (this._position > (this._length = value))
      {
         this._position = this._length;
      }
   }

   public override void Write(byte[] buffer, int offset, int count)
   {
      var currentPage = (int)(this._position / PAGE_SIZE);
      var currentOffset = (int)(this._position % PAGE_SIZE);
      var currentLength = PAGE_SIZE - currentOffset;

      this.AllocSpaceIfNeeded(this._position + count);

      while (count != 0)
      {
         if (currentLength > count)
         {
            currentLength = count;
         }

         Array.Copy(buffer, offset, this._streamBuffers[currentPage++], currentOffset, currentLength);

         offset += currentLength;
         this._position += currentLength;
         count -= currentLength;

         currentOffset = 0;
         currentLength = PAGE_SIZE;
      }
   }

   #endregion Stream
}
