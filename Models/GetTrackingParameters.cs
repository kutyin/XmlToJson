using System.ComponentModel.DataAnnotations;

namespace XmlToJson.Models; 
public class GetTrackingParameters {

    [Required]
    public string Url { get; set; } = string.Empty;

    [Required]
    public string PartnerId { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
    
    [Required]
    public string? Path { get; set; }
    public long UpdSeq { get; set; } = 0;
}
