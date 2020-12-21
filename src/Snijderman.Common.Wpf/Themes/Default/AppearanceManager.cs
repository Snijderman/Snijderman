using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Snijderman.Common.Wpf.Commands;

namespace Snijderman.Common.Wpf.Themes.Default
{
   public class AppearanceManager : NotifyPropertyChanged
   {
      /// <summary>
      /// The location of the dark theme resource dictionary.
      /// </summary>
      public static readonly Uri DarkThemeSource = new("/Snijderman.Common.Wpf;component/Themes/Dark.xaml", UriKind.Relative);
      /// <summary>
      /// The location of the light theme resource dictionary.
      /// </summary>
      public static readonly Uri LightThemeSource = new("/Snijderman.Common.Wpf;component/Themes/Light.xaml", UriKind.Relative);

      /// <summary>
      /// Gets the current <see cref="AppearanceManager"/> instance.
      /// </summary>
      public static AppearanceManager Current { get; } = new AppearanceManager();

      public AppearanceManager()
      {
         this.DarkThemeCommand = new RelayCommand(o => this.ThemeSource = DarkThemeSource, o => !DarkThemeSource.Equals(this.ThemeSource));
         this.LightThemeCommand = new RelayCommand(o => this.ThemeSource = LightThemeSource, o => !LightThemeSource.Equals(this.ThemeSource));
         this.SetThemeCommand = new RelayCommand(o => this.ThemeSource = o as Uri, o => o is Uri);
         this.LargeFontSizeCommand = new RelayCommand(o => this.FontSize = FontSize.Large);
         this.SmallFontSizeCommand = new RelayCommand(o => this.FontSize = FontSize.Small);
      }

      /// <summary>
      /// The command that sets the dark theme.
      /// </summary>
      public ICommand DarkThemeCommand { get; }
      /// <summary>
      /// The command that sets the light color theme.
      /// </summary>
      public ICommand LightThemeCommand { get; }
      /// <summary>
      /// The command that sets a custom theme.
      /// </summary>
      public ICommand SetThemeCommand { get; }
      /// <summary>
      /// The command that sets the large font size.
      /// </summary>
      public ICommand LargeFontSizeCommand { get; }
      /// <summary>
      /// The command that sets the small font size.
      /// </summary>
      public ICommand SmallFontSizeCommand { get; }

      private ResourceDictionary GetThemeDictionary()
      {
         // determine the current theme by looking at the app resources and return the first dictionary having the resource key 'WindowTitleForegroundColorKey' defined.
         return (from dict in Application.Current.Resources.MergedDictionaries
                 where dict.Contains(Colors.WindowTitleForegroundColorKey)
                 select dict).FirstOrDefault();
      }

      private Uri GetThemeSource()
      {
         return this.GetThemeDictionary()?.Source;
      }

      private FontSize GetFontSize()
      {
         if (Application.Current.Resources[Fonts.DefaultFontSizeKey.ResourceId] is double defaultFontSize)
         {
            return defaultFontSize == 12D ? FontSize.Small : FontSize.Large;
         }

         // default large
         return FontSize.Large;
      }

      private void SetFontSize(FontSize fontSize)
      {
         if (this.GetFontSize() == fontSize)
         {
            return;
         }

         Application.Current.Resources[Fonts.DefaultFontSizeKey.ResourceId] = fontSize == FontSize.Small ? 12D : 13D;
         Application.Current.Resources[Fonts.FixedFontSizeKey.ResourceId] = fontSize == FontSize.Small ? 10.667D : 13.333D;

         this.OnPropertyChanged(nameof(this.FontSize));
      }

      private void SetThemeSource(Uri source)
      {
         if (source == null)
         {
            throw new ArgumentNullException(nameof(source));
         }

         var oldThemeDict = this.GetThemeDictionary();
         var dictionaries = Application.Current.Resources.MergedDictionaries;
         var themeDict = new ResourceDictionary { Source = source };

         // add new before removing old theme to avoid dynamicresource not found warnings
         dictionaries.Add(themeDict);

         // remove old theme
         if (oldThemeDict != null)
         {
            dictionaries.Remove(oldThemeDict);
         }

         this.OnPropertyChanged(nameof(this.ThemeSource));
      }

      /// <summary>
      /// Gets or sets the current theme source.
      /// </summary>
      public Uri ThemeSource
      {
         get => this.GetThemeSource();
         set => this.SetThemeSource(value);
      }

      /// <summary>
      /// Gets or sets the font size.
      /// </summary>
      public FontSize FontSize
      {
         get => this.GetFontSize();
         set => this.SetFontSize(value);
      }

   }
}
