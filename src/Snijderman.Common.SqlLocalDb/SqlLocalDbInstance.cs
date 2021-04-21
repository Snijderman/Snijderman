using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using MartinCostello.SqlLocalDb;

namespace Snijderman.Common.SqlLocalDb
{
   public class SqlLocalDbInstance : ISqlLocalDbInstance, IDisposable
   {
      private readonly string _instanceName;
      private readonly ISqlLocalDbApi _sqlLocalDbApi;
      private bool _disposedValue;

      [DebuggerStepThrough]
      internal void TryLog(string message)
      {
         Trace.WriteLine(message);
         Console.WriteLine(message);
         Debug.WriteLine(message);
      }

      public SqlLocalDbInstance(string instanceName)
      {
         this._instanceName = instanceName;
         this._sqlLocalDbApi = new SqlLocalDbApi();
      }

      public IReadOnlyList<string> GetInstanceNames
      {
         get
         {
            var tst = this._sqlLocalDbApi.GetInstanceInfo(this._instanceName);
            return this._sqlLocalDbApi.GetInstanceNames();
         }
      }

      private bool ServerInstanceExists => this._sqlLocalDbApi.InstanceExists(this._instanceName);

      private ISqlLocalDbInstanceManager GetInstanceManager => this._sqlLocalDbApi.GetInstanceInfo(this._instanceName).Manage();


      public void Start()
      {
         try
         {
            this.VerifySqlLocalDbIsPresent();
            this.CreateInstance();
            this.StartInstance();
         }
         catch (Exception exc)
         {
            this.TryLog($"Error starting {this._instanceName}:\r\n{exc}");
         }
      }

      private void VerifySqlLocalDbIsPresent()
      {
         if (!this._sqlLocalDbApi.IsLocalDBInstalled())
         {
            throw new InvalidOperationException("SqlLocalDB is not installed");
         }
      }

      private void CreateInstance()
      {
         if (this.ServerInstanceExists)
         {
            return;
         }
         this.TryLog($"Creating SqlLocalDB instance '{this._instanceName}'");
         _ = this._sqlLocalDbApi.GetOrCreateInstance(this._instanceName);
         if (!this._sqlLocalDbApi.IsLocalDBInstalled())
         {
            throw new InvalidOperationException($"Unable to create SqlLocalDb instance '{this._instanceName}'");
         }
         this.TryLog($"Created SqlLocalDB instance '{this._instanceName}'");
      }

      private void StartInstance()
      {
         var manager = this.GetInstanceManager;
         if (manager.GetInstanceInfo().IsRunning)
         {
            return;
         }
         this.TryLog($"Starting SqlLocalDB instance '{this._instanceName}'");
         manager.Start();
         if (!manager.GetInstanceInfo().IsRunning)
         {
            throw new InvalidOperationException($"Unable to start SqlLocalDb instance '{this._instanceName}'");
         }
         this.TryLog($"Started SqlLocalDB instance '{this._instanceName}'");
      }

      public void Shutdown()
      {
         if (!this.ServerInstanceExists)
         {
            return;
         }

         try
         {
            try
            {
               var manager = this.GetInstanceManager;
               if (manager.GetInstanceInfo().IsRunning)
               {
                  this.TryLog($"Stopping SqlLocalDB instance '{this._instanceName}'");
                  this._sqlLocalDbApi.StopInstance(this._instanceName, TimeSpan.FromSeconds(30));
                  this.TryLog($"Stopped SqlLocalDB instance '{this._instanceName}'");
               }
            }
            catch (Exception exc)
            {
               this.TryLog($"Error stopping instance '{this._instanceName}' using wrapper.\r\n{exc}");
               this.TryLog($"Try to stop instance '{this._instanceName}' using command");
               this.StopLocalDbAndKillInstanceProcess(this._instanceName);
            }

            this.TryLog($"Try to delete instance '{this._instanceName}'");
            if (this._sqlLocalDbApi is not SqlLocalDbApi sqlLocalDbApi)
            {
               this.TryLog($"Try to delete instance' {this._instanceName}'");
               this._sqlLocalDbApi.DeleteInstance(this._instanceName);
            }
            else
            {
               this.TryLog($"Try to delete instance '{this._instanceName}' and delete files");
               sqlLocalDbApi.DeleteInstance(this._instanceName, true);
            }

            if (this.ServerInstanceExists)
            {
               throw new InvalidOperationException($"Unable to delete SqlLocalDb instance '{this._instanceName}'");
            }
            this.TryLog($"Delete instance '{this._instanceName}' succeeded");

         }
         catch (Exception exc)
         {
            this.TryLog($"Error during shutdown of instance '{this._instanceName}':\r\n{exc}");
         }
      }

      private void StopLocalDbAndKillInstanceProcess(string localDbInstanceName) => this.StartCommandAndLogOutput($"stopping SqlLocalDb {localDbInstanceName}", $"/C sqllocaldb stop {localDbInstanceName} -k");

      private void StartCommandAndLogOutput(string logHeader, string arguments)
      {
         var sb = new StringBuilder();
         sb.AppendLine($"Start {logHeader}");
         try
         {
            using var process = new Process();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = arguments;

            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.Start();
            process.WaitForExit(7500);

            this.LogStandardOutput(process.StandardOutput, sb);
            this.LogStandardError(process.StandardError, sb);
         }
         catch (Exception exc)
         {
            sb.AppendLine($"Error {logHeader}:\r\n{exc}");
         }
         sb.AppendLine($"End {logHeader}");
         this.TryLog(sb.ToString());
      }

      private void LogStandardError(StreamReader standardError, StringBuilder sb)
      {
         if (standardError.EndOfStream)
         {
            return;
         }

         sb.AppendLine("Error(s) occured:");
         while (!standardError.EndOfStream)
         {
            var line = standardError.ReadLine();
            if (!string.IsNullOrWhiteSpace(line))
            {
               sb.AppendLine(line);
            }
         }
      }

      private void LogStandardOutput(StreamReader standardOutput, StringBuilder sb)
      {
         while (!standardOutput.EndOfStream)
         {
            var line = standardOutput.ReadLine();
            if (!string.IsNullOrWhiteSpace(line))
            {
               sb.AppendLine(line);
            }
         }
      }

      protected virtual void Dispose(bool disposing)
      {
         if (!this._disposedValue)
         {
            if (disposing)
            {
               if (this._sqlLocalDbApi is IDisposable sqlLocalDbApi)
               {
                  // always make sure the SqlLocalDb instance is removed
                  this.Shutdown();
                  sqlLocalDbApi.Dispose();
               }
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            this._disposedValue = true;
         }
      }

      // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
      // ~ServerInstance()
      // {
      //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      //     Dispose(disposing: false);
      // }

      public void Dispose()
      {
         // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
         this.Dispose(disposing: true);
         GC.SuppressFinalize(this);
      }
   }
}
