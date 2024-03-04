using BackEnd.Domain.SeedWork;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace BackEnd.Infra.Data.EF.Repositories;
#nullable disable
public class RabbitMQClient : IDisposable
{
    private readonly IConnection _connection;
    public RabbitMQClient(IOptions<RabbitMQConfiguration> rabbitMQConfig)
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = rabbitMQConfig.Value.HostName,
                UserName = rabbitMQConfig.Value.UserName,
                Password = rabbitMQConfig.Value.Password,
                Port = rabbitMQConfig.Value.Port
            };

            _connection = factory.CreateConnection();
        }
        catch (Exception) { }
    }

    public IModel CreateChannel()
    {
        if (_connection is not null)
            return _connection.CreateModel();

        return default!;
    }

    public void Dispose()
    {
        if (_connection is not null)
            _connection.Close();
    }
}