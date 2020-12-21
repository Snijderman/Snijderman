using System.Windows;

namespace Snijderman.Common.Wpf.Themes.Default
{
   public static class Colors
   {
      public static ComponentResourceKey AccentColorKey => new(typeof(Colors), "AccentColor");

      public static ComponentResourceKey ForegroundColorKey => new(typeof(Colors), "ForegroundColor");
      public static ComponentResourceKey WindowBackgroundColorKey => new(typeof(Colors), "WindowBackgroundColor");
      public static ComponentResourceKey WindowTitleForegroundColorKey => new(typeof(Colors), "WindowTitleForegroundColor");
      public static ComponentResourceKey DisabledForegroundColorKey => new(typeof(Colors), "DisabledForegroundColor");
      public static ComponentResourceKey BackgroundColorKey => new(typeof(Colors), "BackgroundColor");
      public static ComponentResourceKey BorderColorKey => new(typeof(Colors), "BorderColor");
      public static ComponentResourceKey WindowButtonHighlightColorKey => new(typeof(Colors), "WindowButtonHighlightColor");
      public static ComponentResourceKey WindowButtonInteractionColorKey => new(typeof(Colors), "WindowButtonInteractionColor");
      public static ComponentResourceKey WindowBorderColorKey => new(typeof(Colors), "WindowBorderColor");
      public static ComponentResourceKey WindowBorderActiveColorKey => new(typeof(Colors), "WindowBorderActiveColor");

      // Button
      public static ComponentResourceKey ButtonBackgroundColorKey => new(typeof(Colors), "ButtonBackgroundColor");
      public static ComponentResourceKey ButtonBackgroundHoverColorKey => new(typeof(Colors), "ButtonBackgroundHoverColor");
      public static ComponentResourceKey ButtonBackgroundPressedColorKey => new(typeof(Colors), "ButtonBackgroundPressedColor");
      public static ComponentResourceKey ButtonBorderColorKey => new(typeof(Colors), "ButtonBorderColor");
      public static ComponentResourceKey ButtonBorderHoverColorKey => new(typeof(Colors), "ButtonBorderHoverColor");
      public static ComponentResourceKey ButtonBorderPressedColorKey => new(typeof(Colors), "ButtonBorderPressedColor");
      public static ComponentResourceKey ButtonTextColorKey => new(typeof(Colors), "ButtonTextColor");
      public static ComponentResourceKey ButtonTextHoverColorKey => new(typeof(Colors), "ButtonTextHoverColor");
      public static ComponentResourceKey ButtonTextPressedColorKey => new(typeof(Colors), "ButtonTextPressedColor");
      public static ComponentResourceKey ButtonTextDisabledColorKey => new(typeof(Colors), "ButtonTextDisabledColor");

      // Item
      public static ComponentResourceKey ItemBackgroundHoverColorKey => new(typeof(Colors), "ItemBackgroundHoverColor");
      public static ComponentResourceKey ItemBackgroundSelectedColorKey => new(typeof(Colors), "ItemBackgroundSelectedColor");
      public static ComponentResourceKey ItemBorderColorKey => new(typeof(Colors), "ItemBorderColor");
      public static ComponentResourceKey ItemTextColorKey => new(typeof(Colors), "ItemTextColor");
      public static ComponentResourceKey ItemTextSelectedColorKey => new(typeof(Colors), "ItemTextSelectedColor");
      public static ComponentResourceKey ItemTextHoverColorKey => new(typeof(Colors), "ItemTextHoverColor");
      public static ComponentResourceKey ItemTextDisabledColorKey => new(typeof(Colors), "ItemTextDisabledColor");

      // Input
      public static ComponentResourceKey InputBackgroundColorKey => new(typeof(Colors), "InputBackgroundColor");
      public static ComponentResourceKey InputBackgroundHoverColorKey => new(typeof(Colors), "InputBackgroundHoverColor");
      public static ComponentResourceKey InputBorderColorKey => new(typeof(Colors), "InputBorderColor");
      public static ComponentResourceKey InputBorderHoverColorKey => new(typeof(Colors), "InputBorderHoverColor");
      public static ComponentResourceKey InputTextColorKey => new(typeof(Colors), "InputTextColor");
      public static ComponentResourceKey InputTextHoverColorKey => new(typeof(Colors), "InputTextHoverColor");
      public static ComponentResourceKey InputTextDisabledColorKey => new(typeof(Colors), "InputTextDisabledColor");

