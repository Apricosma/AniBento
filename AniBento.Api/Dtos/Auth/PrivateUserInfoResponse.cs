namespace AniBento.Api.Dtos.Auth
{
    public class PrivateUserInfoResponse
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = string.Empty;
    }
}
