// See https://aka.ms/new-console-template for more information
var lines = File.ReadAllLines("input.txt").ToList();
List<List<int>> cals = new List<List<int>>();
var current = new List<int>();
cals.Add(current);
foreach (var line in lines)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        current = new List<int>();
        cals.Add(current);
        continue;
    }

    current.Add(int.Parse(line));
}

var answer = cals.Select(c => c.Sum()).OrderByDescending(c => c).Take(3).Sum();
Console.WriteLine(answer);