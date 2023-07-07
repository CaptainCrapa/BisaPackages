using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace BSOpenAiApi.BaseModel
{
    public class OpenAiApiConnection
    {
        private string API_URL;
        private string apiKey;
        private readonly HttpClient httpClient;
        /// <summary>
        /// Api Url beállítása
        /// </summary>
        /// <param name="url"></param>
        public void SetApiUrl(string url) => this.API_URL = url;
        /// <summary>
        /// Api kulcs beállítása
        /// </summary>
        /// <param name="apiKey"></param>
        public void SetApiKey(string apiKey) => this.apiKey = apiKey;
        /// <summary>
        /// Itt rakja össze a kommunikációhoz szükséges információkat.
        /// </summary>
        /// <param name="api_Key"></param>
        /// <param name="api_url"></param>
        /// <param name="HttpClientTimeOut"></param>
        public OpenAiApiConnection(string api_Key, string api_url, int HttpClientTimeOut = 200)
        {
            API_URL = api_url;
            apiKey = api_Key;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            httpClient.Timeout = TimeSpan.FromSeconds(HttpClientTimeOut);
        }
        /// <summary>
        /// A beküldött data object alapján POSTol a megadott apiURL-re..
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<HttpResponseMessage> GetResponse(object data)
        {
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(API_URL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"HIBA a válasznál: {response.StatusCode}\n{errorMessage}");
            }
            return response;
        }


    }
}