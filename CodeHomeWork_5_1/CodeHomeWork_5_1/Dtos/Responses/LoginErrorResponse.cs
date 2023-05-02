using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHomeWork_5_1.Dtos.Responses
{
    public class LogingErrorResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
