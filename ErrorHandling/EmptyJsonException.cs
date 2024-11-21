namespace XmlToJson; 
public class EmptyJsonException : ApplicationException {

    private const string defaultMessage = "JSON was empty";
    public EmptyJsonException() : base(defaultMessage) { }
    public EmptyJsonException(Exception innerException) 
        : base(defaultMessage, innerException) { }
}
