using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace InforceUrlShortener.Services.BitlyShortener
{
    public static class UrlShortener
    {
        static readonly HttpClient client = new();
        static readonly string accessToken = "1e7aa55e689997344a9a643b4ddb7efd407086d6";
        static readonly string apiUrl = "https://api-ssl.bitly.com/v4/shorten";
        public static async Task<string> ShortenUrl(string longUrl)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestBody = new
                {
                    long_url = longUrl
                };

                var jsonRequestBody = JsonConvert.SerializeObject(requestBody);

                var httpContent = new StringContent(jsonRequestBody);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BitlyShortenResponse>(jsonResult);

                    return result.Link;
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    throw new BitlyException(response.StatusCode, responseContent);
                }
            }
            catch (BitlyException ex)
            {
                Console.WriteLine("Bitly API returned an error: " + ex.Message);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("An HTTP request exception occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return null;
        }
    }
}
