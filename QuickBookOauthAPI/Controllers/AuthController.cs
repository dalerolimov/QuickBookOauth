using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.AspNetCore.Mvc;
using QuickBookOauth.Entities;

namespace QuickBookOauthAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private const string ClientId = "AB982kbi6DYVFZK8Ea4oaQDkss3SJmUSYjR7OJMDa2zIOiRBtS";
    private const string ClientSecret = "3PyFAlJ3bDPG0uGuHWqvYAUFosOjVPsv1qE0dZeZ";
    private const string Environment = "sandbox";
    private const string RedirectUri = "https://localhost:44306/receiver";
    private readonly OAuth2Client _oAuth2Client;

    public AuthController()
    {
        _oAuth2Client = new OAuth2Client(ClientId, ClientSecret, RedirectUri, Environment);
    }

    [HttpPost]
    public async Task<string> AuthorizationCode(AuthClaims auth)
    {
        var token = await _oAuth2Client.GetBearerTokenAsync(auth.Code);
        
        HttpContext.Response.Cookies.Append("someCookie", "someValue");

        return token.AccessToken;
    }
}