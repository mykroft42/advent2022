
var lines = File.ReadAllLines("input.txt");

List<(int x, int y)> rocks = new List<(int x, int y)>();
foreach (var line in lines)
{
    var pairs = line.Split(" -> ").Select(l =>
    {
        var pair = l.Split(",");
        (int x, int y) point = (int.Parse(pair[0]), int.Parse(pair[1]));
        return point;
    }).ToList();

    for (int i = 0; i < pairs.Count - 1; i++)
    {
        var start = pairs[i];
        var end = pairs[i + 1];

        if (start.x == end.x)
        {
            int sy, ey;
            if (start.y < end.y)
            {
                sy = start.y;
                ey = end.y;
            }
            else
            {
                sy = end.y;
                ey = start.y;
            }

            for (int j = sy; j <= ey; j++)
            {
                if (!rocks.Contains((start.x, j)))
                    rocks.Add((start.x, j));
            }
        }
        else
        {
            int sx, ex;
            if (start.x < end.x)
            {
                sx = start.x;
                ex = end.x;
            }
            else
            {
                sx = end.x;
                ex = start.x;
            }

            for (int j = sx; j <= ex; j++)
            {
                if (!rocks.Contains((j, start.y)))
                    rocks.Add((j, start.y));
            }
        }
    }
}

int floorY = rocks.Select(r => r.y).Max() + 1;
int result = 0;
bool test = false;
List<(int x, int y)> sand = new List<(int x, int y)>();
while (true)
{
    result++;
    var point = findPoint(500, -1);
    if (point.y == 0) break;
    sand.Add(point);
    if (test && result > 24)
    {
        Console.WriteLine($"=== {result} ===");
        Console.WriteLine();
        printPoints();
        Console.WriteLine();
        //test = false;
        Console.ReadKey();
    }
}

Console.WriteLine(result - 1);

(int x, int y) findPoint(int x, int y)
{
    int xres = x;
    int? minY = (from r in rocks.Union(sand)
                 where r.x == x && r.y > y
                 select (int?)(r.y - 1)).Min();

    if (minY.HasValue && minY > y && minY != floorY)
    {
        var check = findPoint(x - 1, minY.Value);
        if (check.y == minY)
        {
            check = findPoint(x + 1, minY.Value);
        }

        if (check.y > minY)
        {
            xres = check.x;
            minY = check.y;
        }
    }

    return (xres, minY ?? floorY);    
}

void printPoints()
{
    for (int i = 0; i <= floorY; i++)
    {
        for (int j = rocks.Union(sand).Min(r => r.x) - 1; j <= rocks.Union(sand).Max(r => r.x) + 1; j++)
        {
            string c = ".";
            if (rocks.Contains((j, i))) c = "#";
            else if (sand.Contains((j, i))) c = "o";
            Console.Write(c);
        }
        Console.WriteLine();
    }
}