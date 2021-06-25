namespace Byndyusoft.Template.Api.Client.Clients
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using Settings;
    using Utils;

    //TODO: Убрать ApiSettings, проставлять BaseUrl там, где клиент используется
    //TODO: Унифицировать методы, сейчас куча PostAsync
    //TODO protected 
    public abstract class HttpServiceClient
    {
        private readonly string _baseUrl;
        private readonly HttpClient _client;

        protected HttpServiceClient(HttpClient client,
                                    IOptions<TemplateApiSettings> apiSettings)
        {
            _client = client;
            var apiSettingsValue = apiSettings.Value ?? throw new ArgumentNullException(nameof(apiSettings));
            _baseUrl = $"{apiSettingsValue.Url}/api";
        }

        public async Task<string> GetAsync(string url, HttpQueryParameters? args = null)
        {
            var endpoint = $"{_baseUrl}{url}";
            var httpQuery = args != null
                                ? $"{endpoint}?{args.ToQueryString()}"
                                : endpoint;

            var response = await _client.GetAsync(httpQuery).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<TResult> GetAsync<TResult>(string url, HttpQueryParameters? args = null)
        {
            var endpoint = $"{_baseUrl}{url}";
            var httpQuery = args != null
                                ? $"{endpoint}?{args.ToQueryString()}"
                                : endpoint;

            var response = await _client.GetAsync(httpQuery).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TResult>();
        }

        public async Task<string> PostAsync(string url, string content)
        {
            var endpoint = $"{_baseUrl}{url}";
            var response = await _client.PostAsync(endpoint, new StringContent(content, Encoding.UTF8, "application/json"));

            var readAsStringAsync = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return readAsStringAsync;
        }

        public async Task<string> PostAsync<T>(string url, T content) where T : class
        {
            var endpoint = $"{_baseUrl}{url}";
            var response = await _client.PostAsJsonAsync(endpoint, content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<TResult> PostAsync<TContent, TResult>(string url, TContent content)
        {
            var endpoint = $"{_baseUrl}{url}";

            var response = await _client.PostAsJsonAsync(endpoint, content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TResult>();
        }

        public async Task<string> PostAsync(string url, Stream stream)
        {
            var endpoint = $"{_baseUrl}{url}";
            var file = new MultipartFormDataContent { { new StreamContent(stream), "file", "file" } };
            var response = await _client.PostAsync(endpoint, file);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PatchAsync(string url, string content)
        {
            var endpoint = $"{_baseUrl}{url}";
            var response = await _client.PatchAsync(endpoint, new StringContent(content, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<byte[]> GetByteArrayAsync(string url)
        {
            var endpoint = $"{_baseUrl}{url}";

            return await _client.GetByteArrayAsync(endpoint);
        }
    }

}