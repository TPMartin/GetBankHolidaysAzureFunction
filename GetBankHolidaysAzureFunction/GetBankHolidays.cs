using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GetBankHolidaysAzureFunction.Classes;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GetBankHolidaysAzureFunction
{
    public class GetBankHolidays
    {
        const string endpoint = "https://www.gov.uk/bank-holidays.json";
        private static DateTime _lastCheck;
        static List<DateTime> bankHolidays = new List<DateTime>();

        [FunctionName("GetBankHolidays")]
        public async Task RunAsync([TimerTrigger("*/5 8-17 * * Mon-Fri")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            if (DateTime.Now > _lastCheck.AddDays(1))
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responce = await client.GetAsync(endpoint);
                    string responceBody = await responce.Content.ReadAsStringAsync();

                    BankHolidaysObject rootObject = JsonConvert.DeserializeObject<BankHolidaysObject>(responceBody)!;
                    var cultureInfo = new CultureInfo("en-GB");

                    if (rootObject?.EnglandAndWales?.Events?.Any() == true)
                    {
                        bankHolidays = new List<DateTime>();
                        foreach (var bankHoliday in rootObject.EnglandAndWales.Events)
                        {
                            DateTime bh = DateTime.ParseExact(bankHoliday.Date, "yyyy-MM-dd", cultureInfo);
                            if (bh >= DateTime.Now)
                                bankHolidays.Add(bh);
                        }
                    }
                    bankHolidays.ForEach(x => Console.WriteLine(x.ToString()));
                    _lastCheck = DateTime.Now;
                }
            }
        }
    }
}
