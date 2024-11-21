using System.Text;
using XmlToJson.Models;
using System.Xml;
using System.Xml.Serialization;

namespace XmlToJson.Services;

public class TrackingCsvBuilder
{
    public async Task<long> BuildAsync(string xmlResponse, string csvPath)
    {
        using StringReader textReader = new(xmlResponse);
        using XmlTextReader reader = new(textReader);
        reader.Namespaces = false;

        XmlSerializer serializer = new(typeof(WmsResponse));
        WmsResponse response = (WmsResponse?)serializer.Deserialize(reader) 
            ?? throw new Exception("Unable to parse response");

        StringBuilder csvBuilder = new();

        response.Operations.ForEach(operation =>
            csvBuilder.Append(operation.Id)
                .Append(',')
                .Append(operation.Barcode)
                .Append(',')
                .Append(operation.Type)
                .Append(',')
                .Append(operation.Category)
                .Append(',')
                .Append(operation.Date.ToString("s"))
                .Append(',')
                .Append(operation.Zip)
                .AppendLine());

        await File.WriteAllTextAsync(csvPath, csvBuilder.ToString());

        return response.UpdateSequence;
    }
}