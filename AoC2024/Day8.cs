using System.Numerics;

namespace AoC2024;

public class Day8
{
    private static readonly char[][] Grid = Array.ConvertAll(File.ReadLines("Day8.txt").ToArray(), row => row.ToCharArray());

    public Day8()
    {
        var locations = new Dictionary<char, List<Vector2>>();
        for (int y = 0; y < Grid.Length; y++)
        {
            for (int x = 0; x < Grid[y].Length; x++)
            {
                if (Grid[y][x] != '.')
                {
                    if (!locations.TryGetValue(Grid[y][x], out var value))
                    {
                        locations.Add(Grid[y][x], [new Vector2(x,y)]);
                    }
                    value?.Add(new Vector2(x,y));
                }
            }
        }

        var antiNodes = new HashSet<Vector2>();
        foreach (var (key, locationValue) in locations)
        {
            foreach (var cordA in locationValue)
            {
                foreach (var cordB in locationValue)
                {
                    if (cordA == cordB)
                    {
                        continue;
                    }

                    var diff = cordA - cordB;
                    var fistAntiNode = cordB + diff * -1;
                    var secondAntiNode = cordA + diff;

                    foreach (var node in (Vector2[])[fistAntiNode, secondAntiNode])
                    {
                        if (IsInBounds(node))
                        {
                            antiNodes.Add(node);
                        }   
                    }
                }
            }
        }
        
        Console.WriteLine($"[Day8] Task1: {antiNodes.Count}");
        
        var antiNodes2 = new HashSet<Vector2>();
        foreach (var (key, locationValue) in locations)
        {
            foreach (var cordA in locationValue)
            {
                foreach (var cordB in locationValue)
                {
                    if (cordA == cordB)
                    {
                        continue;
                    }

                    var diff = cordA - cordB;
                    var fistAntiNode = new Vector2(cordA.X, cordA.Y);
                    var secondAntiNode = new Vector2(cordB.X, cordB.Y);
                    while (true)
                    {
                        fistAntiNode += -1 * diff;
                        secondAntiNode += diff;
                        if (!IsInBounds(fistAntiNode) && !IsInBounds(secondAntiNode))
                        {
                            break;
                        }
                        
                        foreach (var node in (Vector2[])[fistAntiNode, secondAntiNode])
                        {
                            if (IsInBounds(node))
                            {
                                antiNodes2.Add(node);
                            }   
                        }
                    }

                    
                }
            }
        }
        
        Console.WriteLine($"[Day8] Task2: {antiNodes2.Count}");
    }

    private static bool IsInBounds(Vector2 point)
    {
        return point.X >= 0 && point.Y >= 0 && point.X < Grid[0].Length && point.Y < Grid.Length;
    }
}