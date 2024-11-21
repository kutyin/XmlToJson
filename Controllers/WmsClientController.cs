using Microsoft.AspNetCore.Mvc;
using XmlToJson.Models;
using XmlToJson.Services;

namespace XmlToJson.Controllers;

[Route("wms")]
[ApiController]
public class WmsClientController(ILogger<WmsClientController> logger,
    WmsClient wmsClient,
    XmlToJsonConverter converter) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendRequestAsync([FromBody] WmsRequestBody wmsRequestBody)
    {
        var jsonBody = wmsRequestBody.Body;
        var uri = new Uri(wmsRequestBody.Url);

        logger.LogDebug($"Url: {uri}");
        logger.LogDebug($"Body:{jsonBody}");

        var xmlBody = converter.JsonToXml(jsonBody);

        logger.LogDebug($"xmlBody: {xmlBody}");

        var xmlResponse = await wmsClient.SendRequestAsync(xmlBody, uri);

        logger.LogDebug($"xmlResponse: {xmlResponse}");

        var jsonResponse = converter.XmlToJson(xmlResponse);
        
        logger.LogDebug($"jsonResponse: {jsonResponse}");

        return new ContentResult()
        {
            Content = jsonResponse,
            ContentType = "application/json",
            StatusCode = 200
        };
    }
}