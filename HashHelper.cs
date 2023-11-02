using System.Text;
using XmlToJson.Models;
using System.Security.Cryptography;

namespace XmlToJson;
public static class HashHelper {
    public static string HashValues(OperationModel operation) {
        string input = $"{operation.Barcode}{operation.Type}{operation.Category}{operation.Date:s}{operation.Zip}";
        byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));


        StringBuilder builder = new();
        foreach (byte b in hashBytes) {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }
}
