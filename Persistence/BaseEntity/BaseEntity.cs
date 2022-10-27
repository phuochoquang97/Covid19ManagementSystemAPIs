using System;
using System.Linq;
using System.Security.Claims;
using Covid_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Covid_Project.Persistence.BaseEntity
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = DateTime.UtcNow.AddHours(7);
            UpdatedAt = DateTime.UtcNow.AddHours(7);
            IsDeleted = false;
        }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Account UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}