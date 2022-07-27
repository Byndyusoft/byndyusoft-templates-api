# Шаблон сервиса .NET Core

## Что включает?
Шаблон включает:
 - Настройка swagger
 - Пакет для трассировки [Byndyusoft.Tracing](https://github.com/Byndyusoft/byndyusoft-tracing)
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
### Установка шаблона из nuget в консоли Windows:
`dotnet new --install Byndyusoft.Template`

В списке должен появиться шаблон с коротким именем bsapi.

### Создание нового сервиса из шаблона (выполнять в пустой директории)
`dotnet new bsapi -n {Название сервиса}`

Проект готов к использованию!

### Интеграция OpenTelemetry с JaegerTracing
Для интеграции нужно использовать класс [JaegerPropagator](https://github.com/Byndyusoft/Byndyusoft.Products.Extractor/blob/7075b685c8f27eeacdc2578e11b71a68c7715af1/src/Byndyusoft.Messaging.Core/Instrumentation/ActivityContextPropagation.cs). Если этого класса еще не в текущем релизе, можно использовать этот класс из исходников. После релиза он должен появиться в пакете [OpenTelemetry.Extensions.Propagators](https://www.nuget.org/packages/OpenTelemetry.Extensions.Propagators). 

[Пример](https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry.Extensions.Propagators/JaegerPropagator.cs) использования Propagator в RabbitMq.

# Maintainers
github.maintain@byndyusoft.com
