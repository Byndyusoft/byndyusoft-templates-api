# Шаблон сервиса .NET Core

## Что включает?
Шаблон включает:
 - Настройка swagger
 - Пакет для трассировки http-запросов [Byndyusoft.AspNetCore.Instrumentation.Tracing](https://github.com/Byndyusoft/Byndyusoft.AspNetCore.Instrumentation.Tracing)
 - Пакет для логирования [Byndyusoft.Logging](https://github.com/Byndyusoft/Byndyusoft.Logging)
 - Пакет для работы с БД [Byndyusoft.Data.Relational](https://github.com/Byndyusoft/Byndyusoft.Data.Relational)
 - Пакет для маскирования чувствительных данных в логах и в трассировке [Byndyusoft.MaskedSerialization](https://github.com/Byndyusoft/Byndyusoft.MaskedSerialization)
 - Хэлчеки через метод /healthz
 - Подключены OpenTelemetry Tracing & Metrics. Добавлены основные инструменты для метрик. Добавлен пример для метрик приложения.

Проекты:
- Domain - бизнес-логика приложения
- DataAccess - слой доступа к данным
- Migrator - мигратор базы данных на основе https://github.com/fluentmigrator/fluentmigrator
- Api - веб-апи приложения
- Api.Client - клиент для веб-апи, расширение для подключения клиента в потребителе
- Api.Contracts - контракты для веб-апи и клиента
- IntegrationTests - интеграционные тесты на веб-апи
- UnitTests - юнит-тесты

# Как использовать шаблон?

## Установка шаблона из nuget.org

### Установка шаблона из nuget в консоли Windows:
```shell
dotnet new --install Byndyusoft.DotNet.Web.ProjectTemplate
```

В списке должен появиться шаблон с коротким именем bsapi.

### Создание нового сервиса из шаблона (выполнять в пустой директории)
```shell
dotnet new bsapi -n {Название сервиса}
```

Проект готов к использованию!

## Установка шаблона из файла пакета .nupkg.

### Создание nuget пакета для шаблона в корне:
```shell
dotnet pack
```

Вместо создания пакета его можно скачать из [nuget.org](https://www.nuget.org/packages/Byndyusoft.Template).

### Установка шаблона из созданного нами пакета:
```shell
dotnet new --install .\Byndyusoft.Template.1.3.6.nupkg
```

**Примечание:** Версия может отличаться.

### Создание нового сервиса из шаблона

#### Создание в текущей директории (выполнять в пустой директории):
```shell
dotnet new bsapi -n {Название сервиса}
```

#### Создание директории с сервисом:
```shell
dotnet new bsapi -o {Название сервиса}
```

Проект готов к использованию!

# Пример с метриками

В коде был добавлен пример с метриками, которые определяются в классе *ApiTemplateMetrics.cs*.

В этом классе добавлены поля (терминология взята из [OpenTelemetry Metrics API](https://opentelemetry.io/docs/reference/specification/metrics/api/)):

 - *Name* - наименование meter (измеритель).
 - *DurationName* - наименование instrument (инструмента).
 - *DurationBuckets* - границы бакетов, используются в настройке отображения метрик в классе *MeterProviderBuilderExtensions.cs*.

# Настройки проекта

## Фильтрация запросов для трассировки

В проекте добавлены список запросов, которые не будут попадать в трассироку. Они описаны в файле [TracerProviderBuilderExtensions.cs](https://github.com/Byndyusoft/byndyusoft-templates-api/blob/main/templates/src/Api/Infrastructure/OpenTelemetry/TracerProviderBuilderExtensions.cs):

```csharp
var ignoredSegments = new[] { "/swagger", "/favicon", "/healthz", "/metrics" };
```

Если нужно игнорировать какие-то дополнительные запросы, то их можно добавить в список в этом классе.

## Настройки подключения к Jaeger

Для подключения к Jaeger нужно добавить секцию *Jaeger* в файл настроек:

```json
"Jaeger": {
  "AgentHost": "localhost",
  "AgentPort": 6831
}
```

## Интеграция OpenTelemetry с JaegerTracing
Для интеграции нужно использовать класс [JaegerPropagator](https://github.com/open-telemetry/opentelemetry-dotnet/blob/1da5b8623c2bc82fe3681e3c082a6b8e685b66b9/src/OpenTelemetry.Extensions.Propagators/JaegerPropagator.cs). Если этого класса еще нет в текущем релизе, можно использовать этот класс из исходников. После релиза он должен появиться в пакете [OpenTelemetry.Extensions.Propagators](https://www.nuget.org/packages/OpenTelemetry.Extensions.Propagators). 

[Пример](https://github.com/open-telemetry/opentelemetry-dotnet/blob/25cfa1721fd8d55ca5e9ff55a8ac610bba9d69d1/examples/MicroserviceExample/Utils/Messaging/MessageSender.cs#77) использования Propagator для RabbitMq.

# Maintainers
github.maintain@byndyusoft.com
