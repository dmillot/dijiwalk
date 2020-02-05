using DijiWalk.Common.Exceptions;
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
        private readonly IAuthentificationRepository _authentificationRepository;

        public TokenController(IConfiguration configuration, IAuthentificationRepository authenficiationRepository)
        {
            _configuration = configuration;
            _authentificationRepository = authenficiationRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Player login)
        {

            try
            {
                var user =  await _authentificationRepository.Authentificate(login);
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

                    return Ok( new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });
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

    }
}
