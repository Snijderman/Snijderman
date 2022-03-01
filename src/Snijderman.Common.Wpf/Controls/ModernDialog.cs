using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Snijderman.Common.Wpf.Commands;

namespace Snijderman.Common.Wpf.Controls;

/// <summary>
/// Represents a Modern UI styled dialog window.
/// </summary>
public class ModernDialog : Window
{
   /// <summary>
   /// Identifies the BackgroundContent dependency property.
   /// </summary>
   public static readonly DependencyProperty BackgroundContentProperty = DependencyProperty.Register("BackgroundContent", typeof(object), typeof(ModernDialog));
   /// <summary>
   /// Identifies the Buttons dependency property.
   /// </summary>
   public static readonly DependencyProperty ButtonsProperty = DependencyProperty.Register("Buttons", typeof(IEnumerable<Button>), typeof(ModernDialog));

   private readonly ICommand _closeCommand;

   private Button _okButton;
   private Button _cancelButton;
   private Button _yesButton;
   private Button _noButton;
   private Button _closeButton;

   private MessageBoxResult _messageBoxResult = MessageBoxResult.None;

   /// <summary>
   /// Initializes a new instance of the <see cref="ModernDialog"/> class.
   /// </summary>
   public ModernDialog()
   {
      this.DefaultStyleKey = typeof(ModernDialog);
      this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

      this._closeCommand = new RelayCommand(o =>
      {
         var result = o as MessageBoxResult?;
         if (result.HasValue)
         {
            this._messageBoxResult = result.Value;

            // sets the Window.DialogResult as well
            if (result.Value == MessageBoxResult.OK || result.Value == MessageBoxResult.Yes)
            {
               this.DialogResult = true;
            }
            else if (result.Value == MessageBoxResult.Cancel || result.Value == MessageBoxResult.No)
            {
               this.DialogResult = false;
            }
            else
            {
               this.DialogResult = null;
            }
         }
         this.Close();
      });

      this.Buttons = new Button[] { this.CloseButton };

      // set the default owner to the app main window (if possible)
      if (Application.Current != null && Application.Current.MainWindow != this)
      {
         this.Owner = Application.Current.MainWindow;
      }
   }

   private Button CreateCloseDialogButton(string content, bool isDefault, bool isCancel, MessageBoxResult result)
   {
      return new Button
      {
         Content = content,
         Command = this.CloseCommand,
         CommandParameter = result,
         IsDefault = isDefault,
         IsCancel = isCancel,
         MinHeight = 21,
         MinWidth = 65,
         Margin = new Thickness(4, 0, 0, 0)
      };
   }

   /// <summary>
   /// Gets the close window command.
   /// </summary>
   public ICommand CloseCommand => this._closeCommand;

   /// <summary>
   /// Gets the Ok button.
   /// </summary>
   public Button OkButton
   {
      get
      {
         if (this._okButton == null)
         {
            this._okButton = this.CreateCloseDialogButton("Ok", true, false, MessageBoxResult.OK);
         }
         return this._okButton;
      }
   }

   /// <summary>
   /// Gets the Cancel button.
   /// </summary>
   public Button CancelButton
   {
      get
      {
         if (this._cancelButton == null)
         {
            this._cancelButton = this.CreateCloseDialogButton("Cancel", false, true, MessageBoxResult.Cancel);
         }
         return this._cancelButton;
      }
   }

   /// <summary>
   /// Gets the Yes button.
   /// </summary>
   public Button YesButton
   {
      get
      {
         if (this._yesButton == null)
         {
            this._yesButton = this.CreateCloseDialogButton("Yes", true, false, MessageBoxResult.Yes);
         }
         return this._yesButton;
      }
   }

   /// <summary>
   /// Gets the No button.
   /// </summary>
   public Button NoButton
   {
      get
      {
         if (this._noButton == null)
         {
            this._noButton = this.CreateCloseDialogButton("No", false, true, MessageBoxResult.No);
         }
         return this._noButton;
      }
   }

   /// <summary>
   /// Gets the Close button.
   /// </summary>
   public Button CloseButton
   {
      get
      {
         if (this._closeButton == null)
         {
            this._closeButton = this.CreateCloseDialogButton("Close", true, false, MessageBoxResult.None);
         }
         return this._closeButton;
      }
   }

   /// <summary>
   /// Gets or sets the background content of this window instance.
   /// </summary>
   public object BackgroundContent
   {
      get => this.GetValue(BackgroundContentProperty);
      set => this.SetValue(BackgroundContentProperty, value);
   }

   /// <summary>
   /// Gets or sets the dialog buttons.
   /// </summary>
   public IEnumerable<Button> Buttons
   {
      get => (IEnumerable<Button>)this.GetValue(ButtonsProperty);
      set => this.SetValue(ButtonsProperty, value);
   }

   /// <summary>
   /// Gets the message box result.
   /// </summary>
   /// <value>
   /// The message box result.
   /// </value>
   public MessageBoxResult MessageBoxResult => this._messageBoxResult;

   /// <summary>
   /// Displays a messagebox.
   /// </summary>
   /// <param name="text">The text.</param>
   /// <param name="title">The title.</param>
   /// <param name="button">The button.</param>
   /// <param name="owner">The window owning the messagebox. The messagebox will be located at the center of the owner.</param>
   /// <returns></returns>
   public static MessageBoxResult ShowMessage(string text, string title, MessageBoxButton button, Window owner = null)
   {
      var dlg = new ModernDialog
      {
         Title = title,
         Content = new BBCodeBlock { BBCode = text, Margin = new Thickness(0, 0, 0, 8) },
         MinHeight = 0,
         MinWidth = 0,
         MaxHeight = 480,
         MaxWidth = 640,
      };
      if (owner != null)
      {
         dlg.Owner = owner;
      }

      dlg.Buttons = GetButtons(dlg, button);
      dlg.ShowDialog();
      return dlg._messageBoxResult;
   }

   private static IEnumerable<Button> GetButtons(ModernDialog owner, MessageBoxButton button)
   {
      if (button == MessageBoxButton.OK)
      {
         yield return owner.OkButton;
      }
      else if (button == MessageBoxButton.OKCancel)
      {
         yield return owner.OkButton;
         yield return owner.CancelButton;
      }
      else if (button == MessageBoxButton.YesNo)
      {
         yield return owner.YesButton;
         yield return owner.NoButton;
      }
      else if (button == MessageBoxButton.YesNoCancel)
      {
         yield return owner.YesButton;
         yield return owner.NoButton;
         yield return owner.CancelButton;
      }
   }
}
