namespace Bondo.Shared;
public class ValidationResult
{
    public ValidationResult(bool success, string message)
        {
            Success = success;
            Errors = message;
        }
        public bool Success { get; set; }
        public string Errors { get; set; }
}
