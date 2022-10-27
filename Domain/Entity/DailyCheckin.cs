using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Covid_Project.Persistence.BaseEntity;

namespace Covid_Project.Domain.Models
{
    [Table("DailyCheckin")]
    public class DailyCheckin : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CheckedDate { get; set; }
        [Required]
        public bool IsSymptomatic { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
    }
}