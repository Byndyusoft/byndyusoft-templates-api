# Шаблон сервиса .NET Core

## Что включает?
Шаблон включает:
 - Настройка swagger
 - Пакет для трассировки http-запросов [Byndyusoft.AspNetCore.Instrumentation.Tracing](https://github.com/Byndyusoft/Byndyusoft.AspNetCore.Instrumentation.Tracing)
 - Пакет для логирования [Byndyusoft.Logging](https://github.com/Byndyusoft/Byndyusoft.Logging)
 - Пакет для работы с БД [Byndyusoft.Data.Relational](https://github.com/Byndyusoft/Byndyusoft.Data.Relational)
 - Пакет для маскирования чувствительных данных в логах и в трассировке [Byndyusoft.MaskedSerialization](https://github.com/Byndyusoft/Byndyusoft.MaskedSerialization)
 - Хэлчеки через метод /healthz
 - Подключены основные технические метрики Prometheus

Проекты:
- Domain - бизнес-логика приложения
- DataAccess - слой доступа к данным
- Migrator - мигратор базы данных на основе https://github.com/fluentmigrator/fluentmigrator
- Api - веб-апи приложения
- Api.Client - клиент для веб-апи, расширение для подключения клиента в потребителе
- Api.Contracts - Контракты для веб-апи и клиента
- IntegrationTests - интеграционные тесты на веб-апи



# Как использовать шаблон?

## Установка шаблона из nuget.org

### Установка шаблона из nuget в консоли Windows:
`dotnet new --install Byndyusoft.Template`

В списке должен появиться шаблон с коротким именем bsapi.

### Создание нового сервиса из шаблона (выполнять в пустой директории)
`dotnet new bsapi -n {Название сервиса}`

Проект готов к использованию!

## Установка шаблона из файла пакета .nupkg.

### Создание nuget пакета для шаблона в корне:
`dotnet pack`

Вместо создания пакета его можно скачать из [nuget.org](https://www.nuget.org/packages/Byndyusoft.Template).

### Установка шаблона из созданного нами пакета:
`dotnet new --install .\Byndyusoft.Template.1.1.0.nupkg`

Версия может отличаться.

### Создание нового сервиса из шаблона (выполнять в пустой директории)
`dotnet new bsapi -n {Название сервиса}`

Проект готов к использованию!

# Интеграция OpenTelemetry с JaegerTracing
Для интеграции нужно использовать класс [JaegerPropagator](https://github.com/open-telemetry/opentelemetry-dotnet/blob/1da5b8623c2bc82fe3681e3c082a6b8e685b66b9/src/OpenTelemetry.Extensions.Propagators/JaegerPropagator.cs). Если этого класса еще не в текущем релизе, можно использовать этот класс из исходников. После релиза он должен появиться в пакете [OpenTelemetry.Extensions.Propagators](https://www.nuget.org/packages/OpenTelemetry.Extensions.Propagators). 

[Пример](https://github.com/open-telemetry/opentelemetry-dotnet/blob/25cfa1721fd8d55ca5e9ff55a8ac610bba9d69d1/examples/MicroserviceExample/Utils/Messaging/MessageSender.cs#77) использования Propagator для RabbitMq.

# Maintainers
github.maintain@byndyusoft.com
