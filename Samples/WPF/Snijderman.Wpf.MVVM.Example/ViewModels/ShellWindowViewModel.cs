using Snijderman.Common.Mvvm;
using Snijderman.Common.Mvvm.Services;
using Snijderman.Wpf.MVVM.Example.Services;

namespace Snijderman.Wpf.MVVM.Example.ViewModels;

public class ShellWindowViewModel : WpfMvvmViewModelBase
{
   public ShellWindowViewModel(IMessageService messageService)
   {
      this._messageService = messageService;
      this._messageService.Subscribe<ViewModelBase, string>(this, this.OnStringMessage);
      this._messageService.Subscribe<ViewModelBase, bool>(this, this.OnBooleanMessage);
      this._messageService.Subscribe<ApplicationHostService, bool>(this, this.OnApplicationLoaded);
   }

   private bool _showWaiting = true;
   public bool ShowWaiting
   {
      get => this._showWaiting;
      set => this.Set(ref this._showWaiting, value);
   }

   private bool _showContent;
   public bool ShowContent
   {
      get => this._showContent;
      set => this.Set(ref this._showContent, value);
   }

   private string _statusText;
   private readonly IMessageService _messageService;

   public string StatusText
   {
      get => this._statusText;
      set => this.Set(ref this._statusText, value);
   }

   private void OnStringMessage(ViewModelBase viewModel, string message, string status)
   {
      switch (message)
      {
         case MessageConstants.StatusMessage:
            this.SetStatus(status);
            break;
      }
   }

   private void SetStatus(string message)
   {
      message ??= "";
      message = message.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
      this.StatusText = message;
   }

   private void OnBooleanMessage(ViewModelBase viewModel, string message, bool value)
   {
      switch (message)
      {
         case MessageConstants.DisplayWaiting:
            this.ShowWaiting = value;
            break;
      }
   }

   private void OnApplicationLoaded(ApplicationHostService arg1, string message, bool value)
   {
      switch (message)
      {
         case MessageConstants.DisplayWaiting:
            this.ShowWaiting = value;
            this.ShowContent = !value;
            break;
      }
   }
}