      // Popup
      public static ComponentResourceKey PopupBackgroundColorKey => new(typeof(Colors), "PopupBackgroundColor");

      // Modern
      public static ComponentResourceKey ModernButtonBorderColorKey => new(typeof(Colors), "ModernButtonBorderColor");
      public static ComponentResourceKey ModernButtonBorderHoverColorKey => new(typeof(Colors), "ModernButtonBorderHoverColor");
      public static ComponentResourceKey ModernButtonBorderPressedColorKey => new(typeof(Colors), "ModernButtonBorderPressedColor");
      public static ComponentResourceKey ModernButtonBorderDisabledColorKey => new(typeof(Colors), "ModernButtonBorderDisabledColor");
      public static ComponentResourceKey ModernButtonIconBackgroundPressedColorKey => new(typeof(Colors), "ModernButtonIconBackgroundPressedColor");
      public static ComponentResourceKey ModernButtonIconForegroundPressedColorKey => new(typeof(Colors), "ModernButtonIconForegroundPressedColor");
      public static ComponentResourceKey ModernButtonTextColorKey => new(typeof(Colors), "ModernButtonTextColor");
      public static ComponentResourceKey ModernButtonTextHoverColorKey => new(typeof(Colors), "ModernButtonTextHoverColor");
      public static ComponentResourceKey ModernButtonTextPressedColorKey => new(typeof(Colors), "ModernButtonTextPressedColor");
      public static ComponentResourceKey ModernButtonTextDisabledColorKey => new(typeof(Colors), "ModernButtonTextDisabledColor");

      // Scrollbar
      public static ComponentResourceKey ScrollBarBackgroundColorKey => new(typeof(Colors), "ScrollBarBackgroundColor");
      public static ComponentResourceKey ScrollBarThumbBackgroundColorKey => new(typeof(Colors), "ScrollBarThumbBackgroundColor");
      public static ComponentResourceKey ScrollBarThumbBackgroundDraggingColorKey => new(typeof(Colors), "ScrollBarThumbBackgroundDraggingColor");
      public static ComponentResourceKey ScrollBarThumbBackgroundHoverColorKey => new(typeof(Colors), "ScrollBarThumbBackgroundHoverColor");
      public static ComponentResourceKey ScrollBarThumbBorderColorKey => new(typeof(Colors), "ScrollBarThumbBorderColor");
      public static ComponentResourceKey ScrollBarThumbForegroundColorKey => new(typeof(Colors), "ScrollBarThumbForegroundColor");
      public static ComponentResourceKey ScrollBarThumbForegroundDraggingColorKey => new(typeof(Colors), "ScrollBarThumbForegroundDraggingColor");
      public static ComponentResourceKey ScrollBarThumbForegroundHoverColorKey => new(typeof(Colors), "ScrollBarThumbForegroundHoverColor");

      // Seperator
      public static ComponentResourceKey SeparatorBackgroundColorKey => new(typeof(Colors), "SeparatorBackgroundColor");

      // Window text
      public static ComponentResourceKey WindowTextReadOnlyColorKey => new(typeof(Colors), "WindowTextReadOnlyColor");

