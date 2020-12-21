using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Snijderman.Common.Wpf.Themes.Default.Helpers;
using Brush = System.Windows.Media.Brush;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace Snijderman.Common.Wpf.Themes.Default.Controls
{
   /// <summary>
   /// Window with custom chrome supporting theming of non-client areas
   /// </summary>
   [TemplatePart(Name = PART_DragMoveThumb, Type = typeof(FrameworkElement))]
   [TemplatePart(Name = PART_IconPresenter, Type = typeof(FrameworkElement))]
   [TemplatePart(Name = PART_MinimizeButton, Type = typeof(Button))]
   [TemplatePart(Name = PART_MaximizeRestoreButton, Type = typeof(Button))]
   [TemplatePart(Name = PART_CloseButton, Type = typeof(Button))]
   public class ThemedWindow : Window
   {
      private const string PART_DragMoveThumb = "PART_DragMoveThumb";
      private const string PART_IconPresenter = "PART_IconPresenter";
      private const string PART_MinimizeButton = "PART_MinimizeButton";
      private const string PART_MaximizeRestoreButton = "PART_MaximizeRestoreButton";
      private const string PART_CloseButton = "PART_CloseButton";

      static ThemedWindow()
      {
         DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedWindow), new FrameworkPropertyMetadata(typeof(ThemedWindow)));
         IconProperty.OverrideMetadata(typeof(ThemedWindow), new FrameworkPropertyMetadata(OnIconPropertyChanged));
      }


      protected HwndInterop HwndInterop { get; private set; }

      public FrameworkElement DragMoveThumb { get; protected set; }

      public FrameworkElement IconPresenter { get; protected set; }

      public Button MinimizeButton { get; protected set; }

      public Button MaximizeRestoreButton { get; protected set; }

      public Button CloseButton { get; protected set; }

      /// <summary>
      /// Gets or sets the visibility of the icon component of the window.
      /// </summary>
      public Visibility IconVisibility
      {
         get => (Visibility)this.GetValue(IconVisibilityProperty);
         set => this.SetValue(IconVisibilityProperty, value);
      }

      /// <summary>
      /// Gets or sets the window's icon as <see cref="ImageSource">ImageSource</see>.
      /// When the <see cref="Window.IconProperty">IconProperty</see> property changes, this is updated accordingly.
      /// </summary>
      protected internal ImageSource IconSource
      {
         get => (ImageSource)this.GetValue(IconSourceProperty);
         set => this.SetValue(IconSourceProperty, value);
      }

      /// <summary>
      /// Gets or sets the content of the window's title bar
      /// between the title and the window buttons.
      /// </summary>
      public object TitleBarContent
      {
         get => (object)this.GetValue(TitleBarContentProperty);
         set => this.SetValue(TitleBarContentProperty, value);
      }

      /// <summary>
      /// Gets or sets the foreground brush of the window's title bar.
      /// </summary>
      public Brush TitleBarForeground
      {
         get => (Brush)this.GetValue(TitleBarForegroundProperty);
         set => this.SetValue(TitleBarForegroundProperty, value);
      }

      /// <summary>
      /// Gets or sets the background brush of the window's title bar.
      /// </summary>
      public Brush TitleBarBackground
      {
         get => (Brush)this.GetValue(TitleBarBackgroundProperty);
         set => this.SetValue(TitleBarBackgroundProperty, value);
      }

      /// <summary>
      /// Gets or sets the background brush of the minimize, maximize and restore
      /// buttons when they are hovered.
      /// </summary>
      public Brush WindowButtonHighlightBrush
      {
         get => (Brush)this.GetValue(WindowButtonHighlightBrushProperty);
         set => this.SetValue(WindowButtonHighlightBrushProperty, value);
      }

      /// <summary>
      /// Gets the size of the display overlapping area when the window is maximized.
      /// </summary>
      protected internal Thickness MaximizeBorderThickness
      {
         get => (Thickness)this.GetValue(MaximizeBorderThicknessProperty);
         private set => this.SetValue(MaximizeBorderThicknessPropertyKey, value);
      }

      public static readonly DependencyProperty IconVisibilityProperty = DependencyProperty.Register("IconVisibility", typeof(Visibility), typeof(ThemedWindow), new PropertyMetadata(Visibility.Visible));

      protected internal static readonly DependencyProperty IconSourceProperty = DependencyProperty.Register("IconSource", typeof(ImageSource), typeof(ThemedWindow), new PropertyMetadata(null));

      public static readonly DependencyProperty TitleBarContentProperty = DependencyProperty.Register("TitleBarContent", typeof(object), typeof(ThemedWindow), new PropertyMetadata(null));

      public static readonly DependencyProperty TitleBarForegroundProperty = DependencyProperty.Register("TitleBarForeground", typeof(Brush), typeof(ThemedWindow), new PropertyMetadata(null));

      public static readonly DependencyProperty TitleBarBackgroundProperty = DependencyProperty.Register("TitleBarBackground", typeof(Brush), typeof(ThemedWindow), new PropertyMetadata(null));

      public static readonly DependencyProperty WindowButtonHighlightBrushProperty = DependencyProperty.Register("WindowButtonHighlightBrush", typeof(Brush), typeof(ThemedWindow), new PropertyMetadata(null));

      protected internal static readonly DependencyPropertyKey MaximizeBorderThicknessPropertyKey = DependencyProperty.RegisterReadOnly("MaximizeBorderThickness", typeof(Thickness), typeof(ThemedWindow), new PropertyMetadata(new Thickness()));

      protected internal static readonly DependencyProperty MaximizeBorderThicknessProperty = MaximizeBorderThicknessPropertyKey.DependencyProperty;

      private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         if (d is not ThemedWindow sourceWindow)
         {
            return;
         }

         var newIcon = e.NewValue.ToString();

         sourceWindow.IconSource = string.IsNullOrEmpty(newIcon) ? null : new BitmapImage(new Uri(newIcon));
      }

      /// <inheritdoc/>
      public ThemedWindow()
      {
         this.IconSource = this.GetApplicationIcon();
         this.MaximizeBorderThickness = this.GetSystemMaximizeBorderThickness();
      }

      private BitmapSource GetApplicationIcon()
      {
         var appIcon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly()?.ManifestModule.FullyQualifiedName);

         if (appIcon == null)
         {
            return null;
         }

         return Imaging.CreateBitmapSourceFromHIcon(appIcon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
      }

      private Thickness GetSystemMaximizeBorderThickness()
      {
         var frameThickness = SystemParameters.WindowNonClientFrameThickness;
         var resizeBorderThickness = SystemParameters.WindowResizeBorderThickness;

         return new Thickness(
             frameThickness.Left + resizeBorderThickness.Left - 1,
             frameThickness.Top + resizeBorderThickness.Top - SystemParameters.CaptionHeight - 1,
             frameThickness.Right + resizeBorderThickness.Right - 1,
             frameThickness.Bottom + resizeBorderThickness.Bottom - 1);
      }

      /// <inheritdoc/>
      public override void OnApplyTemplate()
      {
         base.OnApplyTemplate();

         this.HwndInterop = new HwndInterop(this);

         this.DragMoveThumb = this.GetTemplateChild(PART_DragMoveThumb) as FrameworkElement;
         this.IconPresenter = this.GetTemplateChild(PART_IconPresenter) as FrameworkElement;
         this.MinimizeButton = this.GetTemplateChild(PART_MinimizeButton) as Button;
         this.MaximizeRestoreButton = this.GetTemplateChild(PART_MaximizeRestoreButton) as Button;
         this.CloseButton = this.GetTemplateChild(PART_CloseButton) as Button;

         if (this.DragMoveThumb != null)
         {
            this.InitDragMoveThumb(this.DragMoveThumb);
         }

         if (this.IconPresenter != null)
         {
            this.InitIconPresenter(this.IconPresenter);
         }

         if (this.MinimizeButton != null)
         {
            this.InitMinimizeButton(this.MinimizeButton);
         }

         if (this.MaximizeRestoreButton != null)
         {
            this.InitMaximizeRestoreButton(this.MaximizeRestoreButton);
         }

         if (this.CloseButton != null)
         {
            this.InitCloseButton(this.CloseButton);
         }

         this.UpdateLayoutForSizeToContent();
         this.HwndInterop.PositionChanging += this.DisableSizeToContentWhenMaximizing;
      }

      /// <summary>
      /// Initializes functionality of the drag/move thumb component of the window's title bar.
      /// </summary>
      /// <param name="dragMoveThumb">The drag/move thumb component of the window</param>
      protected virtual void InitDragMoveThumb(FrameworkElement dragMoveThumb)
      {
         dragMoveThumb.MouseLeftButtonDown += (s, e) =>
         {
            if (e.ChangedButton == MouseButton.Left)
            {
               if (this.WindowState == WindowState.Maximized)
               {
                  dragMoveThumb.MouseMove += this.RestoreOnMouseMove;
               }

               if (Mouse.LeftButton == MouseButtonState.Pressed)
               {
                  this.DragMove();
               }
            }

            if (e.ClickCount == 2 &&
                (this.ResizeMode == ResizeMode.CanResize || this.ResizeMode == ResizeMode.CanResizeWithGrip))
            {
               this.ToggleWindowState();
            }
         };

         dragMoveThumb.MouseLeftButtonUp += (s, e) => dragMoveThumb.MouseMove -= this.RestoreOnMouseMove;

         dragMoveThumb.MouseRightButtonUp += (s, e) => this.OpenSystemContextMenu(e.GetPosition(this));
      }

      /// <summary>
      /// Initializes functionality of the minimize button of the window's title bar.
      /// </summary>
      /// <param name="minimizeButton">The minimize button of the window</param>
      protected virtual void InitMinimizeButton(Button minimizeButton)
      {
         minimizeButton.Click += this.MinimizeClick;
      }

      /// <summary>
      /// Initializes functionality of the maximize/restore button of the window's title bar.
      /// </summary>
      /// <param name="maximizeRestoreButton">The maximize/restore button of the window</param>
      protected virtual void InitMaximizeRestoreButton(Button maximizeRestoreButton)
      {
         maximizeRestoreButton.Click += this.MaximizeRestoreClick;
      }

      /// <summary>
      /// Initializes functionality of the close button of the window's title bar.
      /// </summary>
      /// <param name="closeButton">The close button of the window</param>
      protected virtual void InitCloseButton(Button closeButton)
      {
         closeButton.Click += this.CloseClick;
      }

      /// <summary>
      /// Initializes functionality of the icon presenter component of the window's title bar.
      /// </summary>
      /// <param name="iconPresenter">The icon presenter component of the window</param>
      protected virtual void InitIconPresenter(FrameworkElement iconPresenter)
      {
         iconPresenter.MouseLeftButtonDown += (s, e) =>
         {
            if (e.ClickCount == 2)
            {
               this.Close();
               return;
            }

            var anchorElement = this.DragMoveThumb ?? this.IconPresenter;
            var menuPosition = anchorElement.TranslatePoint(new Point(0, anchorElement.ActualHeight), this);
            this.OpenSystemContextMenu(menuPosition);
         };
      }

      /// <summary>
      /// Handles the close button's click event.
      /// </summary>
      protected virtual void CloseClick(object sender, RoutedEventArgs e)
      {
         this.Close();
      }

      /// <summary>
      /// Handles the maximize/restore button's click event.
      /// </summary>
      protected virtual void MaximizeRestoreClick(object sender, RoutedEventArgs e)
      {
         this.ToggleWindowState();
      }

      /// <summary>
      /// Handles the minimize button's click event.
      /// </summary>
      protected virtual void MinimizeClick(object sender, RoutedEventArgs e)
      {
         this.WindowState = WindowState.Minimized;
      }

      /// <summary>
      /// Sets the <see cref="Window.WindowState"/> to <see cref="WindowState.Maximized"/>
      /// if it is currently at <see cref="WindowState.Normal"/> or else to <see cref="WindowState.Normal"/>.
      /// </summary>
      protected virtual void ToggleWindowState()
      {
         if (this.WindowState == WindowState.Normal)
         {
            this.WindowState = WindowState.Maximized;
         }
         else
         {
            this.WindowState = WindowState.Normal;
         }
      }

      private void RestoreOnMouseMove(object sender, MouseEventArgs e)
      {
         if (sender is not FrameworkElement dragMoveThumb)
         {
            return;
         }

         // detach event handler to ensure it is called only once per mouse down
         dragMoveThumb.MouseMove -= this.RestoreOnMouseMove;

         // collect given window and screen data
         var positionInWindow = e.MouseDevice.GetPosition(this);
         var positionOnScreen = this.PointToScreen(positionInWindow);
         var currentScreen = ScreenInterop.FromPoint(positionOnScreen);
         var restoreSizeOnScreen = this.TransformToScreenCoordinates(new Size(this.RestoreBounds.Width, this.RestoreBounds.Height));

         // calculate window's new top left coordinate
         var restoreLeft = positionOnScreen.X - (restoreSizeOnScreen.Width * 0.5);
         var restoreTop = positionOnScreen.Y - this.MaximizeBorderThickness.Top;

         // make sure the restore bounds are within the current screen bounds
         if (restoreLeft < currentScreen.Bounds.Left)
         {
            restoreLeft = currentScreen.Bounds.Left;
         }
         else if (restoreLeft + restoreSizeOnScreen.Width > currentScreen.Bounds.Right)
         {
            restoreLeft = currentScreen.Bounds.Right - restoreSizeOnScreen.Width;
         }

         // since we calculated with screen values, we need to convert back to window values
         var restoreTopLeftOnScreen = new Point(restoreLeft, restoreTop);
         var restoreTopLeft = this.TransformToWindowCoordinates(restoreTopLeftOnScreen);

         // restore window to calculated position
         this.Left = restoreTopLeft.X;
         this.Top = restoreTopLeft.Y;
         this.WindowState = WindowState.Normal;

         if (Mouse.LeftButton == MouseButtonState.Pressed)
         {
            this.DragMove();
         }
      }

      /// <summary>
      /// Converts a Size that represents the current coordinate system of the window
      /// into a Size in screen coordinates.
      /// </summary>
      protected Size TransformToScreenCoordinates(Size size)
      {
         var presentationSource = PresentationSource.FromVisual(this);

         if (presentationSource?.CompositionTarget == null)
         {
            return size;
         }

         var transformToDevice = presentationSource.CompositionTarget.TransformToDevice;
         return (Size)transformToDevice.Transform(new Vector(size.Width, size.Height));
      }

      /// <summary>
      /// Converts a <see cref="Size"/> that represents the native coordinate system of the screen
      /// into a <see cref="Size"/> in device independent coordinates.
      /// </summary>
      protected Size TransformToWindowCoordinates(Size size)
      {
         var transformedCoordinates = this.TransformToWindowCoordinates(new Point(size.Width, size.Height));
         return new Size(transformedCoordinates.X, transformedCoordinates.Y);
      }

      /// <summary>
      /// Converts a Point that represents the native coordinate system of the screen
      /// into a Point in device independent coordinates.
      /// </summary>
      protected Point TransformToWindowCoordinates(Point point)
      {
         var presentationSource = PresentationSource.FromVisual(this);

         if (presentationSource?.CompositionTarget == null)
         {
            return point;
         }

         var transformFromDevice = presentationSource.CompositionTarget.TransformFromDevice;
         return transformFromDevice.Transform(point);
      }

      /// <summary>
      /// Displays the system's native window context menu at the given position.
      /// </summary>
      /// <param name="positionInWindow">Coordinate of top left corner of the context menu relative to the window</param>
      protected virtual void OpenSystemContextMenu(Point positionInWindow)
      {
         SystemContextMenuInterop.OpenSystemContextMenu(this, positionInWindow);
      }

      /// <summary>
      /// When using <see cref="SizeToContent.WidthAndHeight"/> the layout might not be calculated correctly
      /// which can result in the window being too large and having large black borders filling the remaining space.
      /// This method can be used to force a layout update again to recalculate the window size correctly.
      /// See https://social.msdn.microsoft.com/Forums/vstudio/en-US/89fe6959-ce1a-4064-bdde-94151df7dc01/gradient-style-issue-when-sizetocontentheightandwidth-with-customchrome?forum=wpf
      /// </summary>
      private void UpdateLayoutForSizeToContent()
      {
         if (this.SizeToContent == SizeToContent.WidthAndHeight)
         {
            var previousSizeToContent = this.SizeToContent;
            this.SizeToContent = SizeToContent.Manual;

            this.Dispatcher?.BeginInvoke(DispatcherPriority.Loaded, (Action)(() =>
            {
               this.SizeToContent = previousSizeToContent;
            }));
         }
      }

      /// <summary>
      /// In order to maximize the window correctly, <see cref="SizeToContent.WidthAndHeight"/> must not be set.
      /// This method ensures that <see cref="SizeToContent.Manual"/> is set when the window is about to be maximized.
      /// </summary>
      private void DisableSizeToContentWhenMaximizing(object sender, HwndInteropPositionChangingEventArgs e)
      {
         if (e.Type == HwndInteropPositionChangingEventArgs.PositionChangeType.MAXIMIZERESTORE)
         {
            this.SizeToContent = SizeToContent.Manual;
         }
      }
   }
}
