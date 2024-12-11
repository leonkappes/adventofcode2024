using System.Numerics;

namespace AoC2024;

public class Day10
{
    private static readonly int[][] Grid =
        Array.ConvertAll(File.ReadLines("Day10.txt").ToArray(), row => row.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray());
    private static readonly Vector2[] Directions = [new Vector2(-1, 0), new Vector2(1,0), new Vector2(0,1), new Vector2(0,-1)];

    public Day10()
    {
        var starts = new List<Vector2>();
        for (var row = 0; row < Grid.Length; row++)
        {
            var occurences = Grid[row].Select((b,i) => b == 0 ? i : -1).Where(i => i != -1).ToList();
            starts.AddRange(occurences.Select(occurence => new Vector2(occurence, row)));
        }

        var neigbours = new Dictionary<Vector2, List<Vector2>>();
        for (int row = 0; row < Grid.Length; row++)
        {
            for (int col = 0; col < Grid[0].Length; col++)
            {
                var current = new Vector2(col, row);
                neigbours[current] = (from direction in Directions
                    let next = current + direction
                    where next.Y < Grid.Length && next.Y >= 0
                    where next.X < Grid[0].Length && next.X >= 0
                    where Grid[(int)next.Y][(int)next.X] - Grid[(int)current.Y][(int)current.X] == 1
                    select next).ToList();

            }
        }

        long totalPaths = 0;
        long uniquePaths = 0;
        foreach (var start in starts)
        {
            var queue = new Queue<Vector2>();
            var visitedEnds = new HashSet<Vector2>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (Grid[(int)current.Y][(int)current.X] == 9)
                {
                    totalPaths++;
                    visitedEnds.Add(current);
                    continue;
                }

                foreach (var neigbour in neigbours[current])
                {
                    queue.Enqueue(new Vector2(neigbour.X,neigbour.Y));
                }
            }

            uniquePaths += visitedEnds.Count;
        }
        
        Console.WriteLine($"[Day10] Task1: {uniquePaths}");
        Console.WriteLine($"[Day10] Task2: {totalPaths}");
    }
}