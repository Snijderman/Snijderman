using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Snijderman.Common.ExtensionMethods;

namespace Snijderman.Common.Http
{
   public class ApiClient : IApiClient
   {
      private readonly HttpClient _client;

      public ApiClient(HttpClient httpClient, IConfiguration configuration) : this(httpClient, configuration, "ApiClientBaseUrl")
      {
      }

      public ApiClient(HttpClient httpClient, IConfiguration configuration, string configurationKey)
      {
         this._client = httpClient;
         this._client.Timeout = TimeSpan.FromMinutes(15);
         this.BaseAddress = configuration?[configurationKey];
      }

      private string _baseAddress;
      public string BaseAddress
      {
         get => this._baseAddress;
         set
         {
            if (string.IsNullOrEmpty(value))
            {
               return;
            }

            this.SetBaseAdress(value);
         }
      }

      private void SetBaseAdress(string value)
      {
         this._baseAddress = value;
         if (!this._baseAddress.EndsWith("/", StringComparison.OrdinalIgnoreCase))
         {
            this._baseAddress = $"{this._baseAddress}/";
         }
      }

      private string BuildRequestUri(string endpoint)
      {
         if (string.IsNullOrWhiteSpace(this.BaseAddress))
         {
            return endpoint;
         }

         if (string.IsNullOrWhiteSpace(endpoint))
         {
            return this.BaseAddress;
         }

         var startIndex = 0;
         if (endpoint.StartsWith("/", StringComparison.OrdinalIgnoreCase))
         {
            startIndex = 1;
         }

         return $"{this.BaseAddress}{endpoint[startIndex..]}";
      }

      public Task<TResult> GetAsync<TResult>(string endpoint) => this._client.GetWithResultAsync<TResult>(this.BuildRequestUri(endpoint));

      public Task<TResult> PostAsync<TResult, TInput>(string endpoint, TInput postData) => this._client.PostWithResultAsync<TResult, TInput>(this.BuildRequestUri(endpoint), postData);

      public Task<TResult> GetAndReadFromJsonResponseAsync<TResult>(string endpoint) => this._client.GetAndReadFromJsonResponseAsync<TResult>(this.BuildRequestUri(endpoint));

      public Task<TResult> PostAndReadFromJsonResponseAsync<TResult, TInput>(string endpoint, TInput postData) => this._client.PostAndReadFromJsonResponseAsync<TResult, TInput>(this.BuildRequestUri(endpoint), postData);

      public Task<TResult> GetFromJsonResponseAsync<TResult>(string endpoint) => this.GetAndReadFromJsonResponseAsync<TResult>(endpoint);

      public Task<TResult> PostFromJsonResponseAsync<TResult, TInput>(string endpoint, TInput postData) => this.PostAndReadFromJsonResponseAsync<TResult, TInput>(endpoint, postData);
   }
}
