namespace FileUploadService.API.Models
{
    public class AuthenModel 
    {
        public string User { get; set; }
        public string Password { get; set; }
    }

    public class TokenModel 
    {
        public string? token { get; set; }
        public DateTime? expireDate { get; set; }
    }
    public class DecodeTokenModel
    {
        public string EncodeUsername { get; set; }
        public string DecodeUsername { get; set; }
    }

}
