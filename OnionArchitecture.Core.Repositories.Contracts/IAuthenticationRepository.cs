using OnionArchitecture.Core.Domain.Entities;
using OnionArchitecture.Core.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Core.Repositories.Contracts
{
    public interface IAuthenticationRepository
    {
        Task CreateUser(User user);

        Task<bool> CheckPassword(ApplicationUser user, string password);

        Task<ApplicationUser> GetUser(string email);
    }
}
