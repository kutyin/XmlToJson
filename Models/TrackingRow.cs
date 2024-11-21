using System.Xml.Serialization;
using XmlToJson.Helpers;

namespace XmlToJson.Models;

[XmlRoot("operation")]
public class TrackingRow
{
    private string id = string.Empty;

    [XmlAttribute("barcode")]
    public string? Barcode { get; set; }

    [XmlAttribute("type")]
    public int Type { get; set; }

    [XmlAttribute("category")]
    public int Category { get; set; }

    [XmlAttribute("date")]
    public DateTime Date { get; set; }

    [XmlAttribute("zip")]
    public int Zip { get; set; }

    [XmlIgnore]
    public string Id
    {
        get
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = this.GetSha256Hash();
            }

            return id;
        }
    }
}