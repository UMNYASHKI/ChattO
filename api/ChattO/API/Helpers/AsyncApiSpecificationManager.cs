using LEGO.AsyncAPI.Models;
using System.Reflection;
using System.Text.Json;

namespace API.Helpers;

public static class AsyncApiSpecificationManager
{
    private const string fileName = "async-api.yml";
    public static async Task CheckExistence() 
    {
        if (!File.Exists(fileName))
        {
            await UpdateSpecification();
        }
    }

    public static async Task UpdateSpecification()
    {
        using var stream = new FileStream(fileName, FileMode.OpenOrCreate);

        var document = new AsyncApiDocument() 
        {
            Info = new AsyncApiInfo() 
            {
                Title = "Async-api-specification",
                Version = "1.0.0"
            }
        };

        document.Servers.Add("web-socket", new AsyncApiServer() 
        {
            Url = "ws://localhost:8080",
            Protocol = "websocket",
            ProtocolVersion = "13"
        });

        document.DefaultContentType = "application/json";

        document.Channels.Add("mobile-client", new AsyncApiChannel() 
        {
            Description = "Messaging in mobile feeds",
            Servers = new List<string>() { "/api/ws" },
            Subscribe = new AsyncApiOperation 
            {
                Summary = "Requests from mobile client",
                Description = "Full mobile-client's lifecycle(establishing connection, messaging, closing connetion)",
                Message = new List<AsyncApiMessage>()
                {
                    new AsyncApiMessage()
                    {
                        MessageId = "Connection-managing",
                        Summary = "Establish/close connection with server",
                    },
                    new AsyncApiMessage()
                    {
                        MessageId = "Receiving",
                        Summary = "Receive message with specified sender and feed id",
                        ContentType = "application/json",
                        Payload = new AsyncApiSchema()
                        {
                            Type = SchemaType.Object,
                            Properties = new Dictionary<string, AsyncApiSchema>()
                            {
                                {
                                    "SenderId", new AsyncApiSchema()
                                    {
                                        Type = SchemaType.String
                                    }
                                },
                                {
                                    "FeedId", new AsyncApiSchema()
                                    {
                                        Type = SchemaType.String
                                    }
                                },
                                {
                                    "Content", new AsyncApiSchema()
                                    {
                                        Type = SchemaType.String
                                    }
                                }
                            }
                        }
                    }
                }
            },
            Publish = new AsyncApiOperation 
            {
                Summary = "Responses to mobile client",
                Description = "Processing and sending messages to mobile client",
                Message = new List<AsyncApiMessage>()
                {
                    new AsyncApiMessage()
                    {
                        MessageId = "Sending",
                        Summary = "Send message",
                        ContentType = "application/json",
                        Payload = new AsyncApiSchema()
                        {
                            Type = SchemaType.Object,
                            Properties = new Dictionary<string, AsyncApiSchema>()
                            {
                                {
                                    "SenderId", new AsyncApiSchema()
                                    {
                                        Type = SchemaType.String
                                    }
                                },
                                {
                                    "FeedId", new AsyncApiSchema()
                                    {
                                        Type = SchemaType.String
                                    }
                                },
                                {
                                    "Content", new AsyncApiSchema()
                                    {
                                        Type = SchemaType.String
                                    }
                                }
                            }
                        }
                    }
                }
            }
        });

        await JsonSerializer.SerializeAsync(stream, document, options: new JsonSerializerOptions() { WriteIndented = true});
    }
}
