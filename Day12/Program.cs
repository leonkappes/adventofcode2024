using System.Numerics;

char[][] grid = Array.ConvertAll(File.ReadLines("Day12.txt").ToArray(), row => row.ToCharArray());
Vector2[] directions = [new Vector2(-1, 0), new Vector2(1,0), new Vector2(0,1), new Vector2(0,-1)];

var patches = new List<Patch>();
for (int row = 0; row < grid.Length; row++)
{
    for (int col = 0; col < grid[0].Length; col++)
    {
        var loc = new Vector2(col, row);
        if (!patches.Any(patch => patch.Contains(loc)))
        {
            patches.Add(AssemblePatch(loc));
        }
    }
}

long price = 0;
long price2 = 0;
foreach (var patch in patches)
{
    price += patch.PriceTask1();
    price2 += patch.PriceTask2();
}
Console.WriteLine($"[Day12] Task1: {price}");
Console.WriteLine($"[Day12] Task2: {price2}");

Patch AssemblePatch(Vector2 location)
{
    var visited = new HashSet<Vector2>();
    return new Patch()
    {
        Type = grid[(int)location.Y][(int)location.X],
        Points = FindPoints(visited, location, grid[(int)location.Y][(int)location.X])
    };
}

List<Vector2> FindPoints(HashSet<Vector2> visited, Vector2 start, char type)
{
    if (visited.Contains(start))
    {
        return [];
    }

    visited.Add(start);

    var map = new List<Vector2>() { start };
    foreach (var direction in directions)
    {
        var newLoc = start + direction;
        if (IsInBounds(newLoc) && !visited.Contains(newLoc) && grid[(int)newLoc.Y][(int)newLoc.X] == type)
        {
            map.AddRange(FindPoints(visited,newLoc, type));
        }
    }

    return map;
}

bool IsInBounds(Vector2 point)
{
    return point.X >= 0 && point.X < grid[0].Length && point.Y >= 0 && point.Y < grid.Length;
}

class Patch
{
    public List<Vector2> Points { get; set; }
    public char Type { get; set; }
    private int Area => Points.Count;
    private long _perimeter = 0;
    private long _sides = 0;
    private readonly Vector2[] _directions = [new Vector2(-1, 0), new Vector2(1,0), new Vector2(0,1), new Vector2(0,-1)];
    
    public Patch()
    {
        Points = new List<Vector2>();
    }

    public bool Contains(Vector2 vector2)
    {
        return Points.Contains(vector2);
    }

    public long Perimeter()
    {
        if (_perimeter > 0)
        {
            return _perimeter;
        }
        
        foreach (var point in Points)
        {
            var sides = 4;
            foreach (var direction in _directions)
            {
                if (Points.Contains(point+direction))
                {
                    sides--;
                }
            }
            _perimeter += sides;
        }
        return _perimeter;
    }

    public long Sides()
    {
        if (_sides > 0)
        {
            return _sides;
        }

        foreach (var point in Points)
        {
            var up = point + new Vector2(-1, 0);
            var down = point + new Vector2(1, 0);
            var left = point + new Vector2(0, -1);
            var right = point + new Vector2(0, 1);

            var upLeft = up + new Vector2(0, -1);
            var upRight = up + new Vector2(0, 1);
            var downLeft = down + new Vector2(0, -1);
            var downRight = down + new Vector2(0, 1);

            if (Points.Contains(up) && Points.Contains(left) && !Points.Contains(upLeft)) // inner upleft corner
                _sides++;
            if (!Points.Contains(up) && !Points.Contains(left)) // outer upleft corner
                _sides++;
            if (Points.Contains(up) && Points.Contains(right) && !Points.Contains(upRight)) // inner upright corner
                _sides++;
            if (!Points.Contains(up) && !Points.Contains(right)) // outer upright corner
                _sides++;
            if (Points.Contains(down) && Points.Contains(left) && !Points.Contains(downLeft)) // inner downleft corner
                _sides++;
            if (!Points.Contains(down) && !Points.Contains(left)) // outer downleft corner
                _sides++;
            if (Points.Contains(down) && Points.Contains(right) &&
                !Points.Contains(downRight)) // inner downright corner
                _sides++;
            if (!Points.Contains(down) && !Points.Contains(right)) // outer downright corner
                _sides++;
        }

        return _sides;
    }

    public long PriceTask1()
    {
        return Area * Perimeter();
    }
    
    public long PriceTask2()
    {
        return Area * Sides();
    }
}