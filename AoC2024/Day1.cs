using System.Text.RegularExpressions;

var lines = File.ReadLines("Day1.txt");
var leftList = new List<int>();
var rightList = new List<int>();
var sum = 0;
foreach (var line in lines)
{
    Regex rg = new Regex(@"(\d+)\s+(\d+)$");
    var matches = rg.Matches(line);
    foreach (Match match in matches)
    {
        leftList.Add(int.Parse(match.Groups[1].Value));
        rightList.Add(int.Parse(match.Groups[2].Value));
    }
}

leftList.Sort();
rightList.Sort();
for (int i = 0; i < leftList.Count; i++)
{
    sum += Math.Abs(leftList[i] - rightList[i]);
}

Console.WriteLine($"Task1: {sum}");

// Part 2
var similarityScore = 0;
var group = rightList.GroupBy(i => i);
for (int i = 0; i < leftList.Count; i++)
{
    var occurences = group.FirstOrDefault(x => x.Key == leftList[i])?.Count();
    if (occurences.HasValue)
    {
        similarityScore += (leftList[i] * occurences.Value);
    }
}

Console.WriteLine($"Task2: {similarityScore}");