using System.ComponentModel.DataAnnotations;

namespace XmlToJson.Models; 
public class GetOperationParameters {

    [Required]
    public string Url { get; set; } = string.Empty;

    [Required]
    public string PartnerId { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
    public long UpdSeq { get; set; } = 0;
}
