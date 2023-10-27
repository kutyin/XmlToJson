using Microsoft.AspNetCore.Mvc;

using System.Net.Http.Headers;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using XmlToJson.Models;

namespace XmlToJson.Controllers;

[Route("operations")]
[ApiController]
public class OperationsController : ControllerBase {

    private readonly ILogger<OperationsController> logger;
    public OperationsController(ILogger<OperationsController> logger) {
        this.logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string url, string partnerId, string password, long updSeq = 0) {

        using HttpClient client = new();
        client.BaseAddress = new Uri(url);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

        string xmlContent = "<request " +
            "request_type=\"505\" " +
            $"partner_id =\"{partnerId}\" " +
            $"password =\"{password}\" " +
            $"upd_seq=\"{updSeq}\">" +
        "</request>";

        StringContent content = new(xmlContent, Encoding.UTF8, "application/xml");

        HttpResponseMessage response = await client.PostAsync("", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        logger.LogDebug(responseContent);

        if (!response.IsSuccessStatusCode) {
            return Problem(responseContent);
        }

        using StringReader textReader = new(responseContent);
        using XmlTextReader reader = new(textReader);
        reader.Namespaces = false;
        XmlSerializer serializer = new(typeof(ResponseModel));
        ResponseModel? responseModel = (ResponseModel?)serializer.Deserialize(reader);


        var csvBuilder = new StringBuilder();

        // Add a header row
        csvBuilder.AppendLine("ID,Barcode,Type,Category,Date,Zip");

        // Iterate through the operations and generate CSV rows
        foreach (var operation in responseModel.Operations) {


            // Create a CSV row
            string csvRow = $"{operation.Id},{operation.Barcode},{operation.Type},{operation.Category},{operation.Date},{operation.Zip}";

            // Append the CSV row to the content
            csvBuilder.AppendLine(csvRow);
        }



        return new ContentResult { ContentType = "text/plain", Content = csvBuilder.ToString(), StatusCode= 200};
    }
}
