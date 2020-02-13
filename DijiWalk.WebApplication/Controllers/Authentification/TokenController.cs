using DijiWalk.Common.Exceptions;
using DijiWalk.Common.Response;
using DijiWalk.Entities;
using DijiWalk.EntitiesContext;
using DijiWalk.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.WebApplication.Controllers.Authentification
{
    [Route("api/[controller]"), ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;

        /// <summary>
        /// Object private StepRepository with which we will interact with the database
        /// </summary>
        private readonly IAuthentificationBusiness _authentificationBusiness;

        public TokenController(IConfiguration configuration, IAuthentificationBusiness authenficiationBusiness)
        {
            _configuration = configuration;
            _authentificationBusiness = authenficiationBusiness;
        }

        [HttpPost,Route("player")]
        public async Task<IActionResult> PostPlayer([FromBody] Player login)
        {

            try
            {
                var user =  await _authentificationBusiness.Authentificate(login);
                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("FirstName", user.FirstName),
                        new Claim("LastName", user.LastName),
                        new Claim("UserName", user.Login),
                        new Claim("Email", user.Email)
                       };

                    var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"])), SecurityAlgorithms.HmacSha256));
                    user.JwtToken = new JWTTokens { Token = new JwtSecurityTokenHandler().WriteToken(token), Expiration = token.ValidTo };

                    return Ok( user );
                } else
                {
                    return Ok(new BadResponse { Message = "Invalid informations, try again !"});
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }

        [HttpPost, Route("organizer")]
        public async Task<IActionResult> PostOrganizer([FromBody] Organizer login)
        {

            try
            {
                var user = await _authentificationBusiness.Authentificate(login);
                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("FirstName", user.FirstName),
                        new Claim("LastName", user.LastName),
                        new Claim("UserName", user.Login),
                        new Claim("Email", user.OrganizerEmail)
                       };

                    var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"])), SecurityAlgorithms.HmacSha256));

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });
                }
                else
                {
                    return Ok(new ApiResponse { Status = ApiStatus.InvalidAuth, Message = "Invalid informations, try again !" });
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }


    }
}
