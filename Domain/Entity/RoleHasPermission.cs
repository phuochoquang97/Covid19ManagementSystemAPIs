using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Covid_Project.Persistence.BaseEntity;

namespace Covid_Project.Domain.Models
{
    [Table("RoleHasPermission")]
    public class RoleHasPermission : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PermissionId { get; set; }
        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}