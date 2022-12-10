// See https://aka.ms/new-console-template for more information
var lines = File.ReadAllLines("input.txt");

int regx = 1;
int cycleCount = 0;
int total = 0;
int check = 40;
List<string> output = new List<string>();
string o = "";
foreach(var line in lines)
{
    int pos = cycleCount % 40;
    if (Math.Abs(regx - pos) <= 1) o += "#";
    else o += ".";
    cycleCount++;


    if (cycleCount == check)
    {
        total += check * regx;
        check += 40;
        output.Add(o);
        o = "";
    }

    if (line != "noop")
    {
        pos = cycleCount % 40;
        if (Math.Abs(regx - pos) <= 1) o += "#";
        else o += ".";
        cycleCount++;
        if (cycleCount == check)
        {
            total += check * regx;
            check += 40;
            output.Add(o);
            o = "";
        }
    
        regx += int.Parse(line.Split(" ").Last());
    }
}

// if (lines.Last() != "noop" && cycleCount == check) total += check * regx;

// Console.WriteLine(total);
output.ForEach(Console.WriteLine);