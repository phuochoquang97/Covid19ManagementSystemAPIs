using System;
using System.Collections.Generic;
using System.Linq;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;
using Covid_Project.Domain.Repositories.Authorization;
using Covid_Project.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Covid_Project.Persistence.Repositories.Authorization
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _context;
        public PermissionRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<string> GetListPermission(List<RoleDto> roles)
        {
            try
            {
                var listPermissionId = new List<RoleHasPermission>();
                foreach(var role in roles)
                {
                    var permissionId = _context.RoleHasPermissions
                    .Include(x => x.Permission)
                    .Where(x => x.RoleId == role.Id && x.IsDeleted == false)
                    .ToList();
                    listPermissionId = listPermissionId.Concat(permissionId).ToList();
                }
                var listPermissions = new List<string>();
                foreach(var id in listPermissionId)
                {
                    listPermissions.Add(id.Permission.GuardName);
                }

                return listPermissions.Distinct().ToList();
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}