namespace XmlToJson.Models;

using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("response")]
public class ResponseModel {
    [XmlElement("operation")]
    public List<OperationModel> Operations { get; set; } = new();

    [XmlAttribute("upd_seq")]
    public long UpdateSequence { get; set; }

    [XmlAttribute("state")]
    public int State { get; set; }
}


