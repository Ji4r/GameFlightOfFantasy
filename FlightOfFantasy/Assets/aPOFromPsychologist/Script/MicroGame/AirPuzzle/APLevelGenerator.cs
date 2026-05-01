using System.Collections.Generic;
using UnityEngine;

namespace DiplomGames
{
    public enum AirType
    {
        Void,
        PointStart,
        PointEnd,
        Gorizontal,
        Vertical,
        Angular,
        Tee,
        Cross
    }

    public class APLevelGenerator
    {
        private readonly byte columnCount;
        private readonly byte rowsCount;

        private AirType[,] map;
        private bool mapIsBeingGenerated;
        private System.Random random;

        public APLevelGenerator(byte columnCount, byte rowsCount)
        {
            mapIsBeingGenerated = false;

            this.columnCount = columnCount;
            this.rowsCount = rowsCount;
            map = new AirType[rowsCount, columnCount];
            random = new System.Random();
        }

        public AirType[,] GetCurrentLevel() => map;

        public AirType[,] GenerateNewLevel()
        {
            if (mapIsBeingGenerated)
                throw new System.Exception("Идёт генерация карты, а вы посылаете второй запрос на её генерацию");

            mapIsBeingGenerated = true;
            ClearMap();

            SelectStartAndEndPoint(ref map);
            CreatePath();

            mapIsBeingGenerated = false;
            return map;
        }

        public void PrintMap()
        {
            string output = "";

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    output += GetSymbol(map[i, j]) + " ";
                }
                output += "\n";
            }