      // Datagrid
      public static ComponentResourceKey DataGridBackgroundColorKey => new(typeof(Colors), "DataGridBackgroundColor");
      public static ComponentResourceKey DataGridForegroundColorKey => new(typeof(Colors), "DataGridForegroundColor");
      public static ComponentResourceKey DataGridCellBackgroundColorKey => new(typeof(Colors), "DataGridCellBackgroundColor");
      public static ComponentResourceKey DataGridCellBackgroundHoverColorKey => new(typeof(Colors), "DataGridCellBackgroundHoverColor");
      public static ComponentResourceKey DataGridCellBackgroundSelectedColorKey => new(typeof(Colors), "DataGridCellBackgroundSelectedColor");
      public static ComponentResourceKey DataGridCellForegroundColorKey => new(typeof(Colors), "DataGridCellForegroundColor");
      public static ComponentResourceKey DataGridCellForegroundHoverColorKey => new(typeof(Colors), "DataGridCellForegroundHoverColor");
      public static ComponentResourceKey DataGridCellForegroundSelectedColorKey => new(typeof(Colors), "DataGridCellForegroundSelectedColor");
      public static ComponentResourceKey DataGridHeaderBackgroundColorKey => new(typeof(Colors), "DataGridHeaderBackgroundColor");
      public static ComponentResourceKey DataGridHeaderBackgroundHoverColorKey => new(typeof(Colors), "DataGridHeaderBackgroundHoverColor");
      public static ComponentResourceKey DataGridHeaderBackgroundPressedColorKey => new(typeof(Colors), "DataGridHeaderBackgroundPressedColor");
      public static ComponentResourceKey DataGridHeaderBackgroundSelectedColorKey => new(typeof(Colors), "DataGridHeaderBackgroundSelectedColor");
      public static ComponentResourceKey DataGridHeaderForegroundColorKey => new(typeof(Colors), "DataGridHeaderForegroundColor");
      public static ComponentResourceKey DataGridHeaderForegroundHoverColorKey => new(typeof(Colors), "DataGridHeaderForegroundHoverColor");
      public static ComponentResourceKey DataGridHeaderForegroundPressedColorKey => new(typeof(Colors), "DataGridHeaderForegroundPressedColor");
      public static ComponentResourceKey DataGridHeaderForegroundSelectedColorKey => new(typeof(Colors), "DataGridHeaderForegroundSelectedColor");
      public static ComponentResourceKey DataGridGridLinesColorKey => new(typeof(Colors), "DataGridGridLinesColor");
      public static ComponentResourceKey DataGridDropSeparatorColorKey => new(typeof(Colors), "DataGridDropSeparatorColor");

      // Hyperlink
      public static ComponentResourceKey HyperlinkColorKey => new(typeof(Colors), "HyperlinkColor");
      public static ComponentResourceKey HyperlinkHoverColorKey => new(typeof(Colors), "HyperlinkHoverColor");
      public static ComponentResourceKey HyperlinkDisabledColorKey => new(typeof(Colors), "HyperlinkDisabledColor");

      // Progress
      public static ComponentResourceKey ProgressBackgroundColorKey => new(typeof(Colors), "ProgressBackgroundColor");

      // Slider
      public static ComponentResourceKey SliderSelectionBackgroundColorKey => new(typeof(Colors), "SliderSelectionBackgroundColor");
      public static ComponentResourceKey SliderSelectionBorderColorKey => new(typeof(Colors), "SliderSelectionBorderColor");
      public static ComponentResourceKey SliderThumbBackgroundColorKey => new(typeof(Colors), "SliderThumbBackgroundColor");
      public static ComponentResourceKey SliderThumbBackgroundDraggingColorKey => new(typeof(Colors), "SliderThumbBackgroundDraggingColor");
      public static ComponentResourceKey SliderThumbBackgroundHoverColorKey => new(typeof(Colors), "SliderThumbBackgroundHoverColor");
      public static ComponentResourceKey SliderThumbBackgroundDisabledColorKey => new(typeof(Colors), "SliderThumbBackgroundDisabledColor");
      public static ComponentResourceKey SliderThumbBorderColorKey => new(typeof(Colors), "SliderThumbBorderColor");
      public static ComponentResourceKey SliderThumbBorderDraggingColorKey => new(typeof(Colors), "SliderThumbBorderDraggingColor");
      public static ComponentResourceKey SliderThumbBorderHoverColorKey => new(typeof(Colors), "SliderThumbBorderHoverColor");
      public static ComponentResourceKey SliderThumbBorderDisabledColorKey => new(typeof(Colors), "SliderThumbBorderDisabledColor");
      public static ComponentResourceKey SliderTrackBackgroundColorKey => new(typeof(Colors), "SliderTrackBackgroundColor");
      public static ComponentResourceKey SliderTrackBorderColorKey => new(typeof(Colors), "SliderTrackBorderColor");
      public static ComponentResourceKey SliderTickColorKey => new(typeof(Colors), "SliderTickColor");
      public static ComponentResourceKey SliderTickDisabledColorKey => new(typeof(Colors), "SliderTickDisabledColor");
   }
}
