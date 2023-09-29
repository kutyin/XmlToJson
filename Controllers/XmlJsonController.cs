using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System.Xml;

using XmlToJson;

namespace XmlToJsonService.Controllers;

[ApiController]
public class XmlJsonController : ControllerBase {

    [HttpPost("jsonsort")]
    public async Task<IActionResult> JsonSort(
        [FromQuery] string sort_by,
        [FromQuery] string sort_direction = "asc") {

        try {
            using StreamReader reader = new(Request.Body);
            string body = await reader.ReadToEndAsync();
            bool desc = false;

            if (sort_direction == "desc") {
                desc = true;
            }

            string sortedJson = XmlToJsonConverter.SortJson(body, sort_by, desc);

            return new ContentResult() {
                Content = sortedJson,
                ContentType = "application/json",
                StatusCode = 200
            };
        } catch (Exception ex) {
            return Problem(ex.Message);
        }
    }

    [HttpPost("xmltojson")]
    public async Task<IActionResult> XmlToJsonAsync() {
        try {
            using StreamReader reader = new(Request.Body);
            string body = await reader.ReadToEndAsync();
            string json = XmlToJsonConverter.XmlToJson(body.ToString());
            return new ContentResult() {
                Content = json,
                ContentType = "application/json",
                StatusCode = 200
            };

        } catch (XmlException xmlEx) {
            return BadRequest(xmlEx.Message);
        } catch (Exception ex) {
            return Problem(ex.Message);
        }

    }

    [HttpPost("jsontoxml")]
    public async Task<IActionResult> JsonToXmlAsync() {
        try {
            using StreamReader reader = new(Request.Body);
            string body = await reader.ReadToEndAsync();
            string xml = XmlToJsonConverter.JsonToXml(body.ToString());
            return new ContentResult() {
                Content = xml,
                ContentType = "application/xml",
                StatusCode = 200
            };
        } catch (EmptyJsonException emptyEx) {
            return BadRequest(emptyEx.Message);
        } catch (JsonReaderException jsonEx) {
            return BadRequest(jsonEx.Message);
        } catch (Exception ex) {
            return Problem(ex.Message);
        }

    }
}
