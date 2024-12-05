namespace AoC2024;

public class Day2
{
    public Day2()
    {
        var lines = File.ReadLines("Day2.txt");
        
        var safeReports = lines.Select(report => report.Split(" ").Select(int.Parse).ToArray())
            .Count(levels => IsValidReport(levels));
        var safeReports2 = 0;
        Console.WriteLine($"[Day2] Task1: {safeReports}");

        foreach (var report in lines)
        {
            var levels = report.Split(" ").Select(int.Parse).ToArray();
            
            if (IsValidReport(levels))
            {
                safeReports2++;
                    continue;
            }
            
            if (levels.Select((t, i) => levels.Where((_, index) => index != i).ToArray())
                .Any(IsValidReport))
            {

                safeReports2++;
            }
        }
        
        Console.WriteLine($"[Day2] Task2: {safeReports2}");
    }

    bool IsValidReport(int[] levels)
    {
        var pairCollection = levels.Zip(levels.Skip(1), (x, y) => new { x, y });
        var ordered = pairCollection.All(p => p.x < p.y) || pairCollection.All(p => p.x > p.y);
        return ordered && levels.Zip(levels.Skip(1), (x, y) => Math.Abs(x - y)).All(diff => diff is >= 1 and <= 3);
    }
}