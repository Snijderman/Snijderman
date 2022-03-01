using System.Windows;

namespace Snijderman.Common.Wpf.Themes.Default;

public static class Brushes
{
   public static ComponentResourceKey AccentBrushKey => new(typeof(Brushes), "AccentBrush");
   public static ComponentResourceKey ForegroundBrushKey => new(typeof(Brushes), "ForegroundBrush");
   public static ComponentResourceKey WindowBackgroundBrushKey => new(typeof(Brushes), "WindowBackgroundBrush");
   public static ComponentResourceKey WindowTitleForegroundBrushKey => new(typeof(Brushes), "WindowTitleForegroundBrush");
   public static ComponentResourceKey DisabledForegroundBrushKey => new(typeof(Brushes), "DisabledForegroundBrush");
   public static ComponentResourceKey BackgroundBrushKey => new(typeof(Brushes), "BackgroundBrush");
   public static ComponentResourceKey BorderBrushKey => new(typeof(Brushes), "BorderBrush");
   public static ComponentResourceKey WindowButtonHighlightBrushKey => new(typeof(Brushes), "WindowButtonHighlightBrush");
   public static ComponentResourceKey WindowButtonInteractionBrushKey => new(typeof(Brushes), "WindowButtonInteractionBrush");

   public static ComponentResourceKey WindowBorderBrushKey => new(typeof(Brushes), "WindowBorderBrush");
   public static ComponentResourceKey WindowBorderActiveBrushKey => new(typeof(Brushes), "WindowBorderActiveBrush");

   // Button
   public static ComponentResourceKey ButtonBackgroundBrushKey => new(typeof(Brushes), "ButtonBackgroundBrush");
   public static ComponentResourceKey ButtonBackgroundHoverBrushKey => new(typeof(Brushes), "ButtonBackgroundHoverBrush");
   public static ComponentResourceKey ButtonBackgroundPressedBrushKey => new(typeof(Brushes), "ButtonBackgroundPressedBrush");
   public static ComponentResourceKey ButtonBorderBrushKey => new(typeof(Brushes), "ButtonBorderBrush");
   public static ComponentResourceKey ButtonBorderHoverBrushKey => new(typeof(Brushes), "ButtonBorderHoverBrush");
   public static ComponentResourceKey ButtonBorderPressedBrushKey => new(typeof(Brushes), "ButtonBorderPressedBrush");
   public static ComponentResourceKey ButtonTextBrushKey => new(typeof(Brushes), "ButtonTextBrush");
   public static ComponentResourceKey ButtonTextHoverBrushKey => new(typeof(Brushes), "ButtonTextHoverBrush");
   public static ComponentResourceKey ButtonTextPressedBrushKey => new(typeof(Brushes), "ButtonTextPressedBrush");
   public static ComponentResourceKey ButtonTextDisabledBrushKey => new(typeof(Brushes), "ButtonTextDisabledBrush");

   // Item
   public static ComponentResourceKey ItemBackgroundHoverBrushKey => new(typeof(Brushes), "ItemBackgroundHoverBrush");
   public static ComponentResourceKey ItemBackgroundSelectedBrushKey => new(typeof(Brushes), "ItemBackgroundSelectedBrush");
   public static ComponentResourceKey ItemBorderBrushKey => new(typeof(Brushes), "ItemBorderBrush");
   public static ComponentResourceKey ItemTextBrushKey => new(typeof(Brushes), "ItemTextBrush");
   public static ComponentResourceKey ItemTextSelectedBrushKey => new(typeof(Brushes), "ItemTextSelectedBrush");
   public static ComponentResourceKey ItemTextHoverBrushKey => new(typeof(Brushes), "ItemTextHoverBrush");
   public static ComponentResourceKey ItemTextDisabledBrushKey => new(typeof(Brushes), "ItemTextDisabledBrush");

   // Input
   public static ComponentResourceKey InputBackgroundBrushKey => new(typeof(Brushes), "InputBackgroundBrush");
   public static ComponentResourceKey InputBackgroundHoverBrushKey => new(typeof(Brushes), "InputBackgroundHoverBrush");
   public static ComponentResourceKey InputBorderBrushKey => new(typeof(Brushes), "InputBorderBrush");
   public static ComponentResourceKey InputBorderHoverBrushKey => new(typeof(Brushes), "InputBorderHoverBrush");
   public static ComponentResourceKey InputTextBrushKey => new(typeof(Brushes), "InputTextBrush");
   public static ComponentResourceKey InputTextHoverBrushKey => new(typeof(Brushes), "InputTextHoverBrush");
   public static ComponentResourceKey InputTextDisabledBrushKey => new(typeof(Brushes), "InputTextDisabledBrush");

