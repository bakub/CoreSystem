using Newtonsoft.Json;
using System.Text;

namespace Utils
{
    public static class RestUtils
    {
        public static async Task<TOut> SendRequestAsync<TOut>(string url, HttpMethod method, string json = null) where TOut : class
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var uri = new Uri(url);
                    HttpResponseMessage response = null;
                    switch (method.Method)
                    {
                        case "GET":
                            response = await client.GetAsync(uri).ConfigureAwait(false);
                            break;
                        case "POST":
                            response = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
                            break;
                        case "PUT":
                            response = await client.PutAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
                            break;
                        case "DELETE":
                            response = await client.DeleteAsync(uri);
                            break;
                    }
                    var request = await response.Content.ReadAsStringAsync();

                    if (typeof(TOut) == typeof(string))
                        return request as TOut;

                    var resultConvert = JsonConvert.DeserializeObject<TOut>(request);
                    return resultConvert;
                }
            }
            catch (Exception ex)
            {
                // log.Error($"Error during sending request to {url} using {method.Method} method. Message: {ex.Message}", ex);
                throw new Exception(ex.Message);
            }
        }
    }
}