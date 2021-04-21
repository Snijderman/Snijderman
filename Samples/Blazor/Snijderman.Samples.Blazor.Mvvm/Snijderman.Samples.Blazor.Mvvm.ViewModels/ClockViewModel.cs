using System;
using System.Timers;
using Snijderman.Common.Mvvm;

namespace Snijderman.Samples.Blazor.Mvvm.ViewModels
{
   public class ClockViewModel : ViewModelBase, IDisposable
   {
      private readonly Timer _timer;
      private bool _disposedValue;
      private static int _counter;

      public ClockViewModel()
      {
         _counter++;
         this._timer = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);
         this._timer.Elapsed += this.TimerOnElapsed;
         this._timer.Start();
      }

      public int Counter => _counter;

      private string _displayDateTime;
      public string DisplayDateTime
      {
         get => this._displayDateTime;
         set => this.Set(ref this._displayDateTime, value);
      }

      private void TimerOnElapsed(object sender, ElapsedEventArgs e) => this.DisplayDateTime = DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy");

      protected virtual void Dispose(bool disposing)
      {
         if (!this._disposedValue)
         {
            if (disposing)
            {
               this._timer.Dispose();
            }

            this._disposedValue = true;
         }
      }

      public void Dispose()
      {
         // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
         this.Dispose(disposing: true);
         GC.SuppressFinalize(this);
      }
   }
}
