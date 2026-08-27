namespace backend.Models.Entities;

public class User
{
    public Guid UserId { get; set; }
    
    public required string Email { get; set; }
    
    public string? PasswordHash { get; set; }

    public string? Username { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<JobEntry> JobEntries { get; set; } = new List<JobEntry>();
}