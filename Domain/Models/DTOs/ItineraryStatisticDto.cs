using System;
using System.Collections.Generic;

namespace Covid_Project.Domain.Models.DTOs
{
    public class ItineraryStatisticDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IDictionary<string, int> Data { get; set; }
    }
}