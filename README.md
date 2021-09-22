# Шаблон сервиса .NET Core

## Что включает?
Шаблон включает 8 проектов для полноценного запуска сервиса. Подключены swagger, jaeger, nlog. Добавлены сервисы для работы с базой данных, RabbitMQ, S3-хранилищем. API поддерживает версионирование, health-check\status.

- Byndyusoft.Template.Domain - бизнес-логика приложения
- Byndyusoft.Template.DataAccess - слой доступа к данным
- Byndyusoft.Template.Migrator - мигратор базы данных на основе https://github.com/fluentmigrator/fluentmigrator
- Byndyusoft.Template.Api - веб-апи приложение, с добавленной фоновой службой
- Byndyusoft.Template.Api.Client - клиент для веб-апи
- Byndyusoft.Template.Api.Shared - DTO для веб-апи и клиента, расширение для подключения клиента в потребителе
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
