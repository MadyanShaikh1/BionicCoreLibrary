namespace BionicCoreLibrary.Core.Sessions;
public class CurrentSession
{
    public int? TenantId { get; set; }
    public int? UserId { get; set; }
    public string? UserName { get; set; }
}
