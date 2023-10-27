using System.Xml.Serialization;

namespace XmlToJson.Models; 

[XmlRoot("operation")]
public class OperationModel {
    private int id = 0;

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
    public int Id {
        get {
            if (id == 0) {
                id = GetHashCode();
            }
            return id;
        }
    }

    public override int GetHashCode() {
        return HashCode.Combine(Barcode, Type, Category, Date, Zip);
    }
}