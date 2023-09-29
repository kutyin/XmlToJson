using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Xml;
using System.Xml.Linq;

namespace XmlToJson;
public class XmlToJsonConverter {

    public static string XmlToJson(string xml) {
        try {
            XmlDocument doc = new();
            doc.LoadXml(xml);
            return JsonConvert.SerializeXmlNode(doc);
        } catch {

            throw;
        }
    }

    public static string JsonToXml(string json) {
        try {
            XNode node = JsonConvert.DeserializeXNode(json) ??
                throw new EmptyJsonException(new NullReferenceException());
            return node.ToString();
        } catch {
            throw;
        }
    }

    public static string SortJson(string json, string sort_by, bool desc = false) {

        try {
            JArray array = JArray.Parse(json);
            JArray sorted;
            if (!desc) {
                sorted = new(array.OrderBy(obj => (DateTime)obj[sort_by]));
            } else {
                sorted = new(array.OrderByDescending(obj => (DateTime)obj[sort_by]));
            }
            return sorted.ToString();

        } catch (Exception) {
            throw;
        }

    }
}