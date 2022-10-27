using System.Linq;
using System.Threading.Tasks;
using Covid_Project.Domain.Models;
using Covid_Project.Persistence.Context;
using Covid_Project.Services.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Covid_Project.Persistence.Helper
{
    public static class Seed
    {
        public static async Task SeedRoleAsync(AppDbContext context)
        {
            if (await context.Roles.AnyAsync())
            {
                return;
            }

            Role role1 = new Role();
            role1.Name = "User";
            role1.GuardName = "USER";

            Role role2 = new Role();
            role2.Name = "Admin 1";
            role2.GuardName = "ADMIN1";

            Role role3 = new Role();
            role3.Name = "Admin 2";
            role3.GuardName = "ADMIN2";

            await context.Roles.AddAsync(role1);
            await context.Roles.AddAsync(role2);
            await context.Roles.AddAsync(role3);
            await context.SaveChangesAsync();
        }

        public static async Task SeedPermissionAsync(AppDbContext context)
        {
            if (await context.Permissions.AnyAsync())
            {
                return;
            }

            Permission create = new Permission();
            create.Name = "Quyền tạo";
            create.GuardName = "CREATE";

            Permission update = new Permission();
            update.Name = "Quyền cập nhật";
            update.GuardName = "UPDATE";

            Permission read = new Permission();
            read.Name = "Quyền đọc";
            read.GuardName = "READ";

            Permission delete = new Permission();
            delete.Name = "Quyền xóa";
            delete.GuardName = "DELETE";

            await context.Permissions.AddAsync(create);
            await context.Permissions.AddAsync(update);
            await context.Permissions.AddAsync(read);
            await context.Permissions.AddAsync(delete);

            await context.SaveChangesAsync();
        }

        public static async Task SeedAdmin(AppDbContext context)
        {
            var check = context.Accounts.Where(x => x.Code.Equals("ADMINCODE"));
            if(check.Count() == 2)
            {
                return;
            }
            
            var password = "admin1";
            var passwordService = new PasswordService();
            var admin1 = new Account()
            {
                Email = "admin1@gmail.com",
                Password = passwordService.PasswordEncoder(password),
                Code = "ADMINCODE",
                IsVerified = 1
            };
            await context.Accounts.AddAsync(admin1);
            await context.SaveChangesAsync();
            var admin1Account = await context.Accounts.FirstOrDefaultAsync(x => x.Email.Equals("admin1@gmail.com"));
            var admin1Role = new AccountHasRole();
            var role1 = context.Roles.FirstOrDefault(x => x.GuardName.Equals("ADMIN1"));

            admin1Role.AccountId = admin1Account.Id;
            admin1Role.RoleId = role1.Id;
            await context.AccountHasRoles.AddAsync(admin1Role);

            password = "admin2";
            var admin2 = new Account()
            {
                Email = "admin2@gmail.com",
                Password = passwordService.PasswordEncoder(password),
                Code = "ADMINCODE",
                IsVerified = 1
            };
            await context.Accounts.AddAsync(admin2);
            await context.SaveChangesAsync();
            var admin2Account = await context.Accounts.FirstOrDefaultAsync(x => x.Email.Equals("admin2@gmail.com"));
            var admin2Role = new AccountHasRole();
            var role2 = context.Roles.FirstOrDefault(x => x.GuardName.Equals("ADMIN2"));

            admin2Role.AccountId = admin2Account.Id;
            admin2Role.RoleId = role2.Id;
            await context.AccountHasRoles.AddAsync(admin2Role);

            await context.SaveChangesAsync();
        }
        public static async Task SeedRoleHasPermission(AppDbContext context)
        {
            if (await context.RoleHasPermissions.AnyAsync())
            {
                return;
            }
            var listRoles = context.Roles.Where(x => x.IsDeleted == false).ToList();
            var listPermissions = context.Permissions.Where(x => x.IsDeleted == false).ToList();

            var user = listRoles.Find(item => item.GuardName == "USER");
            var admin1 = listRoles.Find(item => item.GuardName == "ADMIN1");
            var admin2 = listRoles.Find(item => item.GuardName == "ADMIN2");

            var create = listPermissions.Find(item => item.GuardName == "CREATE");
            var read = listPermissions.Find(item => item.GuardName == "READ");
            var update = listPermissions.Find(item => item.GuardName == "UPDATE");
            var delete = listPermissions.Find(item => item.GuardName == "DELETE");

            var userPermission = new RoleHasPermission();
            userPermission.RoleId = user.Id;
            userPermission.PermissionId = create.Id;
            await context.RoleHasPermissions.AddAsync(userPermission);

            userPermission = new RoleHasPermission();
            userPermission.RoleId = user.Id;
            userPermission.PermissionId = read.Id;
            await context.RoleHasPermissions.AddAsync(userPermission);

            userPermission = new RoleHasPermission();
            userPermission.RoleId = user.Id;
            userPermission.PermissionId = update.Id;
            await context.RoleHasPermissions.AddAsync(userPermission);

            var admin1Permission = new RoleHasPermission();
            admin1Permission.RoleId = admin1.Id;
            admin1Permission.PermissionId = create.Id;
            await context.RoleHasPermissions.AddAsync(admin1Permission);

            admin1Permission = new RoleHasPermission();
            admin1Permission.RoleId = admin1.Id;
            admin1Permission.PermissionId = read.Id;
            await context.RoleHasPermissions.AddAsync(admin1Permission);

            admin1Permission = new RoleHasPermission();
            admin1Permission.RoleId = admin1.Id;
            admin1Permission.PermissionId = update.Id;
            await context.RoleHasPermissions.AddAsync(admin1Permission);

            var admin2Permission = new RoleHasPermission();
            admin2Permission.RoleId = admin2.Id;
            admin2Permission.PermissionId = create.Id;
            await context.RoleHasPermissions.AddAsync(admin2Permission);

            admin2Permission = new RoleHasPermission();
            admin2Permission.RoleId = admin2.Id;
            admin2Permission.PermissionId = read.Id;
            await context.RoleHasPermissions.AddAsync(admin2Permission);

            admin2Permission = new RoleHasPermission();
            admin2Permission.RoleId = admin2.Id;
            admin2Permission.PermissionId = update.Id;
            await context.RoleHasPermissions.AddAsync(admin2Permission);

            admin2Permission = new RoleHasPermission();
            admin2Permission.RoleId = admin2.Id;
            admin2Permission.PermissionId = delete.Id;
            await context.RoleHasPermissions.AddAsync(admin2Permission);

            await context.SaveChangesAsync();
        }
    }
}