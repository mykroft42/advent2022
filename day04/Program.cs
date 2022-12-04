// See https://aka.ms/new-console-template for more information

var lines = File.ReadAllLines("input.txt");

int overlap = 0;
foreach(var line in lines)
{
    var set = line.Split(",");
    var p1 = set[0].Split("-").Select(s => int.Parse(s)).ToList();
    var p2 = set[1].Split("-").Select(s => int.Parse(s)).ToList();

//     if (p1[0]>=p2[0] && p1[1]<=p2[1]) overlap++;
//     else if (p2[0] >= p1[0] && p2[1]<=p1[1]) overlap++;
    if ((p1[0] >= p2[0] && p1[0] <= p2[1]) ||
        (p1[1] >= p2[0] && p1[1] <= p2[1]) ||
        (p2[0] >= p1[0] && p2[0] <= p1[1]) ||
        (p2[1] >= p1[0] && p2[1] <= p1[1]))
        overlap++;
}

Console.WriteLine(overlap);