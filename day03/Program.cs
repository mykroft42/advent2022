// See https://aka.ms/new-console-template for more information
var lines = File.ReadAllLines("input.txt");

int Sum = 0;

for(int i = 0; i < lines.Count(); i += 3)
{
    string p1 = lines[i];
    string p2 = lines[i + 1];
    string p3 = lines[i + 2];

    var common = p1.Intersect(p2).Intersect(p3).Single();
    var score = (int)common;
    if (common >= 97) score = common - 96;
    else score = common - 65 + 27;
    Sum += score;
    Console.WriteLine($"{common} {(int)common} {score}");
}
Console.WriteLine(Sum);