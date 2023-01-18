using RabbitMQ.Client;
using System.Text;

namespace Infrastructure.Services;

/// <summary>
/// Class - RabbitMQ Client Service
/// </summary>
public class RabbitMQClientService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    /// <summary>
    /// Constructor - RabbitMQ Client Service
    /// </summary>
    /// <param name="connection"></param>
    public RabbitMQClientService(IConnection connection)
    {
        _connection = connection;
        _channel = connection.CreateModel();
        _channel.ConfirmSelect();
    }

    /// <summary>
    /// Method - Produce
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="exchange"></param>
    /// <param name="routingKey"></param>
    public void Produce<T>(T message, string exchange, string routingKey)
    {
        try
        {
            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message.ToString());

            var basicProperties = _channel.CreateBasicProperties();
            basicProperties.Persistent = true;

            _channel.BasicPublish(exchange,
                                 routingKey,
                                 basicProperties,
                                 body: messageBodyBytes);
            _channel.WaitForConfirms();
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
    /// <param name="exchange"></param>
    /// <param name="routingKey"></param>
    /// <returns></returns>
    public async Task ProduceAsync<T>(T message, string exchange, string routingKey)
    {
        try
        {
            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message.ToString());

            var basicProperties = _channel.CreateBasicProperties();
            basicProperties.Persistent = true;

            await Task.Run(() => _channel.BasicPublish(exchange,
                                 routingKey,
                                 basicProperties,
                                 body: messageBodyBytes));
            await Task.Run(() => _channel.WaitForConfirmsOrDie());
        }
        catch (Exception ex)
        {
            // Handle exception
            Console.WriteLine("Error: " + ex.Message);
            throw;
        }
    }
}
