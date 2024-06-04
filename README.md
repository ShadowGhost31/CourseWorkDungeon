# Dungeon Hero

Dungeon Hero — це інтерактивна консольна гра, де гравці переміщуються через різні рівні підземелля, борються з ворогами та збирають скарби. Цей документ надає вичерпний гід по функціоналу програми, як запустити її локально, а також деталі про принципи програмування, патерни проєктування та техніки рефакторингу, використані в проєкті.

## Зміст
- [Функціонал](#Функціонал)
- [Запуск програми локально](#Запуск-програми-локально)
- [Принципи програмування](#Принципи-програмування)
- [Патерни проєктування](#Патерни-проєктування)
- [Техніки рефакторингу](#Техніки-рефакторингу)
- [Коментарі до коду](#Коментарі-до-коду)

## Функціонал
Dungeon Hero пропонує наступні можливості:
- **Навігація героя**: Гравці можуть переміщувати свого героя через різні рівні підземелля.
- **Система бою**: Боротьба з різними ворогами.
- **Збір скарбів**: Знаходження та збирання скарбів для збільшення очок і можливостей.
- **Прогресія рівнів**: Проходження різних рівнів зі збільшенням складності.
- **Аудіо ефекти**: Відтворення звукових ефектів для покращення занурення у гру.
- **Контейнери для предметів**: Додавання, видалення та розкидання предметів у контейнерах.

## Запуск програми локально
Щоб запустити гру Dungeon Hero локально, виконайте наступні кроки:

### Клонувати репозиторій
```sh
git clone https://github.com/ShadowGhost31/CourseWorkDungeon.git
cd CourseWorkDungeon
```
## Зібрати проект
Переконайтеся, що у вас встановлений .NET SDK. Потім виконайте наступну команду:
```sh
dotnet build
```
## Запустити гру
```sh
 dotnet run
```
### Принцип єдиного обов'язку (SRP)

Принцип єдиного обов'язку стверджує, що кожен клас повинен мати лише одну причину для зміни, тобто відповідати лише за одну функціональність. Наприклад, у вашій програмі DungeonHero відповідає за обробку атрибутів та дій героя, тоді як DungeonLevel управляє структурою та прогресом підземелля.

### Принцип відкритості/закритості (OCP)

Цей принцип стверджує, що програма має бути відкритою для розширення, але закритою для модифікації. Тобто, нові функціональності можуть бути додані без зміни вже існуючого коду. У вашій системі, нові рівні, вороги або предмети можуть бути додані шляхом розширення існуючих класів, не модифікуючи їх.

### Принцип підстановки Лісков (LSP)

Принцип підстановки Лісков стверджує, що об'єкти повинні бути замінними на екземпляри їх підтипів без зміни правильності програми. Наприклад, ви можете вводити різні типи ворогів, усі з яких дотримуються загального інтерфейсу.

### Принцип розділення інтерфейсу (ISP)

Принцип розділення інтерфейсу стверджує, що клієнти не повинні залежати від інтерфейсів, які вони не використовують. Інтерфейси повинні бути специфічними для конкретних дій. Це забезпечує, що класи-реалізації повинні визначати лише ті методи, які для них актуальні.

### Принцип інверсії залежностей (DIP)

Принцип інверсії залежностей стверджує, що високорівневі модулі не повинні залежати від низькорівневих модулів. Вони повинні залежати від абстракцій. У вашій грі, різні компоненти взаємодіють через інтерфейси та абстрактні класи, що дозволяє зберігати систему більш гнучкою.
