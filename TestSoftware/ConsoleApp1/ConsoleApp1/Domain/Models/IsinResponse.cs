using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public class IsinResponse
    {
        public string? IsinNumber { get; set; }
        public DateTime? DataCreate { get; set; }
        public List<string>? RequestedList { get; set; }
    }
}