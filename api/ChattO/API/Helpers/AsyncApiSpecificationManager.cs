using LEGO.AsyncAPI.Models;
using System.Reflection;
using System.Text.Json;

namespace API.Helpers;

public static class AsyncApiSpecificationManager
{
    public static async void UpdateSpecification()
    {
        using var stream = new FileStream("async-api.yml", FileMode.OpenOrCreate);

        var document = new AsyncApiDocument() 
        {
            Info = new AsyncApiInfo() 
            {
                Title = "Async-api-specification",
                Version = "1.0.0"
            }
        };

        document.Servers.Add("", new AsyncApiServer() { Url = "ws://localhost:8080" });

        var chatChannel = new AsyncApiChannel() 
        {
            Servers = new List<string>() { "/api/ws" },
            Subscribe = new AsyncApiOperation 
            {
                Summary = "Receive message from the chat",
                Message = new List<AsyncApiMessage>()
                {
                    new AsyncApiMessage()
                    {
                        ContentType = "application/json",
                        Payload = new AsyncApiSchema() 
                        {

                        }
                    }    
                }
            },
            Publish = new AsyncApiOperation 
            {

            }
        };

        await JsonSerializer.SerializeAsync(stream, document);
    }
}
