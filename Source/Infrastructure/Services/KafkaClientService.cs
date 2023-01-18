using Confluent.Kafka;

namespace Infrastructure.Services;

/// <summary>
/// Class - Kafka Client Service
/// </summary>
public class KafkaClientService
{
    /// <summary>
    /// Constructor - Kafka Client Service
    /// </summary>
    public KafkaClientService()
    {
    }

    /// <summary>
    /// Method - Produce
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="topic"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="config"></param>
    public static void Produce<TKey, TValue>(string topic, TKey key, TValue value, ProducerConfig config)
    {
        using var producer = new ProducerBuilder<TKey, TValue>(config)
                    .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                    .SetStatisticsHandler((_, json) => Console.WriteLine($"Statistics: {json}"))
                    .Build();
        try
        {
            producer.Produce(topic, new Message<TKey, TValue> { Key = key, Value = value });
        }
        catch (ProduceException<TKey, TValue> e)
        {
            Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            throw;
        }
    }

    /// <summary>
    /// Method - Produce Async
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="topic"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static async Task ProduceAsync<TKey, TValue>(string topic, TKey key, TValue value, ProducerConfig config)
    {
        using var producer = new ProducerBuilder<TKey, TValue>(config)
                .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                .SetStatisticsHandler((_, json) => Console.WriteLine($"Statistics: {json}"))
                .Build();
        try
        {
            var result = await producer.ProduceAsync(topic, new Message<TKey, TValue> { Key = key, Value = value });
            Console.WriteLine($"Delivered '{result.Value}' to '{result.TopicPartitionOffset}'");
        }
        catch (ProduceException<TKey, TValue> e)
        {
            Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            throw;
        }
    }
}
