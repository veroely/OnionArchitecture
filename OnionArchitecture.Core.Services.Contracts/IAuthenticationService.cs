using OnionArchitecture.Core.Dtos.Requests;
using OnionArchitecture.Core.Dtos.Responses;
using System.Threading.Tasks;

namespace OnionArchitecture.Core.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task Register(UserRegisterRequestDto user);

        Task<UserLoginrResponseDto> Login(UserLoginRequestDto user);
    }
}
