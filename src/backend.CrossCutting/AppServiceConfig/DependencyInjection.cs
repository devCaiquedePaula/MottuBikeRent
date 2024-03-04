using BackEnd.Application.CQRS.Entregadors.Read;
using BackEnd.Application.CQRS.Entregadors.Write;
using BackEnd.Application.CQRS.Motos.Read;
using BackEnd.Application.CQRS.Motos.Write;
using BackEnd.Application.RabbitConsumer;
using BackEnd.Domain.SeedWork;
using BackEnd.Infra.Data.EF.Context;
using BackEnd.Infra.Data.EF.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackEnd.CrossCutting.AppServiceConfig;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection _services,
        IConfiguration _configuration
    )
    {
        string conexao = _configuration.GetConnectionString("DatabaseSettings")!;

        _services.AddDbContext<PgDbContext>(options =>
                options.UseNpgsql(conexao)
        );

        _services.Configure<RabbitMQConfiguration>(_configuration.GetSection("RabbitMqConfig"));
        _services.AddTransient<IRabbitMQMessageRepository, RabbitMQMessageRepository>();

        _services.AddScoped<IRepository, Repository>();
        _services.AddScoped<IRepositoryDapper, RepositoryDapper>();
        _services.AddScoped<IUnitOfWork, UnitOfWork>();

        _services.AddSingleton<PedidoConsumeService>();
        _services.AddHostedService<PedidoConsumeService>();

        _services.AddMediatR(typeof(CreateMotoCommandHandler).Assembly);
        _services.AddMediatR(typeof(UpdateMotoCommandHandler).Assembly);
        _services.AddMediatR(typeof(UpdateMotoPlacaCommandHandler).Assembly);
        _services.AddMediatR(typeof(DeleteMotoCommandHandler).Assembly);
        _services.AddMediatR(typeof(GetMotosAllHandler).Assembly);
        _services.AddMediatR(typeof(GetMotoByIdHandler).Assembly);

        _services.AddMediatR(typeof(CreateEntregadorCommandHandler).Assembly);
        _services.AddMediatR(typeof(UpdateEntregadorCommandHandler).Assembly);
        _services.AddMediatR(typeof(UpdateEntregadorCNHCommandHandler).Assembly);
        _services.AddMediatR(typeof(DeleteEntregadorCommandHandler).Assembly);
        _services.AddMediatR(typeof(GetEntregadoresAllHandler).Assembly);
        _services.AddMediatR(typeof(GetEntregadorByIdHandler).Assembly);

        return _services;
    }
}