using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public class DataProviderIsinResponse
    {
        [JsonProperty("isinNumber")]
        public string? IsinNumber { get; set; }

        [JsonProperty("DataCreateOfrequest")]
        public DateTime? DataCreateOfRequest { get; set; }

        [JsonProperty("requestedList")]
        public List<string>? RequestedList { get; set; }

    }
}