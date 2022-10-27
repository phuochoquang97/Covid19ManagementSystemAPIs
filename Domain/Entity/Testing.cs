using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Covid_Project.Persistence.BaseEntity;

namespace Covid_Project.Domain.Models
{
    [Table("Testing")]
    public class Testing : BaseEntity
    {
        public Testing()
        {
            this.Cost = 734000;
            this.TestingState = 0;
            this.IsPaid = false;
            this.TestingLocation = new TestingLocation();
            this.RegisterDate = DateTime.Now.AddHours(7);
        }
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        public int TestingLocationId { get; set; }
        [ForeignKey("TestingLocationId ")]
        public virtual TestingLocation TestingLocation { get; set; }
        
        public DateTime RegisterDate { get; set; }
        public DateTime TestingDate { get; set; }
        public bool Result { get; set; }
        public int TestingState { get; set; } // 0: đang chờ xét duyet, 1: dang xet nghiem, 2: da tra kq
        public float Cost { get; set; }
        public bool IsPaid { get; set; }
        // public int MedicalInfoId { get; set; }
        // [ForeignKey("MedicalInfoId")]
        // public virtual MedicalInfo MedicalInfo { get; set; }
        
    }
}