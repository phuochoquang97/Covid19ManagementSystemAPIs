using System;

namespace Covid_Project.Domain.Models.DTOs
{
    public class TestingResultDto
    {
        public TestingResultDto()
        {
            this.Result = "Chưa có kết quả.";
            this.Cost = 734000;
            this.TestingState = "Đang đợi xét duyệt.";
            this.IsPaid = false;
        }
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string FullName { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime TestingDate { get; set; }
        public TestingLocation TestingLocation { get; set; }
        public string Result { get; set; } // positive or negative
        public string TestingState { get; set; } // dang doi duyet, dang xet nghiem, da co kq
        public int TestingStateId { get; set; }
        public float Cost { get; set; }
        public bool IsPaid { get; set; }
    }
}