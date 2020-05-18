using System;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace NGK_handin3.Token
{
    public class TokenManager
    {
        public string GenerateToken(string user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,
                    new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("b4f472babe9a656bfc9fbe9b68eecf5c65da1a82f9d83abc597a5f3cd3218c99")),
                    SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
