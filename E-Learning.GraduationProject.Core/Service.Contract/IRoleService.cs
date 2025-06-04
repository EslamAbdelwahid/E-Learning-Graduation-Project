using E_Learning.GraduationProject.Core.Dtos.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface IRoleService
    {
        Task<RoleDto?> GetRoleByNameAsync(string name);
        Task<List<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> CreateRoleAsync(CreateRoleDto dto);
        Task<bool> UpdateRoleAsync(string id, UpdateRoleDto dto);
        Task<bool> DeleteRoleAsync(string id);
    }
}
