using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Snijderman.Common.Wpf.Helpers;

public static class ImagesHelper
{
   public static byte[] ImageSourceToBytes(BitmapEncoder encoder, ImageSource imageSource)
   {
      if (!(imageSource is BitmapSource))
      {
         return default;
      }

      using var stream = ImageSourceToStream(encoder, imageSource);
      return stream.ToArray();
   }

   public static MemoryStream ImageSourceToStream(BitmapEncoder encoder, ImageSource imageSource)
   {
      if (imageSource is not BitmapSource bitmapSource)
      {
         return default;
      }

      encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

      var stream = new MemoryStream();
      encoder.Save(stream);

      return stream;
   }
}
