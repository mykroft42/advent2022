int HEADER_SIZE = 8;
// int HEADER_WIDTH = 3;
int HEADER_END = 10;

// See https://aka.ms/new-console-template for more information
var lines = File.ReadAllLines("input.txt");

List<List<string>> columns = new List<List<string>>();

for (int i = 0; i < HEADER_SIZE; i++)
{
    var line = lines[i];
    int k = 0;
    for (int j = 0; j < line.Length; j += 4)
    {
        if (columns.Count == k) columns.Add(new List<string>());
        string res = line.Substring(j)[1].ToString();
        if (!string.IsNullOrWhiteSpace(res))
            columns[k].Add(res);
        k++;
    }
}

for (int i = HEADER_END; i < lines.Count(); i++)
{
    var line = lines[i];
    var items = line.Split(" ");
    int cnt = int.Parse(items[1]);
    int from = int.Parse(items[3]) - 1;
    int to = int.Parse(items[5]) - 1;

    move(cnt, from, to);
}

foreach(var i in columns)
{
    // Console.WriteLine("--");
    Console.Write(i[0]);
}
Console.WriteLine();


void move(int count, int from, int to)
{
    var items = columns[from].Take(count).ToList();
    for (int i = 0; i < count; i++)
        columns[from].RemoveAt(0);
    columns[to].InsertRange(0, items);
}