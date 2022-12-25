using EonNext.Monitor.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

EonNextClient client = new EonNextClient();

var configLines = System.IO.File.ReadAllText("../config.json");

var config = JsonConvert.DeserializeObject<JObject>(configLines);

string email = (string?)config["email"] ?? "N/A";
string password = (string?)config["password"] ?? "N/A";

await client.Login(email, password);

Console.WriteLine(client.AuthenticationToken);

string? accountNumber = await client.GetAccountNumber();
Console.WriteLine(accountNumber);

var meters = await client.GetActiveMeters(accountNumber);

Console.WriteLine(meters);

