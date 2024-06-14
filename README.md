Проект является частью выпускной квалификационной работы бакалавра на тему "Разработка программного комлекса для мониторинга платной парковки", в данном случае это сетевое приложение. Технологии выбраны Razor Pages на платформе ASP.NET Core 8, Entity Framework, для работы с базой данных и аутентификация на основе JWT-токенов.
Для веб-приложения реализованы такие возможности:
1. отображение схемы загруженности парковки;
2. отображение видеопотока с камеры парковки с/без визуализации заня-тых парковочных мест;
3. предоставление информации о количестве занятых мест;
4. авторизация/регистрация с использованием подтверждения по элек-тронной почте;
5. изменение пользовательских данных и пароля;
6. аутентификация с применением JWT-токенов;
7. разграничение доступа к административным страницам;
8. доступ к ручному изменению занятости любого парковочного места;
9. доступ к ручному изменению данных о пользователе;
10. получение статистики о средней загруженности парковки по дням, ко-личеству въехавших/покинувших парковку машинах за период;
11. взаимодействие с базой данных с использованием Entity Framework;
12. использование защищенного соединения и предоставление SSL-сертификата. 

Схема сетевого приложения в рамках инфраструктуры:

![image](https://github.com/70Null07/Parking-Control-Web-Application/assets/76547066/07702dcc-b474-4069-a6c6-88c15aab86a8)

Возможные действия пользователей:

![image](https://github.com/70Null07/Parking-Control-Web-Application/assets/76547066/dcdd021a-d308-435e-a5d4-25bd4b16beda)


Диаграмма активности для регистрации клиентов:

![image](https://github.com/70Null07/Parking-Control-Web-Application/assets/76547066/315a44da-4f9e-48e0-8f0c-c5e1b2c54c1e)

Некоторые страницы сайта для пользователей:

![image](https://github.com/70Null07/Parking-Control-Web-Application/assets/76547066/0e9c38a6-a388-4f41-a067-c36fe91478a8)
![image](https://github.com/70Null07/Parking-Control-Web-Application/assets/76547066/7f883f15-8f71-4dac-bd0e-00f530f1f499)
![image](https://github.com/70Null07/Parking-Control-Web-Application/assets/76547066/debbad50-52d4-4c0a-bd2b-10dfad56b9b5)
![image](https://github.com/70Null07/Parking-Control-Web-Application/assets/76547066/abe5b1ce-014c-4ad3-8a74-58df5399d646)
![image](https://github.com/70Null07/Parking-Control-Web-Application/assets/76547066/734d7c95-1aec-492c-a31f-555d67a5d6e8)
![image](https://github.com/70Null07/Parking-Control-Web-Application/assets/76547066/53a9691a-4b6e-4d0a-8753-947d8dd93e36)

Физическая модель базы данных:

![image](https://github.com/70Null07/Parking-Control-Web-Application/assets/76547066/a72ddbfb-225a-41fd-b29c-81d1fa98e6e1)
