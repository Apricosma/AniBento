namespace AniBento.Api.Dtos.User
{
    /// <summary>
    /// Response DTO for public user information.
    /// </summary>
    public class PublicUserInfoResponse
    {
        public required string UserName { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
