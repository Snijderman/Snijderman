using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;

namespace Snijderman.Common.Wpf.Themes.Default.Helpers;

/// <summary>
/// Represents a display device or multiple display devices on a single system.
/// See https://github.com/micdenny/WpfScreenHelper/
/// </summary>
internal class ScreenInterop
{
   [DllImport("user32.dll", CharSet = CharSet.Auto)]
   public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out] Monitorinfoex info);

   [DllImport("user32.dll", ExactSpelling = true)]
   public static extern bool EnumDisplayMonitors(HandleRef hdc, Comrect rcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

   [DllImport("user32.dll", ExactSpelling = true)]
   public static extern IntPtr MonitorFromWindow(HandleRef handle, int flags);

   [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
   public static extern int GetSystemMetrics(int nIndex);

   [DllImport("user32.dll", CharSet = CharSet.Auto)]
   public static extern bool SystemParametersInfo(int nAction, int nParam, ref Rect rc, int nUpdate);

   [DllImport("user32.dll", ExactSpelling = true)]
   public static extern IntPtr MonitorFromPoint(Pointstruct pt, int flags);

   [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
   public static extern bool GetCursorPos([In, Out] Point pt);

#pragma warning disable CS0649
   public static HandleRef NullHandleRef;
#pragma warning restore CS0649

   public delegate bool MonitorEnumProc(IntPtr monitor);

   [StructLayout(LayoutKind.Sequential)]
   public struct Rect
   {
      public int left;
      public int top;
      public int right;
      public int bottom;

      public Rect(int left, int top, int right, int bottom)
      {
         this.left = left;
         this.top = top;
         this.right = right;
         this.bottom = bottom;
      }

      public Rect(System.Windows.Rect r)
      {
         this.left = (int)r.Left;
         this.top = (int)r.Top;
         this.right = (int)r.Right;
         this.bottom = (int)r.Bottom;
      }

      public static Rect FromXywh(int x, int y, int width, int height) => new(x, y, x + width, y + height);

      public Size Size => new(this.right - this.left, this.bottom - this.top);
   }

   // use this in cases where the Native API takes a POINT not a POINT*
   // classes marshal by ref.
   [StructLayout(LayoutKind.Sequential)]
   public struct Pointstruct
   {
      public int x;
      public int y;
      public Pointstruct(int x, int y)
      {
         this.x = x;
         this.y = y;
      }
   }

   [StructLayout(LayoutKind.Sequential)]
   public class Point
   {
      public int x;
      public int y;

      public Point()
      {
      }

      public Point(int x, int y)
      {
         this.x = x;
         this.y = y;
      }

#if DEBUG
      public override string ToString()
      {
         return "{x=" + this.x + ", y=" + this.y + "}";
      }
#endif
   }

   [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
   public class Monitorinfoex
   {
      internal int _cbSize = Marshal.SizeOf(typeof(Monitorinfoex));
      internal Rect _rcMonitor = new();
      internal Rect _rcWork = new();
      internal int _dwFlags = 0;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)] internal char[] _szDevice = new char[32];
   }

   [StructLayout(LayoutKind.Sequential)]
   public class Comrect
   {
      public int left;
      public int top;
      public int right;
      public int bottom;

      public Comrect()
      {
      }

      public Comrect(System.Windows.Rect r)
      {
         this.left = (int)r.X;
         this.top = (int)r.Y;
         this.right = (int)r.Right;
         this.bottom = (int)r.Bottom;
      }

      public Comrect(int left, int top, int right, int bottom)
      {
         this.left = left;
         this.top = top;
         this.right = right;
         this.bottom = bottom;
      }

      public static Comrect FromXywh(int x, int y, int width, int height) => new(x, y, x + width, y + height);

      public override string ToString() => "Left = " + this.left + " Top " + this.top + " Right = " + this.right + " Bottom = " + this.bottom;
   }

   public const int SmCmonitors = 80;

   private readonly IntPtr _hmonitor;

   // This identifier is just for us, so that we don't try to call the multimon
   // functions if we just need the primary monitor... this is safer for
   // non-multimon OSes.
   private const int PrimaryMonitor = unchecked((int)0xBAADF00D);

   private const int MonitorinfofPrimary = 0x00000001;

   private const int MonitorDefaulttonearest = 0x00000002;

   private static readonly bool _multiMonitorSupport;

   static ScreenInterop()
   {
      _multiMonitorSupport = GetSystemMetrics(SmCmonitors) != 0;
   }

   private ScreenInterop(IntPtr monitor)
   {
      if (!_multiMonitorSupport || monitor == PrimaryMonitor)
      {
         this.Bounds = new System.Windows.Rect(SystemParameters.VirtualScreenLeft, SystemParameters.VirtualScreenTop, SystemParameters.VirtualScreenWidth, SystemParameters.VirtualScreenHeight);
         this.Primary = true;
         this.DeviceName = "DISPLAY";
      }
      else
      {
         var info = new Monitorinfoex();

         GetMonitorInfo(new HandleRef(null, monitor), info);

         this.Bounds = new System.Windows.Rect(
             info._rcMonitor.left, info._rcMonitor.top,
             info._rcMonitor.right - info._rcMonitor.left,
             info._rcMonitor.bottom - info._rcMonitor.top);

         this.Primary = ((info._dwFlags & MonitorinfofPrimary) != 0);

         this.DeviceName = new string(info._szDevice).TrimEnd((char)0);
      }
      this._hmonitor = monitor;
   }

   /// <summary>
   /// Gets an array of all displays on the system.
   /// </summary>
   /// <returns>An enumerable of type Screen, containing all displays on the system.</returns>
   public static IEnumerable<ScreenInterop> AllScreens
   {
      get
      {
         if (_multiMonitorSupport)
         {
            var closure = new MonitorEnumCallback();
            var proc = new MonitorEnumProc(closure.Callback);

            EnumDisplayMonitors(NullHandleRef, null, proc, IntPtr.Zero);

            if (closure.Screens.Count > 0)
            {
               return closure.Screens.Cast<ScreenInterop>();
            }
         }

         return new[] { new ScreenInterop(PrimaryMonitor) };
      }
   }

   /// <summary>
   /// Gets the bounds of the display.
   /// </summary>
   /// <returns>A <see cref="T:System.Windows.Rect" />, representing the bounds of the display.</returns>
   public System.Windows.Rect Bounds { get; private set; }

   /// <summary>
   /// Gets the device name associated with a display.
   /// </summary>
   /// <returns>The device name associated with a display.</returns>
   public string DeviceName { get; private set; }

   /// <summary>
   /// Gets a value indicating whether a particular display is the primary device.
   /// </summary>
   /// <returns>true if this display is primary; otherwise, false.</returns>
   public bool Primary { get; private set; }

   /// <summary>
   /// Gets the primary display.
   /// </summary>
   /// <returns>The primary display.</returns>
   public static ScreenInterop PrimaryScreen
   {
      get
      {
         if (_multiMonitorSupport)
         {
            return AllScreens.FirstOrDefault(t => t.Primary);
         }
         return new ScreenInterop(PrimaryMonitor);
      }
   }

   /// <summary>
   /// Gets the working area of the display. The working area is the desktop area of the display, excluding taskbars, docked windows, and docked tool bars.
   /// </summary>
   /// <returns>A <see cref="T:System.Windows.Rect" />, representing the working area of the display.</returns>
   public System.Windows.Rect WorkingArea
   {
      get
      {
         if (!_multiMonitorSupport || this._hmonitor == PrimaryMonitor)
         {
            return SystemParameters.WorkArea;
         }
         var info = new Monitorinfoex();
         GetMonitorInfo(new HandleRef(null, this._hmonitor), info);
         return new System.Windows.Rect(
             info._rcWork.left, info._rcWork.top,
             info._rcWork.right - info._rcWork.left,
             info._rcWork.bottom - info._rcWork.top);
      }
   }

   /// <summary>
   /// Retrieves a Screen for the display that contains the largest portion of the specified control.
   /// </summary>
   /// <param name="hwnd">The window handle for which to retrieve the Screen.</param>
   /// <returns>A Screen for the display that contains the largest region of the object. In multiple display environments where no display contains any portion of the specified window, the display closest to the object is returned.</returns>
   public static ScreenInterop FromHandle(IntPtr hwnd)
   {
      if (_multiMonitorSupport)
      {
         return new ScreenInterop(MonitorFromWindow(new HandleRef(null, hwnd), 2));
      }
      return new ScreenInterop(PrimaryMonitor);
   }

   /// <summary>
   /// Retrieves a Screen for the display that contains the specified point.
   /// </summary>
   /// <param name="point">A <see cref="T:System.Windows.Point" /> that specifies the location for which to retrieve a Screen.</param>
   /// <returns>A Screen for the display that contains the point. In multiple display environments where no display contains the point, the display closest to the specified point is returned.</returns>
   public static ScreenInterop FromPoint(System.Windows.Point point)
   {
      if (_multiMonitorSupport)
      {
         var pt = new Pointstruct((int)point.X, (int)point.Y);
         return new ScreenInterop(MonitorFromPoint(pt, MonitorDefaulttonearest));
      }
      return new ScreenInterop(PrimaryMonitor);
   }

   /// <summary>
   /// Gets or sets a value indicating whether the specified object is equal to this Screen.
   /// </summary>
   /// <param name="obj">The object to compare to this Screen.</param>
   /// <returns>true if the specified object is equal to this Screen; otherwise, false.</returns>
   public override bool Equals(object obj) => obj is ScreenInterop monitor && this._hmonitor == monitor._hmonitor;

   /// <summary>
   /// Computes and retrieves a hash code for an object.
   /// </summary>
   /// <returns>A hash code for an object.</returns>
   public override int GetHashCode() => (int)this._hmonitor;

   private class MonitorEnumCallback
   {
      public ArrayList Screens { get; private set; }

      public MonitorEnumCallback()
      {
         this.Screens = new ArrayList();
      }

      public bool Callback(IntPtr monitor)
      {
         this.Screens.Add(new ScreenInterop(monitor));
         return true;
      }
   }
}
