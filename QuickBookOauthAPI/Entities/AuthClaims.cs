namespace QuickBookOauth.Entities;

public class AuthClaims
{
    public string Code { get; set; }
    public string State { get; set; }
    public string RealmId { get; set; }
}