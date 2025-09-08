### [На главную](../README.md)
# NotesFEServiceSettings.json

Файл конфигурации сервиса NotesFEService.

**Следует обратить внимание на раздел Jwt**

- UserApiBaseUrl - адрес API UserService. Зависит от hostname и порта UserService.
- NotesApiBaseUrl - адрес API NotesService. Зависит от hostname и порта NotesService.
- PathBase - корень Url
- Kestrel
  - EndPoints - адрес и порт, на котором сервис слушает соединение.
- Logging - настройки логгирования
- AllowedHosts - паттерн адресов, с которых разрешены запросы.
- Jwt - настройки проверки токенов Jwt
  - Issuer - валидный издатель токена
  - Audience - валидная цель токена
  - Key - **уникальный симметричный ключ подписи токена, должен быть создан случайным образом** и совпадать с таковым в конфигурации UserService