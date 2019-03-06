using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OvChipApi.Demo
{
    /// <summary>
    /// A small console application to show how to use "OVChipApi".
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            var username = "***REMOVED***";
            var password = "***REMOVED***";

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Please set-up your OV-chipkaart credentials correctly.");
            }

            Run(username, password).GetAwaiter().GetResult();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static async Task Run(string username, string password)
        {
            using (var client = new OvClient())
            {
                var loginResponse = await client.LoginAsync(username, password);
                if (!loginResponse) return;

                Console.WriteLine(JsonConvert.SerializeObject(client.OAuthTokens, Formatting.Indented));
                Console.WriteLine(client.AuthorizationToken);

                var cards = await client.GetCardsAsync();
                Console.WriteLine(JsonConvert.SerializeObject(await client.GetTransactionsAsync(cards.Data[0].MediumId), Formatting.Indented));

            }
        }
    }
}