            Debug.Log(output);
        }

        private char GetSymbol(AirType type)
        {
            return type switch
            {
                AirType.Void => '.',
                AirType.PointStart => 'S',
                AirType.PointEnd => 'E',
                AirType.Gorizontal => '─',
                AirType.Vertical => '│',
                AirType.Angular => '└',
                AirType.Tee => '┬',
                AirType.Cross => '┼',
                _ => '?'
            };
        }

        private void SelectStartAndEndPoint(ref AirType[,] map)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            // Старт на левой границе
            map[Random.Range(0, rows), 0] = AirType.PointStart;

            // Конец на правой границе
            map[Random.Range(0, rows), cols - 1] = AirType.PointEnd;
        }

        private void CreatePath()
        {
            // Находим позиции старта и конца
            Vector2Int startPos = FindPosition(AirType.PointStart);
            Vector2Int endPos = FindPosition(AirType.PointEnd);

            if (startPos == null || endPos == null)
            {
                Debug.LogError("Не удалось найти старт или конец!");
                return;
            }

            // Генерируем путь
            List<Vector2Int> path = GenerateRandomPath(startPos, endPos);

            // Заполняем путь трубами
            FillPathWithPipes(path);
        }

        private Vector2Int FindPosition(AirType type)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == type)
                        return new Vector2Int(j, i);
                }
            }
            return new Vector2Int(-1, -1);
        }

        private List<Vector2Int> GenerateRandomPath(Vector2Int start, Vector2Int end)
        {
            List<Vector2Int> path = new List<Vector2Int>();
            path.Add(start);

            Vector2Int current = start;

            // Защита от бесконечного цикла (максимум шагов)
            int maxSteps = rowsCount * columnCount * 2;
            int steps = 0;

            while (current.x < end.x && steps < maxSteps)
            {
                steps++;

                // Определяем доступные направления
                List<Vector2Int> availableMoves = GetAvailableMoves(current, end);

                if (availableMoves.Count == 0)
                {
                    // Тупик — возвращаемся назад и пробуем другой путь
                    if (path.Count > 1)
                    {
                        path.RemoveAt(path.Count - 1);
                        current = path[path.Count - 1];
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                // Выбираем случайное направление
                Vector2Int next = availableMoves[random.Next(availableMoves.Count)];
                path.Add(next);
                current = next;
            }

            // Убедимся, что путь достиг конца
            if (current.x != end.x || current.y != end.y)
            {
                // Если не достигли, добавляем прямой путь до конца
                path = AddStraightPath(path, end);
            }

            return path;
        }

        private List<Vector2Int> GetAvailableMoves(Vector2Int current, Vector2Int end)
        {
            List<Vector2Int> moves = new List<Vector2Int>();
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            // Вправо (приоритет, так как нужно двигаться к концу)
            if (current.x + 1 < cols && !IsPositionOccupied(current.x + 1, current.y))
            {
                moves.Add(new Vector2Int(current.x + 1, current.y));
            }

            // Вверх
            if (current.y - 1 >= 0 && !IsPositionOccupied(current.x, current.y - 1))
            {
                moves.Add(new Vector2Int(current.x, current.y - 1));
            }

            // Вниз
            if (current.y + 1 < rows && !IsPositionOccupied(current.x, current.y + 1))
            {
                moves.Add(new Vector2Int(current.x, current.y + 1));
            }

            // Влево (только если это ведёт к цели)
            if (current.x - 1 >= 0 && !IsPositionOccupied(current.x - 1, current.y) && current.x - 1 >= end.x)
            {
                moves.Add(new Vector2Int(current.x - 1, current.y));
            }

            return moves;
        }

        private bool IsPositionOccupied(int x, int y)
        {
            // Пустые клетки и клетки, не занятые стартом/концом
            return map[y, x] != AirType.Void && map[y, x] != AirType.PointEnd;
        }

        private List<Vector2Int> AddStraightPath(List<Vector2Int> path, Vector2Int end)
        {
            Vector2Int current = path[path.Count - 1];

            // Двигаемся прямо к цели
            while (current.x < end.x)
            {
                current = new Vector2Int(current.x + 1, current.y);
                path.Add(current);
            }

            // Если нужно сместиться по вертикали
            while (current.y < end.y)
            {
                current = new Vector2Int(current.x, current.y + 1);
                path.Add(current);
            }

            while (current.y > end.y)
            {
                current = new Vector2Int(current.x, current.y - 1);
                path.Add(current);
            }

            return path;
        }

        private void FillPathWithPipes(List<Vector2Int> path)
        {
            if (path.Count < 2) return;

            for (int i = 0; i < path.Count; i++)
            {
                Vector2Int pos = path[i];

                // Пропускаем старт и конец
                if (map[pos.y, pos.x] == AirType.PointStart || map[pos.y, pos.x] == AirType.PointEnd)
                    continue;

                // Определяем направление входа и выхода
                Vector2Int? prev = i > 0 ? path[i - 1] : (Vector2Int?)null;
                Vector2Int? next = i < path.Count - 1 ? path[i + 1] : (Vector2Int?)null;

                AirType pipeType = DeterminePipeType(prev, pos, next);
                map[pos.y, pos.x] = pipeType;
            }
        }

        private AirType DeterminePipeType(Vector2Int? prev, Vector2Int current, Vector2Int? next)
        {
            bool hasUp = false, hasDown = false, hasLeft = false, hasRight = false;

            // Проверяем соединение с предыдущей клеткой
            if (prev.HasValue)
            {
                if (prev.Value.x < current.x) hasLeft = true;
                if (prev.Value.x > current.x) hasRight = true;
                if (prev.Value.y < current.y) hasUp = true;
                if (prev.Value.y > current.y) hasDown = true;
            }

            // Проверяем соединение со следующей клеткой
            if (next.HasValue)
            {
                if (next.Value.x < current.x) hasLeft = true;
                if (next.Value.x > current.x) hasRight = true;
                if (next.Value.y < current.y) hasUp = true;
                if (next.Value.y > current.y) hasDown = true;
            }

            // Определяем тип трубы по количеству и расположению соединений
            int connectionCount = (hasUp ? 1 : 0) + (hasDown ? 1 : 0) + (hasLeft ? 1 : 0) + (hasRight ? 1 : 0);

            switch (connectionCount)
            {
                case 2:
                    // Прямая или угловая
                    if ((hasLeft && hasRight) || (hasUp && hasDown))
                        return AirType.Gorizontal;
                    else
                        return AirType.Angular;

                case 3:
                    return AirType.Tee;

                case 4:
                    return AirType.Cross;

                default:
                    return AirType.Gorizontal;
            }
        }

        private void ClearMap()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = AirType.Void;
                }
            }
        }
    }
}