using Microsoft.AspNetCore.Identity;
using OnionArchitecture.Core.Domain.Entities;
using OnionArchitecture.Core.Domain.Entities.Auth;
using OnionArchitecture.Core.Repositories.Contracts;
using System;
using System.Threading.Tasks;

namespace OnionArchitecture.Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly OnionArchitectureContext _context;
        public AuthenticationRepository(UserManager<ApplicationUser> userManager, OnionArchitectureContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public async Task CreateUser(User user)
        {
            var newUser = new ApplicationUser() { Email = user.Email, UserName = user.Email };

            var isCreated = await _userManager.CreateAsync(newUser, user.Password);
            if (isCreated.Succeeded)
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CheckPassword(ApplicationUser user, string password)
        {
            bool isCorrect = await _userManager.CheckPasswordAsync(user, password);
            return isCorrect;
        }

        public async Task<ApplicationUser> GetUser(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            return user;
        }
    }
}
