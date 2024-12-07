using System.Numerics;

namespace AoC2024;

public class Day6
{
    private static readonly string[] Lines = File.ReadLines("Day6.txt").ToArray();
    public Day6()
    {
        var grid = Array.ConvertAll(Lines, row => row.ToCharArray());
        var start = new Vector2(-1, -1);
        var visited = new HashSet<(int, int)>();
        for (int row = 0; row < grid.Length; row++)
        {
            var col = Array.IndexOf(grid[row], '^');
            if (col != -1)
            {
                start = new Vector2(col, row);
            }
        }

        if (start == new Vector2(-1,-1))
        {
            throw new ArgumentNullException("start");
        }

        var direction = new Vector2(0, -1);
        var cPos = start;
        while (true)
        {
            visited.Add(((int, int))(cPos.X, cPos.Y));

            var nextPos = cPos + direction;
            if (nextPos.Y >= grid.Length || nextPos.X >= grid[0].Length || nextPos.X < 0 || nextPos.Y < 0)
            {
                break;
            }

            if (grid[(int)nextPos.Y][(int)nextPos.X] == '#')
            {
                direction = new Vector2(-direction.Y, direction.X);
            }

            cPos += direction;
        }
        
        Console.WriteLine($"[Day6] Task1: {visited.Count}");

        
        var looped = 0;
        foreach (var (x,y) in visited)
        {
            if (grid[y][x] != '.') continue;

            grid[y][x] = '#';
            var visited2 = new HashSet<(int, int, int, int)>();
            cPos = start;
            direction = new Vector2(0, -1);
            while (true)
            {
                var nextPos = cPos + direction;
                visited2.Add(((int, int, int, int))(cPos.X, cPos.Y, direction.X, direction.Y));
                if (nextPos.Y >= grid.Length || nextPos.X >= grid[0].Length || nextPos.X < 0 || nextPos.Y < 0)
                {
                    break;
                }

                if (grid[(int)nextPos.Y][(int)nextPos.X] == '#')
                {
                    direction = new Vector2(-direction.Y, direction.X);
                }
                else
                {
                    cPos += direction;
                }

                if (visited2.Contains(((int, int, int, int))(cPos.X, cPos.Y, direction.X, direction.Y)))
                {
                    looped++;
                    break;
                }
            }
            grid[y][x] = '.';
        }
        
        Console.WriteLine($"[Day6] Task2: {looped}");
    }
}