using System;
namespace Covid_Project.Domain.Models.DTOs
{
    public class TestingDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public TestingLocation TestingLocation { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime TestingDate { get; set; }
        public bool Result { get; set; }
        public int TestingState { get; set; } // 0: đang chờ xét duyet, 1: dang xet nghiem, 2: da tra kq
        public float Cost { get; set; }
        public bool IsPaid { get; set; }
    }
}