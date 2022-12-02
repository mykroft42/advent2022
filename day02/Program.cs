// See https://aka.ms/new-console-template for more information
var lines = File.ReadAllLines("input.txt");

int score = 0;
foreach(var line in lines)
{
    Console.WriteLine(line);
    char them = line[0];
    char me = line[2];

    if (them == 'A')
    {
        if (me == 'X') me = 'C';
        else if (me == 'Y') me = them;
        else if (me == 'Z') me = 'B';
    } 
    else if (them == 'B')
    {
        if (me == 'X') me = 'A';
        else if (me == 'Y') me = them;
        else if (me == 'Z') me = 'C';
    }
    else if (them == 'C')
    {
        if (me == 'X') me = 'B';
        else if (me == 'Y') me = them;
        else if (me == 'Z') me = 'A';
    }

    Console.WriteLine($"them: {them} me: {me}");
    if (me == 'A') score += 1;
    else if (me == 'B') score += 2;
    else if (me == 'C') score += 3;

    if (me == them) score += 3;
    else if (me == 'A' && them == 'C') score += 6;
    else if (me == 'B' && them == 'A') score += 6;
    else if (me == 'C' && them == 'B') score += 6;
}

Console.WriteLine(score);