using System.Text;

namespace XmlToJson.Services;

public class WmsClient(IHttpClientFactory httpClientFactory)
{
    public async Task<string> SendRequestAsync(string xmlRequest, Uri uri)
    {
        var httpClient = httpClientFactory.CreateClient();

        StringContent content = new(xmlRequest, Encoding.UTF8, "application/xml");

        HttpResponseMessage response = await httpClient.PostAsync(uri, content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"error in WMS request, {response.StatusCode}");
        }

        return responseContent;
    }
}