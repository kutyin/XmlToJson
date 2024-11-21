using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using XmlToJson.Models;
using XmlToJson.Services;

namespace XmlToJson.Controllers;

[Route("operations")]
[ApiController]
public class TrackingController(ILogger<TrackingController> logger,
    TrackingCsvBuilder trackingCsvBuilder) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Get(GetTrackingParameters parameters)
    {
        string? csvPath = parameters.Path;

        if (string.IsNullOrWhiteSpace(csvPath))
        {
            return Problem("Export path not set");
        }

        using HttpClient client = new();
        client.BaseAddress = new Uri(parameters.Url);
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/xml"));

        string xmlContent = "<request " +
            "request_type=\"505\" " +
            $"partner_id =\"{parameters.PartnerId}\" " +
            $"password =\"{parameters.Password}\" " +
            $"upd_seq=\"{parameters.UpdSeq}\">" +
            "</request>";

        StringContent content = new(xmlContent, Encoding.UTF8, "application/xml");

        HttpResponseMessage response = await client.PostAsync("", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        logger.LogDebug(responseContent);

        if (!response.IsSuccessStatusCode)
        {
            return Problem(responseContent);
        }

        long updateSequence = await trackingCsvBuilder.BuildAsync(responseContent, csvPath);

        return Ok(new
        {
            csvPath,
            updSeq = updateSequence
        });
    }
}