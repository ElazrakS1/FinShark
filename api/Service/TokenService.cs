﻿using System.IdentityModel.Tokens.Jwt;
        using System.Security.Claims;
        using System.Text;
        using api.Interfaces;
        using api.Models;
        using Microsoft.IdentityModel.Tokens;
        
        namespace api.Service
        {
            public class TokenService : ITokenService
            {
                private readonly IConfiguration _config;
                private readonly SymmetricSecurityKey _key;
        
                public TokenService(IConfiguration config)
                {
                    _config = config;
                    var signingKey = _config["JWT:SigningKey"] ?? throw new ArgumentNullException("JWT:SigningKey");
                    _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
                }
        
                public string CreateToken(AppUser user)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                        new Claim(JwtRegisteredClaimNames.GivenName, user.UserName ?? "")
                    };
        
                    var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.Now.AddDays(7),
                        SigningCredentials = creds,
                        Issuer = _config["JWT:Issuer"],
                        Audience = _config["JWT:Audience"]
                    };
        
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
        
                    return tokenHandler.WriteToken(token);
                }
            }
        }