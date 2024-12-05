using System.Text.RegularExpressions;

namespace AoC2024;

public class Day3
{
    public Day3()
    {
        var lines = File.ReadLines("Day3.txt");
        var regex = new Regex(@"mul\((\d+),(\d+)\)");
        var sum1 = lines.SelectMany(line => regex.Matches(line)).Sum(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
        Console.WriteLine($"[Day3] Task1: {sum1}");

        var sum2 = 0;
        var enabled = true;
        var regex2 = new Regex(@"(do\(\)|don't\(\)|mul\((\d+),(\d+)\))");
        foreach (var line in lines)
        {
            var matches = regex2.Matches(line);
            foreach (Match match in matches)
            {
                enabled = match.Groups[1].Value switch
                {
                    "do()" => true,
                    "don't()" => false,
                    _ => enabled
                };

                if (enabled && match.Groups[1].Value.StartsWith("mul"))
                {
                    sum2 += int.Parse(match.Groups[2].Value) * int.Parse(match.Groups[3].Value);
                }
            }
        }
        
        Console.WriteLine($"[Day3] Task2: {sum2}");
    }
}