### [На главную](../README.md)
# NotesServiceSettings.json

Файл конфигурации сервиса NotesService.

- Kestrel
  - EndPoints - адрес и порт, на котором сервис слушает соединение.
- Logging - настройки логгирования
- AllowedHosts - паттерн адресов, с которых разрешены запросы.
- ConnectionStrings - подключения баз данных
  - NotesDatabase - строка подключения БД.

Строку подключения стоит взять из примера, заменив значение Password на значение переменной окружения POSTGRES_PASSWORD, указанной в database.env