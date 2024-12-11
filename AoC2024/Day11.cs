namespace AoC2024;

public class Day11
{
    private static List<long> _input = File.ReadAllText("Day11.txt").Split(" ").Select(long.Parse).ToList();

    public Day11()
    {
        for (int i = 0; i < 25; i++)
        {
            var copy = new List<long>(_input);
            for (int index = 0; index < copy.Count; index++)
            {
                var value = copy[index];
                if (value == 0)
                {
                    copy[index] = 1;
                }
                else if (Math.Floor(Math.Log10(value) +1 ) % 2 == 0)
                {
                    var digits = value.ToString().Select(digit => long.Parse(digit.ToString())).ToArray();
                    var middle = digits.Length / 2;
                    var fistHalf = string.Join("", digits.Take(middle).ToArray());
                    var secondHalf = string.Join("", digits.Skip(middle).ToArray());
                    copy[index] = long.Parse(fistHalf);
                    copy.Insert(index+1,long.Parse(secondHalf));
                    index++;
                }
                else
                {
                    copy[index] = value * 2024;
                }
            }

            _input = copy;
        }

        var totalStones = _input.Count;
        Console.WriteLine($"[Day11] Task1: {totalStones}");
    }
}