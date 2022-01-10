using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Snijderman.Common.Http;

namespace Snijderman.Common.ExtensionMethods
{
   public static class HttpClientExtensions
   {
      private static StringContent GetPostContent<T>(T contentToPost) => new(JsonSerializer.Serialize(contentToPost), Encoding.UTF8, "application/json");

      public static Task<TResult> GetWithResultAsync<TResult>(this HttpClient httpClient, string endpoint) => ExecuteRequestAsync<TResult>(httpClient, CreateHttpGetRequestMessage(endpoint));

      public static Task<TResult> PostWithResultAsync<TResult, TInput>(this HttpClient httpClient, string endpoint, TInput postData) => ExecuteRequestAsync<TResult>(httpClient, CreateHttpPostRequestMessage(endpoint, postData));

      public static Task<TResult> GetAndReadFromJsonResponseAsync<TResult>(this HttpClient httpClient, string endpoint) => DeserializeFromStreamCallAsync<TResult>(httpClient, CreateHttpGetRequestMessage(endpoint));

      public static Task<TResult> PostAndReadFromJsonResponseAsync<TResult, TInput>(this HttpClient httpClient, string endpoint, TInput postData) => DeserializeFromStreamCallAsync<TResult>(httpClient, CreateHttpPostRequestMessage(endpoint, postData));

      private static HttpRequestMessage CreateHttpGetRequestMessage(string requestUrl)
      {
         return new HttpRequestMessage(HttpMethod.Get, requestUrl);
      }

      private static HttpRequestMessage CreateHttpPostRequestMessage<TInput>(string requestUrl, TInput postData)
      {
         return new HttpRequestMessage
         {
            Method = HttpMethod.Post,
            RequestUri = new Uri(requestUrl, UriKind.RelativeOrAbsolute),
            Content = GetPostContent(postData)
         };
      }

      private static async Task<TResult> ExecuteRequestAsync<TResult>(HttpClient httpClient, HttpRequestMessage request)
      {
         var requestUri = request.RequestUri;
         try
         {
            using (request)
            {
               using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
               var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

               if (response.IsSuccessStatusCode)
               {
                  return responseContent.ChangeType<TResult>();
               }

               throw new ApiException($"An error occured while executing a request to {requestUri}")
               {
                  StatusCode = (int)response.StatusCode,
                  Content = responseContent
               };
            }
         }
         catch (ApiException)
         {
            throw; // because we threw this ourselves
         }
         catch (HttpRequestException exc)
         {
            throw new Exception($"A HttpRequestException occured while executing request to {requestUri}:\r\n{exc}");
         }
         catch (Exception exc)
         {
            throw new Exception($"A non-HttpRequestException occured while executing request to {requestUri}:\r\n{exc}");
         }
      }

      private static async Task<TResult> DeserializeFromStreamCallAsync<TResult>(HttpClient httpClient, HttpRequestMessage request)
      {
         var requestUri = request.RequestUri;
         try
         {
            using (request)
            {
               using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
               using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

               if (response.IsSuccessStatusCode)
               {
                  return await JsonSerializer.DeserializeAsync<TResult>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).ConfigureAwait(false);
               }

               var content = await StreamToStringAsync(stream).ConfigureAwait(false);
               throw new ApiException($"An error occured while executing a request to {requestUri}")
               {
                  StatusCode = (int)response.StatusCode,
                  Content = content
               };
            }
         }
         catch (ApiException)
         {
            throw; // because we threw this ourselves
         }
         catch (HttpRequestException exc)
         {
            throw new Exception($"A HttpRequestException occured while executing request to {requestUri}:\r\n{exc}");
         }
         catch (Exception exc)
         {
            throw new Exception($"A non-HttpRequestException occured while executing request to {requestUri}:\r\n{exc}");
         }
      }

      private static async Task<string> StreamToStringAsync(Stream stream)
      {
         string content = default;

         if (stream != null)
         {
            using var sr = new StreamReader(stream);
            content = await sr.ReadToEndAsync().ConfigureAwait(false);
         }

         return content;
      }
   }
}
