1. Открыть Package Manager
2. Удостовериться, что Default project: DataLayer
3.1 Выполнить команду Add-Migration InitCreate
3.2 Выполнить команду Update-Database

4. Запустить приложение TestAppBrightSoft
4.1 Для перехода к справочнику мета-данных внизу есть кнопка

5. ReportApp — Сервис отчётов. Index-метод может принимать на вход json (пример json лежит в корне). 
Для упрощения он задаётся внутри метода Index
//json = "{\'Strings\':{\'Table\':\'directoryObjects\',\'Data\':\'Code, Name\'},\'Columns\':{\'Table\':\'Versions\',\'Data\':\'Name\'}}";