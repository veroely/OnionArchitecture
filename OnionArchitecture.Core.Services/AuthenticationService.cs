using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnionArchitecture.Core.Domain.Entities;
using OnionArchitecture.Core.Domain.Entities.Auth;
using OnionArchitecture.Core.Domain.Entities.Exceptions;
using OnionArchitecture.Core.Dtos.Requests;
using OnionArchitecture.Core.Dtos.Responses;
using OnionArchitecture.Core.Repositories.Contracts;
using OnionArchitecture.Core.Services.Contracts;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IAuthenticationRepository authenticationRepository, IConfiguration configuration)
        {
            _authenticationRepository = authenticationRepository;
            _configuration = configuration;
        }

        public async Task<UserLoginrResponseDto> Login(UserLoginRequestDto user)
        {
            ApplicationUser userApp = await _authenticationRepository.GetUser(user.Email);

            if (userApp == null)
            {
                throw new AppException("No existe un usuario con ese email");
            }

            var isCorrect = await _authenticationRepository.CheckPassword(userApp ,user.Password);

            if (!isCorrect)
            {
                throw new Exception("El password es incorrecto");
            }

            var jwtToken = GenerateJwtToken(userApp);

            return new UserLoginrResponseDto { Email = user.Email, Token = jwtToken };
        }

        public async Task Register(UserRegisterRequestDto user)
        {
            ApplicationUser userApp = await _authenticationRepository.GetUser(user.Email);

            if (userApp != null)
            {
                throw new Exception("Ya existe un usuario con ese email");
            }

            User userNew = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Email = user.Email
            };

            await _authenticationRepository.CreateUser(userNew);
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
