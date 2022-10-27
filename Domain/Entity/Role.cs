using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Covid_Project.Persistence.BaseEntity;

namespace Covid_Project.Domain.Models
{
    [Table("Role")]
    public class Role : BaseEntity
    {
        public Role()
        {
            AccountHasRoles = new List<AccountHasRole>();
            RoleHasPermissions = new List<RoleHasPermission>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required][MaxLength(50)]
        public string GuardName { get; set; }
        public virtual List<AccountHasRole> AccountHasRoles { get; set; }
        public virtual List<RoleHasPermission> RoleHasPermissions { get; set; }
    }
}