﻿using Microsoft.AspNetCore.Mvc;

using System.Reflection;

namespace XmlToJsonService.Controllers;

[ApiController]
public class HomeController {

    [HttpGet("/")]    
    public IActionResult Index() {
        return new ContentResult() {
            Content = $"XmlToJson is up and running.\n" +
                $"Version: {Assembly.GetExecutingAssembly().GetName().Version}\n" +
                "Use /help to view instructions",
            ContentType = "text/plain",
            StatusCode = 200
        };
    }

    [HttpGet("/help")]
    public IActionResult Help() {
        return new ContentResult() {
            Content = "use POST /xmltojson to convert from XML to JSON\n" +
                "use POST /jsontoxml to convert from JSON to XML\n" +
                "use POST /jsonsort to sort JSON array by date/time with params:\n"+
                "\tsort_by: field to sort by\n"+
                "\tsort_direction: asc || desc (optional, asc by default)",
            ContentType = "text/plain",
            StatusCode = 200
        };
    }
}
