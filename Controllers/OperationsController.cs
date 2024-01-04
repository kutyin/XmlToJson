using Microsoft.AspNetCore.Mvc;

using System.Net.Http.Headers;
using System.Text;
using System.Xml;
using IoFile = System.IO.File;
using System.Xml.Serialization;

using XmlToJson.Models;

namespace XmlToJson.Controllers;

[Route("operations")]
[ApiController]
public class OperationsController(ILogger<OperationsController> logger)
    : ControllerBase {

    private readonly ILogger<OperationsController> logger = logger;

    [HttpPost]
    public async Task<IActionResult> Get(GetOperationParameters param) {

        try {

            using HttpClient client = new();
            client.BaseAddress = new Uri(param.Url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

            string xmlContent = "<request " +
                "request_type=\"505\" " +
                $"partner_id =\"{param.PartnerId}\" " +
                $"password =\"{param.Password}\" " +
                $"upd_seq=\"{param.UpdSeq}\">" +
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

            foreach (OperationModel operation in responseModel?.Operations ?? Enumerable.Empty<OperationModel>()) {

                string csvRow = $"{operation.Id},{operation.Barcode},{operation.Type},{operation.Category},{operation.Date:s},{operation.Zip}";
                csvBuilder.AppendLine(csvRow);
            }

            string? csvPath = param.Path;

            if (string.IsNullOrWhiteSpace(csvPath)) {
                return Problem("Export path not set");
            }

            await IoFile.WriteAllTextAsync(csvPath, csvBuilder.ToString());

            return Ok(new {
                csvPath,
                updSeq = responseModel?.UpdateSequence
            });
        } catch (Exception ex) {
            logger.LogError(ex.Message, ex);
            return StatusCode(500, new { error = "An internal error occured processing your request" });
        }
    }
}
