using E_Learning.GraduationProject.Core.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Repository.Contract
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync(string searchInputByName);
        Task<UserDto?> GetUserByIdAsync(string id);
        Task<bool> UpdateUserAsync(string id, UpdateUserDto dto);
        Task<bool> DeleteUserAsync(string id);
    }
}
