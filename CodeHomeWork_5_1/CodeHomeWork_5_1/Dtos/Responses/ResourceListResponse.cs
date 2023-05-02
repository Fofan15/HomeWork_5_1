using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHomeWork_5_1.Dtos.Responses
{
    public class ResourceListResponse
    {
        [JsonProperty("data")]
        public List<ResourceDto> Data { get; set; }
    }
}
