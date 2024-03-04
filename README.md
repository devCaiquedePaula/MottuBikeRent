# Sistema de Gerenciamento de Aluguel de Motos

Este projeto é um sistema de gerenciamento de aluguel de motos e entrega por meio de uma plataforma online. Ele foi construído utilizando .NET Core, MongoDB como banco de dados NoSQL, e RabbitMQ para comunicação assíncrona.

## Funcionalidades

### Entregadores

-   Cadastro de entregadores com informações detalhadas.
-   Consulta individual de entregadores.
-   Notificação de pedidos disponíveis.

### Admin

-   Cadastro de motos.
-   Consulta de motos existentes.

### Pedidos

-   Criação de pedidos.
-   Notificação assíncrona de pedidos disponíveis aos entregadores.
-   Aceitação e entrega de pedidos por parte dos entregadores.
-   Cálculo de custos de locação.

## Estrutura do Projeto

-   `Models`: Contém as classes de modelo para Entregador, Moto, Pedido e NotificaçãoPedido.
-   `Controllers`: Contém os controladores para Entregador e Admin.
-   `Services`: Contém o serviço PedidoService para gerenciar pedidos.
-   `Consumers`: Contém o consumidor PedidoConsumer para processar notificações assíncronas.

## Licença

Este projeto é licenciado sob a Licença MIT - veja o arquivo [LICENSE.md](LICENSE.md) para detalhes.

