namespace SimpleApp
{
    public record RegisterDto(string Username, string Email, string Password);
    public record LoginDto(string Username, string Password);
    public record TokenResponse(string AccessToken, string RefreshToken, long AccessTokenExpiresAtUnix);
    public record RefreshRequest(string RefreshToken);
}
