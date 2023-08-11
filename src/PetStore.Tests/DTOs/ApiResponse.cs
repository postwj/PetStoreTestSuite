using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Tests.DTOs
{
    internal class ApiResponse
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; } = string.Empty;
    }
}