   // Popup
   public static ComponentResourceKey PopupBackgroundBrushKey => new(typeof(Brushes), "PopupBackgroundBrush");

   // Modern
   public static ComponentResourceKey ModernButtonBorderBrushKey => new(typeof(Brushes), "ModernButtonBorderBrush");
   public static ComponentResourceKey ModernButtonBorderHoverBrushKey => new(typeof(Brushes), "ModernButtonBorderHoverBrush");
   public static ComponentResourceKey ModernButtonBorderPressedBrushKey => new(typeof(Brushes), "ModernButtonBorderPressedBrush");
   public static ComponentResourceKey ModernButtonBorderDisabledBrushKey => new(typeof(Brushes), "ModernButtonBorderDisabledBrush");
   public static ComponentResourceKey ModernButtonIconBackgroundPressedBrushKey => new(typeof(Brushes), "ModernButtonIconBackgroundPressedBrush");
   public static ComponentResourceKey ModernButtonIconForegroundPressedBrushKey => new(typeof(Brushes), "ModernButtonIconForegroundPressedBrush");
   public static ComponentResourceKey ModernButtonTextBrushKey => new(typeof(Brushes), "ModernButtonTextBrush");
   public static ComponentResourceKey ModernButtonTextHoverBrushKey => new(typeof(Brushes), "ModernButtonTextHoverBrush");
   public static ComponentResourceKey ModernButtonTextPressedBrushKey => new(typeof(Brushes), "ModernButtonTextPressedBrush");
   public static ComponentResourceKey ModernButtonTextDisabledBrushKey => new(typeof(Brushes), "ModernButtonTextDisabledBrush");

   // Scrollbar
   public static ComponentResourceKey ScrollBarBackgroundBrushKey => new(typeof(Brushes), "ScrollBarBackgroundBrush");
   public static ComponentResourceKey ScrollBarThumbBackgroundBrushKey => new(typeof(Brushes), "ScrollBarThumbBackgroundBrush");
   public static ComponentResourceKey ScrollBarThumbBackgroundDraggingBrushKey => new(typeof(Brushes), "ScrollBarThumbBackgroundDraggingBrush");
   public static ComponentResourceKey ScrollBarThumbBackgroundHoverBrushKey => new(typeof(Brushes), "ScrollBarThumbBackgroundHoverBrush");
   public static ComponentResourceKey ScrollBarThumbBorderBrushKey => new(typeof(Brushes), "ScrollBarThumbBorderBrush");
   public static ComponentResourceKey ScrollBarThumbForegroundBrushKey => new(typeof(Brushes), "ScrollBarThumbForegroundBrush");
   public static ComponentResourceKey ScrollBarThumbForegroundDraggingBrushKey => new(typeof(Brushes), "ScrollBarThumbForegroundDraggingBrush");
   public static ComponentResourceKey ScrollBarThumbForegroundHoverBrushKey => new(typeof(Brushes), "ScrollBarThumbForegroundHoverBrush");

   // Seperator
   public static ComponentResourceKey SeparatorBackgroundBrushKey => new(typeof(Brushes), "SeparatorBackgroundBrush");

   // Window text
   public static ComponentResourceKey WindowTextReadOnlyBrushKey => new(typeof(Brushes), "WindowTextReadOnlyBrush");

