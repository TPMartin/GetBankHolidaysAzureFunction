using Newtonsoft.Json;

namespace GetBankHolidaysAzureFunction.Classes
{
    public class BankHolidaysObject
    {
        [JsonProperty(PropertyName = "england-and-wales")]
        public Region EnglandAndWales { get; set; }
        [JsonProperty(PropertyName = "scotland")]
        public Region Scotland { get; set; }
        [JsonProperty(PropertyName = "northern-ireland")]
        public Region NorthernIreland { get; set; }
    }
}
