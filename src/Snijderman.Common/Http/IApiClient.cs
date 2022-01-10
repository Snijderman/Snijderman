using System.Threading.Tasks;

namespace Snijderman.Common.Http
{
   internal interface IApiClient
   {
      string BaseAddress { get; set; }

      Task<TResult> GetAsync<TResult>(string endpoint);

      Task<TResult> GetFromJsonResponseAsync<TResult>(string endpoint);

      Task<TResult> GetAndReadFromJsonResponseAsync<TResult>(string endpoint);

      Task<TResult> PostAsync<TResult, TInput>(string endpoint, TInput postData);

      Task<TResult> PostFromJsonResponseAsync<TResult, TInput>(string endpoint, TInput postData);

      Task<TResult> PostAndReadFromJsonResponseAsync<TResult, TInput>(string endpoint, TInput postData);
   }
}
