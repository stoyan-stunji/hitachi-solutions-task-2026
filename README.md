# Hitachi Solutions - Assessment Task - SPACE 2026 :rocket:
`C#.NET Console Application`, която симулира космическа навигация на астронавти в поле с различни препятствия. Системата намира най-късите пътища от всеки астронавт до космическа станция т.е. от начална до крайна точка, и визуализира най-късите им пътища.

### Приложението поддържа:
- Ръчно въвеждане на карта: брой редици, брой колони и самата карта;
- Генериране на случайна карта спрямо въведен от потребителя брой астронавти и препятствия;
- `Email` отчет за съответната мисия.

### Основни функционалности:
- `Парсване на карта`:
    - Чете от конзолата;
    - Валидира:
        - Дали броят редове и броят колони е в интервала `[2,100]`;
        - Дали има между `1` и `3` астронавта;
        - Дали има точно `1` дестинация;
    - Поддържа символите:
        - `S1`, `S2`, `S3` (астонавти/начални точки);
        - `F` (космическа станция/крайна точка);
        - `0` и `O` (проходимо поле);
        - `X` (непроходимо поле/астероид);
- `Pathfinding система`: реализирана е чрез `Strategy Pattern`, за да се скрият импленетационните детайли на алгоритъма за намиране най-къс път от начална до крайна точка. В случая, имаме интерфейс `SSSPStrategy`, който е имплементиран от класа `DijkstraStrategy`, реализиращ алгоритъма на `Dijkstra`:
```
public interface SSSPStrategy {
    PathResultDto FindPath(Grid grid, Position source, Position destination);
}
```
- Има `4` посоки: `up`, `down`, `left`, `right` като всяко поле има съответната си цена за преминаване през него. В случая, `F` и `O`/`0` са с цена `1`, докато `X` е непроходимо поле и е с цена `Integer.maxValue`;
- Алгоритъмът на `Dijkstra` използва: `PriorityQueue<Position, int>`, dictionary за разстоянията и проследяване на tile-parent;

### Execution Flow
1. Зареждане или генериране на карта;
2. За всеки астронавт изчисляване на най-къс път до `F`;
3. Сортиране по кратност на пътя (използвайки `OrderBy`, но неуспешните мисии се визуализират първи);
4. Показване на резултатите;
5. Генериране на `Email` отчет (по избор).

### Clean OOP layered architecture:
- `DataStructures`:
  - `Grid` - представя картата;
  - Йерархия от `Tile(s)`;
  - `Astronaut`;
  - `Position`;
- `Commands Layer`: Реализира `Command Pattern` чрез интерфейса `Command`, използвайки и `Factory Pattern` - `InputCustomMapCommand` и `ExitCommand`;
- `PathFinding`:
  - `SSSPStrategy`;
  - `DijkstraStrategy`;
- `DTO Layer` - за data transfer между различни layer-и;
- `Services`/`Mission`:
  - `MissionRunnerService` - изпълнява мисията;
  - `MissionNavigatorService` - обвързва астронавта с пътя му;
  - `MissionPrinter` - визуализира резултата от мисията.
```
Input Number Of Rows: 5
Input Number Of Cols: 7
Input Cosmic Map: 
S1 0 X 0 0 0 S2
X 0 0 0 0 X 0
X X 0 X 0 X 0
0 X X 0 0 X 0
0 X X 0 0 0 F

Astronaut S2 - Shortest path: 4
S1 0 X 0 0 0 S2
X 0 0 0 0 X *
X X 0 X 0 X *
0 X X 0 0 X *
0 X X 0 0 0 F

Astronaut S1 - Shortest path: 10
S1 * X 0 0 0 S2
X * * * * X 0
X X 0 X * X 0
0 X X 0 * X 0
0 X X 0 * * F
```

### Бонус задачи:
1. Добавено е ново поле `D` (отломки), което е проходимо, но с цена `2`. `DijkstraStrategy` вече не отчита най-къс път, а "най-лек" път т.е. пътят, който има най-малка цена до съответната дестинация;
```
Input Number Of Rows: 3
Input Number Of Cols: 3
Input Cosmic Map: 
S1 D F
O X O
O O O

Astronaut S1 - Shortest path: 3
S1 * F
0 X 0
0 0 0
```
2. Още в началото е изполвана `Strategy Pattern` за скриване на имплементация на алгоритъма за най-къс път. Възползвайки се от интерфейса `SSSPStrategy`, можем да създаден нов клас, например `BellmanFordStrategy` (не е имплементиран), и да подменим алгоритъма в приложението чрез `Dependency Injection` в конструкторите на класовете, ползващи алгоритъма;
```
public sealed class BellmanFordStrategy : SSSPStrategy {
    public PathResultDto FindPath(Grid grid, Position source, Position destination) {
        throw new NotImplementedException();
    }
}
```
3. Към `Service Layer`-a е добавен модул `Email` за изпращане на доклад за мисията на всеки астронавт като се въвежда имейл адрес на изпращач, паролата му и имейл адрес на получателя. Установява се връзка чрез `SMTP`. `TO:DO - липсва authentication`;
- `Services`/`Email`:
  - `EmailMissionWorkflowService`;
  - `EmailMissionResultsSender`;
  - `EmailMissionReportBuilder`;
  - `SmtpEmailService`;
4. Втората команда `RandomizeMapCommand` реализира `Dynamic Map Generation` като се въвеждат брой астронавти, брой астероиди и брой отломки, спазвайки ограниченията от `InputCustomMapCommand`.
```
Hitachi Solutions Europe - Assessment Task - SPACE 2026
[1]: Custom Map
[2]: Random Map
[3]: Exit
```
