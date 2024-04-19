using BlazorTest.Abstraction;
using BlazorTest.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace BlazorTest.Helpers
{
    public class ApiHelper
    {
        public HttpClient Client { get; }
        public ITokenService TokenService { get; }

        public ApiHelper(HttpClient client, ITokenService tokenService)
        {
            Client = client;
            TokenService = tokenService;
        }

        private async Task<T?> CallAsync<T>(HttpMethod httpMethod, string url, object? content = null, bool setAuthHeader = false) where T : class
        {
            try
            {
                var request = new HttpRequestMessage(httpMethod, url);

                if (content is not null)
                {
                    var jsonContent = JsonSerializer.Serialize(content);
                    request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                }

                if (setAuthHeader)
                {
                    var token = await TokenService.GetToken();
                    if (token is not null)
                        request.Headers.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
                }

                var callResult = await Client.SendAsync(request);

                string responseBody = await callResult.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseBody))
                    throw new ApiCallException { ErrorMessages = new string[] { callResult.StatusCode.ToString() + " [" + Convert.ToInt32(callResult.StatusCode) + "]" } };

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<NPResult<T>>(responseBody, options);
                if (result is not null)
                    if (result.IsSuccess)
                        return result.Value;
                    else
                        throw new ApiCallException { ErrorMessages = result.Errors.ToArray() };

                return null;
            }
            catch (ApiCallException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured during calling the api.", ex);
            }
        }
        private async Task<bool> CallAsync(HttpMethod httpMethod, string url, object? content = null, bool setAuthHeader = false)
        {
            try
            {
                var request = new HttpRequestMessage(httpMethod, url);

                if (content is not null)
                {
                    var jsonContent = JsonSerializer.Serialize(content);
                    request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                }

                if (setAuthHeader)
                {
                    var token = await TokenService.GetToken();
                    if (token is not null)
                        request.Headers.Authorization = new AuthenticationHeaderValue($"Bearer", $"{token.Token}");
                }

                var callResult = await Client.SendAsync(request);

                string responseBody = await callResult.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseBody))
                    throw new ApiCallException { ErrorMessages = new string[] { callResult.StatusCode.ToString() } };

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<NPResult>(responseBody, options);
                if (result is not null)
                    if (result.IsSuccess)
                        return true;
                    else
                        throw new ApiCallException { ErrorMessages = result.Errors.ToArray() };

                return false;
            }
            catch (ApiCallException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured during calling the api.", ex);
            }
        }

        public Task<T?> GetAsync<T>(string url, bool setAuthHeader = false) where T : class
        {
            return CallAsync<T>(HttpMethod.Get, url, setAuthHeader: setAuthHeader);
            //var result = await Client.GetFromJsonAsync<NPResult<T>>(url);
            //if (result is not null)
            //    if (result.IsSuccess)
            //        return result.Value;
            //    else
            //        throw new ApiCallException { ErrorMessages = result.Errors.ToArray() };

            //return null;
        }

        public Task<bool> DeleteAsync(string url, object? content = null, bool setAuthHeader = false)
        {
            return CallAsync(HttpMethod.Delete, url, content, setAuthHeader);
        }

        public Task<bool> PostAsync(string url, object? content = null, bool setAuthHeader = false)
        {
            return CallAsync(HttpMethod.Post, url, content, setAuthHeader);
        }
        public Task<T?> PostAsync<T>(string url, object? content = null, bool setAuthHeader = false) where T : class
        {
            return CallAsync<T>(HttpMethod.Post, url, content, setAuthHeader);
        }

        public Task<bool> PutAsync(string url, object content, bool setAuthHeader = false)
        {
            return CallAsync(HttpMethod.Put, url, content, setAuthHeader);
        }
        public Task<T?> PutAsync<T>(string url, object content, bool setAuthHeader = false) where T : class
        {
            return CallAsync<T>(HttpMethod.Put, url, content, setAuthHeader);
        }

        public Task<bool> PatchAsync(string url, object? content = null, bool setAuthHeader = false)
        {
            return CallAsync(HttpMethod.Patch, url, content, setAuthHeader);
        }
        public Task<T?> PatchAsync<T>(string url, object? content = null, bool setAuthHeader = false) where T : class
        {
            return CallAsync<T>(HttpMethod.Patch, url, content, setAuthHeader);
        }
    }
}
