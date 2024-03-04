using System.Text;
using BackEnd.Application.Dtos.Pedidos;
using BackEnd.Domain.Entities;
using BackEnd.Domain.SeedWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BackEnd.Application.RabbitConsumer;

public class PedidoConsumeService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<PedidoConsumeService> _logger;
    private readonly ConnectionFactory _factory;
    private const string QUEUE_NAME = "notification-order";

    public PedidoConsumeService(IServiceProvider serviceProvider, IOptions<RabbitMQConfiguration> rabbitMQConfig, ILogger<PedidoConsumeService> logger)
    {
        _serviceProvider = serviceProvider;
        _factory = new ConnectionFactory
        {
            HostName = rabbitMQConfig.Value.HostName,
            UserName = rabbitMQConfig.Value.UserName,
            Password = rabbitMQConfig.Value.Password,
            Port = rabbitMQConfig.Value.Port
        };
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: QUEUE_NAME,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            var listaEntregadoresNotificacao = JsonConvert.DeserializeObject<PedidosNotificadosByEntregadorId>(contentString);

            if (listaEntregadoresNotificacao is not null)
                if (listaEntregadoresNotificacao.ListaEntregadores is not null)
                    foreach (var entregador in listaEntregadoresNotificacao.ListaEntregadores)
                    {
                        var notification = new Notificacao()
                        {
                            PedidoId = listaEntregadoresNotificacao!.PedidoId,
                            EntregadorId = entregador,
                            DataNoticacao = DateTime.Now
                        };

                        await _unitOfWork.Repository.AddObject<Notificacao>(notification);

                        await _unitOfWork.CommitAsync();
                    }        

            _logger.LogWarning($"Send with sucess: {contentString}");

            channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        channel.BasicConsume(
            queue: QUEUE_NAME,
            autoAck: false,
            consumer: consumer
        );

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}