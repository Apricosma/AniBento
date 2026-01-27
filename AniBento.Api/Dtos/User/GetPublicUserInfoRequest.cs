namespace AniBento.Api.Dtos.User
{
    /// <summary>
    /// Request DTO for fetching public user information by username.
    /// </summary>
    public class GetPublicUserInfoRequest
    {
        public required string Username { get; set; }
    }
}
