# Monopoly App

Консольное .NET приложение для склада, описывающее иерархию объектов паллет и коробок, и предоставляющее возможность сгруппировать паллеты по сроку годности с сортировкой по весу, а также отсортировать и выбрать три паллеты с наибольшими сроками годности.
Приложение поддерживает ручное добавление новых паллет и коробок, автоматическую генерацию множества паллет с коробками, а также сохранение и загрузка паллет из текстового файла.

Решение состоит из трёх проектов.

##Monopoly
Содержит в себе основные модели для приложения, а также класс для их обработки. Также содержит класс для сериализации и десериализации паллет.

##MonopolyConsole
Содержит в себе реализацию управления паллетами с консоли.

##MonopolyTests
Содержит модульные тесты (пока только касаемые создания новых паллет и коробок).
