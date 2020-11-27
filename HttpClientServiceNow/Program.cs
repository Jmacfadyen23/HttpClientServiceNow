using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace HttpClientServiceNow
{
    class Entry
    {
        public string sysparm_action { get; set; }
        public string short_description { get; set; }
        public int priority { get; set; }

        public int impact { get; set; }
    }
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            var entry = new Entry();
            entry.sysparm_action = "insert";
            entry.short_description = "Can't Connect";
            entry.priority = 1;
            entry.impact = 2;
            var userName = "admin";
            var passwd = "kvsjIpTS5MV7";
            var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(authToken));
            var json = JsonConvert.SerializeObject(entry);
            var data = new StringContent(json, Encoding.UTF8,"application/json");

            var url = "https://dev89953.service-now.com/api/now/table/incident";

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
            
        }
    }
}