   // Datagrid
   public static ComponentResourceKey DataGridBackgroundBrushKey => new(typeof(Brushes), "DataGridBackgroundBrush");
   public static ComponentResourceKey DataGridForegroundBrushKey => new(typeof(Brushes), "DataGridForegroundBrush");
   public static ComponentResourceKey DataGridCellBackgroundBrushKey => new(typeof(Brushes), "DataGridCellBackgroundBrush");
   public static ComponentResourceKey DataGridCellBackgroundHoverBrushKey => new(typeof(Brushes), "DataGridCellBackgroundHoverBrush");
   public static ComponentResourceKey DataGridCellBackgroundSelectedBrushKey => new(typeof(Brushes), "DataGridCellBackgroundSelectedBrush");
   public static ComponentResourceKey DataGridCellForegroundBrushKey => new(typeof(Brushes), "DataGridCellForegroundBrush");
   public static ComponentResourceKey DataGridCellForegroundHoverBrushKey => new(typeof(Brushes), "DataGridCellForegroundHoverBrush");
   public static ComponentResourceKey DataGridCellForegroundSelectedBrushKey => new(typeof(Brushes), "DataGridCellForegroundSelectedBrush");
   public static ComponentResourceKey DataGridHeaderBackgroundBrushKey => new(typeof(Brushes), "DataGridHeaderBackgroundBrush");
   public static ComponentResourceKey DataGridHeaderBackgroundHoverBrushKey => new(typeof(Brushes), "DataGridHeaderBackgroundHoverBrush");
   public static ComponentResourceKey DataGridHeaderBackgroundPressedBrushKey => new(typeof(Brushes), "DataGridHeaderBackgroundPressedBrush");
   public static ComponentResourceKey DataGridHeaderBackgroundSelectedBrushKey => new(typeof(Brushes), "DataGridHeaderBackgroundSelectedBrush");
   public static ComponentResourceKey DataGridHeaderForegroundBrushKey => new(typeof(Brushes), "DataGridHeaderForegroundBrush");
   public static ComponentResourceKey DataGridHeaderForegroundHoverBrushKey => new(typeof(Brushes), "DataGridHeaderForegroundHoverBrush");
   public static ComponentResourceKey DataGridHeaderForegroundPressedBrushKey => new(typeof(Brushes), "DataGridHeaderForegroundPressedBrush");
   public static ComponentResourceKey DataGridHeaderForegroundSelectedBrushKey => new(typeof(Brushes), "DataGridHeaderForegroundSelectedBrush");
   public static ComponentResourceKey DataGridGridLinesBrushKey => new(typeof(Brushes), "DataGridGridLinesBrush");
   public static ComponentResourceKey DataGridDropSeparatorBrushKey => new(typeof(Brushes), "DataGridDropSeparatorBrush");

   // Hyperlink
   public static ComponentResourceKey HyperlinkBrushKey => new(typeof(Brushes), "HyperlinkBrush");
   public static ComponentResourceKey HyperlinkHoverBrushKey => new(typeof(Brushes), "HyperlinkHoverBrush");
   public static ComponentResourceKey HyperlinkDisabledBrushKey => new(typeof(Brushes), "HyperlinkDisabledBrush");

   // Progress
   public static ComponentResourceKey ProgressBackgroundBrushKey => new(typeof(Brushes), "ProgressBackgroundBrush");

   // Slider
   public static ComponentResourceKey SliderSelectionBackgroundBrushKey => new(typeof(Brushes), "SliderSelectionBackgroundBrush");
   public static ComponentResourceKey SliderSelectionBorderBrushKey => new(typeof(Brushes), "SliderSelectionBorderBrush");
   public static ComponentResourceKey SliderThumbBackgroundBrushKey => new(typeof(Brushes), "SliderThumbBackgroundBrush");
   public static ComponentResourceKey SliderThumbBackgroundDraggingBrushKey => new(typeof(Brushes), "SliderThumbBackgroundDraggingBrush");
   public static ComponentResourceKey SliderThumbBackgroundHoverBrushKey => new(typeof(Brushes), "SliderThumbBackgroundHoverBrush");
   public static ComponentResourceKey SliderThumbBackgroundDisabledBrushKey => new(typeof(Brushes), "SliderThumbBackgroundDisabledBrush");
   public static ComponentResourceKey SliderThumbBorderBrushKey => new(typeof(Brushes), "SliderThumbBorderBrush");
   public static ComponentResourceKey SliderThumbBorderDraggingBrushKey => new(typeof(Brushes), "SliderThumbBorderDraggingBrush");
   public static ComponentResourceKey SliderThumbBorderHoverBrushKey => new(typeof(Brushes), "SliderThumbBorderHoverBrush");
   public static ComponentResourceKey SliderThumbBorderDisabledBrushKey => new(typeof(Brushes), "SliderThumbBorderDisabledBrush");
   public static ComponentResourceKey SliderTrackBackgroundBrushKey => new(typeof(Brushes), "SliderTrackBackgroundBrush");
   public static ComponentResourceKey SliderTrackBorderBrushKey => new(typeof(Brushes), "SliderTrackBorderBrush");
   public static ComponentResourceKey SliderTickBrushKey => new(typeof(Brushes), "SliderTickBrush");
   public static ComponentResourceKey SliderTickDisabledBrushKey => new(typeof(Brushes), "SliderTickDisabledBrush");

   // TabControl
   public static ComponentResourceKey TabControlBorderBrushKey => new(typeof(Brushes), "TabControlBorderBrush");
}
