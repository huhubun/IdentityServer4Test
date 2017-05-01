using ClientFw.Models;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientFw
{
    class Program
    {
        static void Main(string[] args)
        {
            // discover endpoints from metadata
            var discover = DiscoveryClient.GetAsync("http://localhost:5000").Result;

            // request token
            var tokenClient = new TokenClient(discover.TokenEndpoint, "client", "secret");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("api1").Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine();


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5001");
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = client.GetAsync("samples").Result;
            if (response.IsSuccessStatusCode)
            {
                var sample = response.Content.ReadAsAsync<SampleResponse>().Result;
                Console.WriteLine("Response: ");
                Console.WriteLine($"{nameof(sample.Id)}: {sample.Id}");
                Console.WriteLine($"{nameof(sample.Name)}: {sample.Name}");
                Console.WriteLine($"{nameof(sample.Date)}: {sample.Date}");
            }
            else
            {
                Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode}");
            }

            Console.ReadKey();
        }
    }
}
