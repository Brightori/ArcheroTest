# ArcheroTest
Demo project with simple architecture

Вся логика построена на бихейверах, компонентах и контроллерах, все основные контроллеры лежат на сцене, 
Все необходимые настройки хранятся в папке Configs. Юниты присутствуют двух видов - те что рандомно шарахаются и те что изначально генерят 1 путь и его придерживаются.
У них один тип бихейвера, выбор поведения регулируется конфигом с нужным флагом, там же в конфиге можно настроить радиус перемещения (в рамках которого юнит выбирает точку для перемещения), разброс ожидания в точки прибытия.

Уровень построен на прегенерации, наверху есть меню для генерации уровня нужного размера. На данном этапе стенки и заграждения ставятся вручную. 
