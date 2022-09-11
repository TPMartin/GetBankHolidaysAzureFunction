using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetBankHolidaysAzureFunction.Classes
{
    public class Region
    {
        [JsonProperty(PropertyName = "division")]
        public string DivisionName { get; set; }
        [JsonProperty(PropertyName = "events")]
        public List<Event> Events { get; set; }
    }
}
