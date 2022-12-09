using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace day09
{
    class Program
    {
        static void Main(string[] args)
        {
            (int x, int y) head = (0, 0);
            (int x, int y) tail = (0, 0);

            List<(int x, int y)> tails = new List<(int x, int y)> { (0, 0) };

            var lines = File.ReadAllLines("input.txt");

            foreach(var line in lines)
            {
                var comps = line.Split(" ");

                for (int i = 0; i < int.Parse(comps[1]); i++)
                {
                    switch(comps[0])
                    {
                        case "R":
                            head = (head.x + 1, head.y);
                            break;
                        case "L":
                            head = (head.x - 1, head.y);
                            break;
                        case "U":
                            head = (head.x, head.y + 1);
                            break;
                        case "D":
                            head = (head.x, head.y - 1);
                            break;
                    }

                    if (Math.Abs(head.x - tail.x) <= 1 && Math.Abs(head.y - tail.y) <= 1)
                    {
                        continue;
                    }

                    if (Math.Abs(head.x - tail.x) > 1)
                    {
                        if (head.x - tail.x > 0) tail = (tail.x + 1, tail.y);
                        else tail = (tail.x - 1, tail.y);
                        
                        if (head.y != tail.y)
                        {
                            if (head.y - tail.y > 0) tail = (tail.x, tail.y + 1);
                            else tail = (tail.x, tail.y - 1);
                        }

                        tails.Add(tail);
                    }
                    else
                    {
                        if (head.y - tail.y > 0) tail = (tail.x, tail.y + 1);
                        else tail = (tail.x, tail.y - 1);
                        
                        if (head.x != tail.x)
                        {
                            if (head.x - tail.x > 0) tail = (tail.x + 1, tail.y);
                            else tail = (tail.x - 1, tail.y);
                        }

                        tails.Add(tail);
                    }
                }
            }

            Console.WriteLine(tails.Distinct().Count());
        }
    }
}
