using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShadowsocksTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string proxyAddress = "socks5://34.173.184.152:8334"; 
            string targetUrl = "http://httpbin.org/ip";

            var proxy = new HttpClientHandler
            {
                Proxy = new System.Net.WebProxy(proxyAddress),
                UseProxy = true
            };

            using var client = new HttpClient(proxy);
            client.Timeout = TimeSpan.FromSeconds(50);

            try
            {
                var stopwatch = Stopwatch.StartNew();
                var response = await client.GetAsync(targetUrl);
                stopwatch.Stop();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request successful!");
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response content: " + content);
                    Console.WriteLine("Total latency: " + stopwatch.ElapsedMilliseconds + "ms");
                }
                else
                {
                    Console.WriteLine("Request failed with status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Request failed: " + ex.Message);
            }
        }
    }
}