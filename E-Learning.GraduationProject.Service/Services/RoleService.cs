using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.Roles;
using E_Learning.GraduationProject.Core.Mapping.Roles;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleService(
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            this._roleManager = roleManager;
            this._mapper = mapper;
        }
        public async Task<RoleDto> CreateRoleAsync(CreateRoleDto dto)
        {
            var role = new IdentityRole(dto.Name);
            var res = await _roleManager.CreateAsync(role);
            if (!res.Succeeded) throw new Exception("Falid to create the role");
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<List<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return _mapper.Map<List<RoleDto>>(roles);  
        }

        public async Task<RoleDto?> GetRoleByNameAsync(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            return role == null ? null : _mapper.Map<RoleDto>(role);
        }

        public async Task<bool> UpdateRoleAsync(string id, UpdateRoleDto dto)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return false;
            role.Name = dto.Name;
            var res = await _roleManager.UpdateAsync(role);
            return res.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return false;
            var res = await _roleManager.DeleteAsync(role); 
            return res.Succeeded;
        }
    }
}
