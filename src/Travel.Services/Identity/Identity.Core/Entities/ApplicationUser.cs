namespace Identity.Core.Entities;

public class ApplicationUser: IdentityUser, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string Middle { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string SurName { get; set; } = string.Empty;
    public string Identification { get; set; } = string.Empty;
}