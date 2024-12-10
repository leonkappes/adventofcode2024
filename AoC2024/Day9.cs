namespace AoC2024;

public class Day9
{
    private static readonly string Line = File.ReadLines("Day9.txt").ToArray()[0];
    private const int Emptyid = -1;
    public Day9()
    {
        List<long> diskMap = [];
        var blockId = 0;
        foreach (var it in Line.Select((x, i) => new { Value = x, Index = i }))
        {
            if (it.Index % 2 == 0)
            {
                for (int i = 0; i < int.Parse(it.Value.ToString()); i++)
                {
                    diskMap.Add(blockId);
                }

                blockId++;
            }
            else
            {
                for (int i = 0; i < int.Parse(it.Value.ToString()); i++)
                {
                    diskMap.Add(Emptyid);
                }
            }
        }

        var placePointer = 0;
        var seekPointer = diskMap.Count-1;
        while (placePointer < seekPointer)
        {
            while (diskMap[placePointer] != Emptyid && placePointer < seekPointer)
            {
                placePointer++;
            }
            while (diskMap[seekPointer] == Emptyid && seekPointer > placePointer)
            {
                seekPointer--;
            }
            if (placePointer == seekPointer)
            {
                break;
            }

            var moveValue = diskMap[seekPointer];
            diskMap.RemoveAt(seekPointer);
            seekPointer--;
            diskMap[placePointer] = moveValue;
        }
        
        long checkSum = diskMap.Where(((value, index) => value != Emptyid)).Select(((value, index) => value*index)).Sum();
        Console.WriteLine($"[Day9] Task1: {checkSum}");
    }
}