using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MQTTnet;
using Moondesk.Edge.Simulator;
using Microsoft.Extensions.Configuration;


var builder = new ConfigurationBuilder()
    // Set the base path to the directory where the application assembly resides
    .SetBasePath(Directory.GetCurrentDirectory())
    
    // 2. Load the main appsettings.json file
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    
    // 4. Overwrite settings with actual Environment Variables
    .AddEnvironmentVariables();

// 5. Build the final configuration object
IConfigurationRoot configuration = builder.Build();
var emqxConfig = configuration.GetRequiredSection("EMQX");

string broker = emqxConfig["BROKER"] ?? string.Empty;
int port = Convert.ToInt32(emqxConfig["PORT"]) ;
string clientId = emqxConfig["CLIENTID"] ?? string.Empty;
string username = emqxConfig["USERNAME"] ?? string.Empty;
string password = "pDIY4ekFrPgRtCl9TYRu";

var factory = new MqttClientFactory();
var client = factory.CreateMqttClient();


var options = new MqttClientOptionsBuilder()
    .WithTlsOptions(tlsOptions => // <-- TLS config moves in here
    {
        string certFile = File.ReadAllText("/home/abdssamie/Desktop/emqxsl-ca.crt", Encoding.ASCII);
        
        var caChain = new X509Certificate2Collection();
        caChain.ImportFromPem(certFile);
        
        tlsOptions.WithCertificateValidationHandler(_ => true);

        // The default value is determined by the OS. Set manually to force version.
        tlsOptions.WithSslProtocols(SslProtocols.Tls12);
        
        // The property is now called 'ClientCertificates'
        tlsOptions.WithTrustChain(caChain);
    })
    .WithTcpServer(broker, port)
    .WithCredentials(username, password)
    .WithClientId(clientId)
    .WithCleanSession()
    .Build();

try
{
    await client.ConnectAsync(options: options, cancellationToken: CancellationToken.None);
    
    var task = Task.Run((() => SensorReadingSimulator.PublishAsync(client)));
    var task1 = Task.Run((() => SensorReadingSimulator.SimulateAlert(client)));

    try
    {
        await Task.WhenAll(task, task1);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}
catch (Exception exception)
{
    Console.WriteLine($@"Connection failed: {exception.Message}");
}