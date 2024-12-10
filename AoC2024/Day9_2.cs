namespace AoC2024;

public class Day9_2
{
    private static readonly string Line = File.ReadLines("Day9.txt").ToArray()[0];
    private const int Emptyid = -1;

    public Day9_2()
    {
        List<DiskPoint> diskMap = [];
        var blockId = 0;
        foreach (var it in Line.Select((x, i) => new { Value = x, Index = i }))
        {
            if (it.Index % 2 == 0)
            {
                diskMap.Add(new DiskPoint
                {
                    Count = int.Parse(it.Value.ToString()),
                    Id = blockId
                });

                blockId++;
            }
            else
            {
                diskMap.Add(new DiskPoint
                {
                    Count = int.Parse(it.Value.ToString()),
                    Id = Emptyid
                });
            }
        }

        var seekPointer = diskMap.Count - 1;
        while (true)
        {
            while (diskMap[seekPointer].Id == Emptyid && seekPointer > 0)
            {
                seekPointer--;
            }

            if (seekPointer == 0)
            {
                break;
            }

            var spacesNeeded = diskMap[seekPointer].Count;
            var placePointer = 0;
            while ((diskMap[placePointer].Id != Emptyid || diskMap[placePointer].Count < spacesNeeded) &&
                   placePointer < seekPointer)
            {
                placePointer++;
            }

            if (placePointer >= seekPointer)
            {
                seekPointer--;
                continue;
            }

            var blockData = diskMap[seekPointer];
            diskMap[seekPointer] = new DiskPoint
            {
                Count = blockData.Count,
                Id = Emptyid
            };
            seekPointer--;

            diskMap[placePointer].Count -= spacesNeeded;
            if (diskMap[placePointer].Count == 0)
            {
                diskMap[placePointer] = blockData;
            }
            else
            {
                diskMap.Insert(placePointer, blockData);
                seekPointer++;
            }
        }

        long checkSum = 0;
        var posCounter = 0;
        foreach (var diskPoint in diskMap)
        {
            for (int i = 0; i < diskPoint.Count; i++)
            {
                if (diskPoint.Id != Emptyid)
                {
                    checkSum += diskPoint.Id * posCounter;
                }

                posCounter++;
            }
        }

        Console.WriteLine($"[Day9] Task2: {checkSum}");
    }

    private record class DiskPoint
    {
        public required int Count { get; set; }
        public required int Id { get; set; }
    };
}