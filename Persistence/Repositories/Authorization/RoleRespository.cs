using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Repositories.Authorization;
using Covid_Project.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Covid_Project.Persistence.Repositories.Authorization
{
    public class RoleRespository : IRoleRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public RoleRespository(AppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        public RoleRespository(int accountId)
        {
            this.AccountId = accountId;

        }
        public int AccountId { get; set; }
        public List<Role> Roles { get; set; }

        public List<RoleDto> GetRoles(int accountId)
        {
            try
            {
                var roleId = _context.AccountHasRoles
                .Include(x => x.Role)
                .Where(x => x.AccountId == accountId && x.IsDeleted == false)
                .ToList();

                List<RoleDto> roles = new List<RoleDto>();

                foreach (var id in roleId)
                {
                    roles.Add(_mapper.Map<RoleDto>(id.Role));
                }

                if (roles.Capacity == 0)
                {
                    return null;
                }
                return roles;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
    }
}