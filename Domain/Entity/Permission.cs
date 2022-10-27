using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Covid_Project.Persistence.BaseEntity;

namespace Covid_Project.Domain.Models
{
    [Table("Permission")]
    public class Permission : BaseEntity
    {
        public Permission()
        {
            RoleHasPermissions = new HashSet<RoleHasPermission>();
            AccountHasPermissions = new HashSet<AccountHasPermission>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required][MaxLength(50)]
        public string GuardName { get; set; }
        public virtual ICollection<RoleHasPermission> RoleHasPermissions { get; set; }
        public virtual ICollection<AccountHasPermission> AccountHasPermissions { get; set; }
        
    }
}