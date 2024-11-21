using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Xml;
using System.Xml.Linq;

namespace XmlToJson.Services;

public class XmlToJsonConverter
{
    public string XmlToJson(string xml)
    {
        XmlDocument doc = new();
        doc.LoadXml(xml);
        return JsonConvert.SerializeXmlNode(doc);
    }

    public string JsonToXml(string json)
    {
        XNode node = JsonConvert.DeserializeXNode(json) ??
            throw new EmptyJsonException(new NullReferenceException());
        return node.ToString();
    }

    public string SortJson(string json, string sort_by, bool desc = false)
    {
        JArray array = JArray.Parse(json);
        JArray sorted;
        if (!desc)
        {
            sorted = new(array.OrderBy(obj => (DateTime)obj[sort_by]));
        }
        else
        {
            sorted = new(array.OrderByDescending(obj => (DateTime)obj[sort_by]));
        }

        return sorted.ToString();
    }
}