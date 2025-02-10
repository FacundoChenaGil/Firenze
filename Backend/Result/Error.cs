namespace Utilities
{
    public class Error
    {
        public Error(string message, string propertyName)
        {
            Message = message;
            PropertyName = propertyName;    
        }

        public string Message { get; set; }
        public string PropertyName { get; set; }
    }
}
