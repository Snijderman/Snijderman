using System;
using System.Threading.Tasks;
using Snijderman.Common.Commands;
using Snijderman.Common.Wpf.Helpers;

namespace Snijderman.Common.Wpf.Commands;

public class AsyncCommand<T> : CommandBase, IAsyncCommand<T>
{
   private readonly Func<T, Task> _execute;
   private readonly Predicate<T> _canExecute;
   private readonly IErrorHandler _errorHandler;

   public AsyncCommand(Func<T, Task> execute, Predicate<T> canExecute = null, IErrorHandler errorHandler = null)
   {
      this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
      if (canExecute == null)
      {
         // no can execute provided, then always executable
         canExecute = (o) => true;
      }
      this._canExecute = canExecute;
      this._errorHandler = errorHandler;
   }

   public override bool CanExecute(object parameter) => this._canExecute((T)parameter);

   protected override void OnExecute(object parameter) => this.ExecuteAsync((T)parameter).FireAndForgetSafeAsync(this._errorHandler);

   public async Task ExecuteAsync(T parameter) => await this._execute(parameter).ConfigureAwait(false);
}

public class AsyncCommand : CommandBase, IAsyncCommand
{
   private readonly Func<Task> _execute;
   private readonly Predicate<object> _canExecute;
   private readonly IErrorHandler _errorHandler;

   public AsyncCommand(Func<Task> execute, Predicate<object> canExecute = null, IErrorHandler errorHandler = null)
   {
      this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
      if (canExecute == null)
      {
         // no can execute provided, then always executable
         canExecute = (o) => true;
      }
      this._canExecute = canExecute;
      this._errorHandler = errorHandler;
   }

   public override bool CanExecute(object parameter) => this._canExecute(parameter);

   protected override void OnExecute(object parameter) => this.ExecuteAsync().FireAndForgetSafeAsync(this._errorHandler);

   public async Task ExecuteAsync() => await this._execute().ConfigureAwait(false);
}
