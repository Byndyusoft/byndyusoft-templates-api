# Шаблон сервиса .NET Core

## Что включает?
Шаблон включает:
 - Настройка swagger
 - Пакет для трассировки [Byndyusoft.Tracing](https://github.com/Byndyusoft/byndyusoft-tracing)
 - Пакет для логирования [Byndyusoft.Logging](https://github.com/Byndyusoft/Byndyusoft.Logging)
 - Пакет для работы с БД [Byndyusoft.Data.Relational](https://github.com/Byndyusoft/Byndyusoft.Data.Relational)
 - Хэлчеки через метод /healthz
 - Подключены основные технические метрики Prometheus

Проекты:
- Byndyusoft.Template.Domain - бизнес-логика приложения
- Byndyusoft.Template.DataAccess - слой доступа к данным
- Byndyusoft.Template.Migrator - мигратор базы данных на основе https://github.com/fluentmigrator/fluentmigrator
- Byndyusoft.Template.Api - веб-апи приложения
- Byndyusoft.Template.Api.Client - клиент для веб-апи, расширение для подключения клиента в потребителе
- Byndyusoft.Template.Api.Contracts - Контракты для веб-апи и клиента
- Byndyusoft.Template.IntegrationTests - интеграционные тесты на веб-апи



# Как использовать шаблон?
### Установка шаблона из nuget в консоли Windows:
`dotnet new --install Byndyusoft.Template`

В списке должен появиться шаблон с коротким именем bsapi.

### Создание нового сервиса из шаблона (выполнять в пустой директории)
`dotnet new bsapi -n {Название сервиса}`

Проект готов к использованию!

# Maintainers
github.maintain@byndyusoft.com
