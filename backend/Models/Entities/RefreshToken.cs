namespace backend.Models.Entities;

public class RefreshToken
{
    public Guid RefreshTokenId { get; set; }
    public required string TokenHash { get; set; }
    public User? User { get; set; }
    public DateTime Expiration { get; set; }
    public Guid UserId { get; set; }
}