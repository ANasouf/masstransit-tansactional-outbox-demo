# MassTransit Transactional Outbox Demo

This repository demonstrates a simple implementation of [MassTransit's Transactional Outbox pattern](https://masstransit.io/documentation/patterns/transactional-outbox) in a .NET solution. The solution consists of two projects: Publisher and Consumer. The Publisher project publishes messages to a message queue, and the Consumer project processes those messages.

## Getting Started

### Prerequisites

- .NET SDK (version 8 or higher)
- Docker (for running rabbitmq, sql server)

### Running the Demo

1. Clone this repository:

   ```bash
   git clone https://github.com/yourusername/masstransit-transactional-outbox-demo.git
   ```

1. Run docker compose:

   ```bash
   docker compose up -d
   ```

1. Uppdate database migrations:

   ```bash
   dotnet ef database update -s Publisher
   ```

   ```bash
   dotnet ef database update -s Consumer
   ```

1. Run both projects:

   ```bash
   dotnet run --project Publisher
   ```

   ```bash
   dotnet run --project Consumer
   ```
