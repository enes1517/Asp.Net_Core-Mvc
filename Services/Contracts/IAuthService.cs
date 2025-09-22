using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IAuthService
    {
        IEnumerable<IdentityRole> Roles { get; }
        IEnumerable<IdentityUser> GetAllUsers();
        Task<IdentityResult> CreateUser(UserDtoForCreation userDto); 
        Task<UserDtoForUpdate> GetOneUserForUpdate(string  userName); 
        Task<IdentityUser> GetOneUser(string userName);
        Task Update(UserDtoForUpdate userDto);
        Task<IdentityResult> ResetPassword(ResetPasswordDto passwordDto);
        Task<IdentityResult> DeleteOneUser(string userName);
    }
}
