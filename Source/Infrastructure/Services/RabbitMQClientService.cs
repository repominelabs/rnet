using RabbitMQ.Client;
using System.Text;

namespace Infrastructure.Services;

/// <summary>
/// Class - RabbitMQ Client Service
/// </summary>
public class RabbitMQClientService
{
    /// <summary>
    /// Constructor - RabbitMQ Client Service
    /// </summary>
    /// <param name="connection"></param>
    public RabbitMQClientService()
    {
    }

    /// <summary>
    /// Method - Produce
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="host"></param>
    /// <param name="exchange"></param>
    /// <param name="routingKey"></param>
    public static void Produce<T>(T message, string host, string exchange, string routingKey)
    {
        var factory = new ConnectionFactory() { HostName = host  };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        try
        {
            channel.ConfirmSelect();
            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message.ToString());

            var basicProperties = channel.CreateBasicProperties();
            basicProperties.Persistent = true;

            channel.BasicPublish(exchange,
                                 routingKey,
                                 basicProperties,
                                 body: messageBodyBytes);
            channel.WaitForConfirms();
        }
        catch (Exception ex)
        {
            // Handle exception
            Console.WriteLine("Error: " + ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Method - Produce Async
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="host"></param>
    /// <param name="exchange"></param>
    /// <param name="routingKey"></param>
    /// <returns></returns>
    public static async Task ProduceAsync<T>(T message, string host, string exchange, string routingKey)
    {
        var factory = new ConnectionFactory() { HostName = host };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        try
        {
            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message.ToString());

            var basicProperties = channel.CreateBasicProperties();
            basicProperties.Persistent = true;

            await Task.Run(() => channel.BasicPublish(exchange,
                                 routingKey,
                                 basicProperties,
                                 body: messageBodyBytes));
            await Task.Run(() => channel.WaitForConfirmsOrDie());
        }
        catch (Exception ex)
        {
            // Handle exception
            Console.WriteLine("Error: " + ex.Message);
            throw;
        }
    }
}
