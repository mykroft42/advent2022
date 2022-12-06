// See https://aka.ms/new-console-template for more information
var line = File.ReadAllLines("input.txt").First();
// line = "bvwbjplbgvbhsrlpgdmjqwftvncz";
List<char> buffer = new List<char>();
int count = 0;
foreach (var l in line)
{
    if (buffer.Count == 14)
    {
        if ((from b in buffer
        group b by b into bs
        select bs.Count()).All(i => i == 1))
            break;
        buffer.RemoveAt(0);
    }

    count++;
    buffer.Add(l);
}

Console.WriteLine(count);