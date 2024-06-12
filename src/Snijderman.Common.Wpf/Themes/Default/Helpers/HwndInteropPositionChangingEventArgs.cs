using System;

namespace Snijderman.Common.Wpf.Themes.Default.Helpers;

public class HwndInteropPositionChangingEventArgs
    : EventArgs
{
   public enum PositionChangeType
   {
      /// <summary>
      /// Draws a frame (defined in the window's class description) around the window. Same as the <see cref="Framechanged"/> flag.
      /// </summary>
      Drawframe = 0x0020,

      /// <summary>
      /// Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed.
      /// </summary>
      Framechanged = Drawframe,

      /// <summary>
      /// Hides the window.
      /// </summary>
      Hidewindow = 0x0080,

      /// <summary>
      /// Does not activate the window.
      /// </summary>
      Noactivate = 0x0010,

      /// <summary>
      /// Discards the entire contents of the client area.
      /// </summary>
      Nocopybits = 0x0100,

      /// <summary>
      /// Retains the current position (ignores the x and y members).
      /// </summary>
      Nomove = 0x0002,

      /// <summary>
      /// Does not change the owner window's position in the Z order.
      /// </summary>
      Noownerzorder = 0x0200,

      /// <summary>
      /// Does not redraw changes.
      /// </summary>
      SwpNoredraw = 0x0008,

      /// <summary>
      /// Does not change the owner window's position in the Z order. Same as the <see cref="Noownerzorder"/> flag.
      /// </summary>
      Noreposition = Noownerzorder,

      /// <summary>
      /// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
      /// </summary>
      Nosendchanging = 0x0400,

      /// <summary>
      /// Retains the current size (ignores the cx and cy members).
      /// </summary>
      Nosize = 0x0001,

      /// <summary>
      /// Retains the current Z order (ignores the hwndInsertAfter member).
      /// </summary>
      Nozorder = 0x0004,

      /// <summary>
      /// Displays the window.
      /// </summary>
      Showwindow = 0x0040,

      /// <summary>
      /// No official documentation found. Seems to occur whe maximizing or restoring a window.
      /// </summary>
      Maximizerestore = 0x8020,
   }

   public PositionChangeType Type { get; private set; }

   public HwndInteropPositionChangingEventArgs(PositionChangeType positionChangeType)
   {
      this.Type = positionChangeType;
   }
}
