namespace AoC2024;

public class Day5
{
    private static readonly string[] Lines = File.ReadLines("Day5.txt").ToArray();
    public Day5()
    {
        var (rules, updates) = ParseInput();
        var task1Sum = 0;
        var task2Sum = 0;
        
        foreach (var update in updates)
        {
            var (isOrdered, result) = IsValidOrder(update, rules);
            if (isOrdered)
            {
                task1Sum += result[result.Length / 2];
            }
            else
            {
                task2Sum += result[result.Length / 2];
            }
        }
        
        Console.WriteLine($"[Day5] Task1: {task1Sum}");
        Console.WriteLine($"[Day5] Task2: {task2Sum}");
    }

    private static int[] FixOrder(int[] update, Dictionary<int, List<int>> rules)
    {
        var swapped = false;
        do
        {
            swapped = false;
            for (var i = 0; i < update.Length; i++)
            {
                var page = update[i];
                if(!rules.TryGetValue(page, out var ruleValues)) continue;

                foreach (var ruleValue in ruleValues)
                {
                    var valueIndex = Array.IndexOf(update, ruleValue);
                    if (valueIndex < i) continue;
                    (update[i], update[valueIndex]) = (update[valueIndex], update[i]);
                    swapped = true;
                }
            }
        } while (swapped);

        return update;
    }

    private static (bool, int[] update) IsValidOrder(int[] update, Dictionary<int, List<int>> rules)
    {
        var positions = update.Select((value, index) => new { value, index }).ToDictionary(x => x.value, x => x.index);
        foreach (var (key, value) in rules)
        {
            if (!positions.TryGetValue(key, out var keyIndex)) continue;
            foreach (var v in value)
            {
                if (positions.TryGetValue(v, out var vIndex) && vIndex < keyIndex)
                {
                    return (false, FixOrder(update, rules));
                }
            }
        }

        return (true, update);
    }

    private static (Dictionary<int, List<int>> rules, List<int[]> updates) ParseInput()
    {
        var rules = new Dictionary<int, List<int>>();
        var updates = new List<int[]>();
        foreach (var line in Lines)
        {
            if (line.Contains('|'))
            {
                var nums = line.Split('|');
                var num1 = int.Parse(nums[0]);
                var num2 = int.Parse(nums[1]);
                if (!rules.TryGetValue(num1, out var value))
                {
                    rules[num1] = [num2];
                }
                
                value?.Add(num2);
            }else if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            else
            {
                var nums = line.Split(',');
                updates.Add(nums.Select(int.Parse).ToArray());
            }
        }
        
        return (rules, updates);
    }
}