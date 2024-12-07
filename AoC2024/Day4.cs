using System.Numerics;

namespace AoC2024;

public class Day4
{
    private static readonly string[] _lines = File.ReadLines("Day4.txt").ToArray();
    private const string Word = "XMAS";
    private const string Word2 = "MAS";
    private readonly int _height = _lines.Length;
    private readonly int _width = _lines.First().Length;
    private static readonly String[] Directions = ["up", "down", "left", "right", "ldown", "rdown", "lup", "rup"];
    private static readonly Vector2[] DirectionsDiagonal = [new Vector2(-1, 1), new Vector2(1,1), new Vector2(-1,-1), new Vector2(1,-1)];

    public Day4()
    {
        var foundWords = 0;
        var foundWords2 = 0;
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                foundWords += Directions.Count(dir => IsWordAtPoint(new Vector2(x, y), dir));

                var matches = 0;
                foreach (var direction in DirectionsDiagonal)
                {
                    var start = new Vector2(x, y);
                    var newDir = direction * -1;
                    var point = start + newDir;
                    for (int charIndex = 0; charIndex < Word2.Length; charIndex++)
                    {
                        var newPoint = point + (direction * charIndex);
                        var curChar = IsOutOfBounds(newPoint) ? '0' : _lines[(int)newPoint.Y][(int)newPoint.X];
                        var curWorChar = Word2[charIndex];
                        if (IsOutOfBounds(newPoint) || _lines[(int)newPoint.Y][(int)newPoint.X] != Word2[charIndex])
                        {
                            goto Abort;
                        }
                    }

                    matches++;
                    Abort: {}
                }
                if (matches == 2) foundWords2++;
            }
        }
        
        Console.WriteLine($"[Day4] Task1: {foundWords}");
        Console.WriteLine($"[Day4] Task2: {foundWords2}");
    }

    private bool IsWordAtPoint(Vector2 point, string direction)
    {
        switch (direction)
        {
            case "up":
                for (int charIndex = 0; charIndex < Word.Length; charIndex++)
                {
                    var newPoint = point + new Vector2(0, -1*charIndex);
                    if (IsOutOfBounds(newPoint) || _lines[(int)newPoint.Y][(int)newPoint.X] != Word[charIndex])
                    {
                        return false;
                    }
                }
                return true;
            case "down":
                for (int charIndex = 0; charIndex < Word.Length; charIndex++)
                {
                    var newPoint = point + new Vector2(0, 1*charIndex);
                    if (IsOutOfBounds(newPoint) || _lines[(int)newPoint.Y][(int)newPoint.X] != Word[charIndex])
                    {
                        return false;
                    }
                }
                return true;
            case "left":
                for (int charIndex = 0; charIndex < Word.Length; charIndex++)
                {
                    var newPoint = point + new Vector2(-1*charIndex, 0);
                    if (IsOutOfBounds(newPoint) || _lines[(int)newPoint.Y][(int)newPoint.X] != Word[charIndex])
                    {
                        return false;
                    }
                }
                return true;
            case "right":
                for (int charIndex = 0; charIndex < Word.Length; charIndex++)
                {
                    var newPoint = point + new Vector2(1*charIndex, 0);
                    if (IsOutOfBounds(newPoint) || _lines[(int)newPoint.Y][(int)newPoint.X] != Word[charIndex])
                    {
                        return false;
                    }
                }
                return true;
            case "ldown":
                for (int charIndex = 0; charIndex < Word.Length; charIndex++)
                {
                    var newPoint = point + new Vector2(-1*charIndex, 1*charIndex);
                    if (IsOutOfBounds(newPoint) || _lines[(int)newPoint.Y][(int)newPoint.X] != Word[charIndex])
                    {
                        return false;
                    }
                }
                return true;
            case "rdown":
                for (int charIndex = 0; charIndex < Word.Length; charIndex++)
                {
                    var newPoint = point + new Vector2(1*charIndex, 1*charIndex);
                    if (IsOutOfBounds(newPoint) || _lines[(int)newPoint.Y][(int)newPoint.X] != Word[charIndex])
                    {
                        return false;
                    }
                }
                return true;
            case "lup":
                for (int charIndex = 0; charIndex < Word.Length; charIndex++)
                {
                    var newPoint = point + new Vector2(-1*charIndex, -1*charIndex);
                    if (IsOutOfBounds(newPoint) || _lines[(int)newPoint.Y][(int)newPoint.X] != Word[charIndex])
                    {
                        return false;
                    }
                }
                return true;
            case "rup":
                for (int charIndex = 0; charIndex < Word.Length; charIndex++)
                {
                    var newPoint = point + new Vector2(1*charIndex, -1*charIndex);
                    if (IsOutOfBounds(newPoint) || _lines[(int)newPoint.Y][(int)newPoint.X] != Word[charIndex])
                    {
                        return false;
                    }
                }
                return true;
            default:
                return false;
        }
    }

    private bool IsOutOfBounds(Vector2 point)
    {
        return point.X < 0 || point.Y < 0 || point.X > _width-1 || point.Y > _height-1;
    }
}