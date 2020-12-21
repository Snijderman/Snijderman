using System;

namespace Snijderman.Common.Wpf.Commands
{
   public class RelayCommand<T> : CommandBase
   {
      private readonly Predicate<T> _canExecute;
      private readonly Action<T> _execute;

      public RelayCommand(Action<T> execute) : this(execute, null)
      {
      }

      public RelayCommand(Action<T> execute, Predicate<T> canExecute)
      {
         this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
         if (canExecute == null)
         {
            // no can execute provided, then always executable
            canExecute = (o) => true;
         }
         this._canExecute = canExecute;
      }

      public override bool CanExecute(object parameter) => this._canExecute((T)parameter);

      /// <inheritdoc />
      protected override void OnExecute(object parameter) => this._execute((T)parameter);
   }

   /// <summary>
   /// The command that relays its functionality by invoking delegates.
   /// </summary>
   public class RelayCommand : RelayCommand<object>
   {
      public RelayCommand(Action<object> execute) : base(execute)
      { }

      public RelayCommand(Action<object> execute, Predicate<object> canExecute) : base(execute, canExecute)
      { }
   }
}
