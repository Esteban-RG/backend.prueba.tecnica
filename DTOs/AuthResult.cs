
namespace PruebaBackend.DTOs
{
    public class AuthResult
    {
        public bool Succeeded { get; private set; }
        public string? Token { get; private set; } 
        public IEnumerable<string>? Errors { get; private set; } 
        public static AuthResult Success(string token)
        {
            return new AuthResult { Succeeded = true, Token = token };
        }

        public static AuthResult Failure(IEnumerable<string> errors)
        {
            return new AuthResult { Succeeded = false, Errors = errors };
        }
    }
}
