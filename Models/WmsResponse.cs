namespace XmlToJson.Models;

using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("response")]
public class WmsResponse {
    
    [XmlElement("operation")]
    public List<TrackingRow> Operations { get; set; } = [];

    [XmlAttribute("upd_seq")]
    public long UpdateSequence { get; set; }

    [XmlAttribute("state")]
    public int State { get; set; }
}