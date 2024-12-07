namespace AoC2024;

public class Day7
{
    private static readonly string[] Lines = File.ReadLines("Day7.txt").ToArray();
    public Day7()
    {
        long total = 0;
        long total2 = 0;
        foreach (var line in Lines)
        {
            var result = long.Parse(line.Split(':')[0]);
            var equation = Array.ConvertAll(line.Split(':')[1].Trim().Split(), long.Parse);
            if (Recurse(1, equation, equation[0], result, false))
            {
                total += result;
            }
            if (Recurse(1, equation, equation[0], result, true))
            {
                total2 += result;
            }
        }
        
        Console.WriteLine($"[Day7] Task1: {total}");
        Console.WriteLine($"[Day7] Task2: {total2}");
    }

    private static bool Recurse(int index, long[] equationNumbers, long currentValue, long target, bool isPartTwo)
    {
        // base case
        if (index == equationNumbers.Length)
        {
            return currentValue == target;
        }
        
        // add
        if (Recurse(index + 1, equationNumbers, currentValue+equationNumbers[index], target, isPartTwo))
        {
            return true;
        }
        
        // concat
        if (isPartTwo && Recurse(index +1, equationNumbers, long.Parse(string.Concat(currentValue, equationNumbers[index])) ,target, isPartTwo))
        {
            return true;
        }

        // mul
        return Recurse(index + 1, equationNumbers, currentValue*equationNumbers[index], target, isPartTwo);
    }
}