namespace AoC2024;

public class Day11
{
    private static List<long> _input = File.ReadAllText("Day11.txt").Split(" ").Select(long.Parse).ToList();
    private static Dictionary<long, List<long>> cache = new Dictionary<long, List<long>>();
    
    public Day11()
    {
        var stones = _input.ToDictionary<long, long, long>(l => l, _ => 1);
        for (int i = 0; i < 25; i++)
        {
            var copy = new Dictionary<long, long>();
            foreach (var (stone, occurence) in stones)
            {
                foreach (var newStone in changeStones(stone))
                {
                    if (!copy.TryAdd(newStone, occurence))
                    {
                        copy[newStone] += occurence;
                    }
                }
            }

            stones = copy;
        }

        var totalStones = 0L;
        foreach (var (stone, occurence) in stones)
        {
            totalStones += occurence;
        }
        Console.WriteLine($"[Day11] Task1: {totalStones}");
        
        stones = _input.ToDictionary<long, long, long>(l => l, _ => 1);
        for (int i = 0; i < 75; i++)
        {
            var copy = new Dictionary<long, long>();
            foreach (var (stone, occurence) in stones)
            {
                foreach (var newStone in changeStones(stone))
                {
                    if (!copy.TryAdd(newStone, occurence))
                    {
                        copy[newStone] += occurence;
                    }
                }
            }

            stones = copy;
        }

        var totalStones2 = 0L;
        foreach (var (stone, occurence) in stones)
        {
            totalStones2 += occurence;
        }
        Console.WriteLine($"[Day11] Task2: {totalStones2}");
    }

    private List<long> changeStones(long value)
    {
        if (cache.TryGetValue(value, out var cachedData))
            return cachedData;
        var newData = new List<long>();
        
        if (value == 0)
        {
            newData.Add(1);
        }
        else if (Math.Floor(Math.Log10(value) +1 ) % 2 == 0)
        {
            var digits = value.ToString().Select(digit => long.Parse(digit.ToString())).ToArray();
            var middle = digits.Length / 2;
            var fistHalf = string.Join("", digits.Take(middle).ToArray());
            var secondHalf = string.Join("", digits.Skip(middle).ToArray());
            newData.Add(long.Parse(fistHalf));
            newData.Add(long.Parse(secondHalf));
        }
        else
        {
            newData.Add(value * 2024);
        }

        cache.Add(value, newData);
        return newData;
    }
}