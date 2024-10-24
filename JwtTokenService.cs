using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(string username)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));

       
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        double expirationTime;
        _ = Double.TryParse(_configuration["Jwt:ExpiryInMinutes"], out expirationTime);


        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:issuer"],
            audience: _configuration["Jwt:issuer"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(expirationTime),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
