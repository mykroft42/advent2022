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
            // (int x, int y) head = (0, 0);
            // (int x, int y) tail = (0, 0);
            List<(int x, int y)> knots = new List<(int x, int y)>()
            {
                (0,0),
                (0,0),
                (0,0),
                (0,0),
                (0,0),
                (0,0),
                (0,0),
                (0,0),
                (0,0),
                (0,0),
            };

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
                            knots[0] = (knots[0].x + 1, knots[0].y);
                            break;
                        case "L":
                            knots[0] = (knots[0].x - 1, knots[0].y);
                            break;
                        case "U":
                            knots[0] = (knots[0].x, knots[0].y + 1);
                            break;
                        case "D":
                            knots[0] = (knots[0].x, knots[0].y - 1);
                            break;
                    }

                    for (int j = 0; j < knots.Count - 1; j++)
                    {
                        var tail = knots[j+1];
                        processMove(knots[j], ref tail);
                        knots[j+1] = tail;
                    }                    
                    tails.Add(knots.Last());
                }
            }

            Console.WriteLine(tails.Distinct().Count());
        }

        static void processMove((int x, int y) head, ref (int x, int y) tail)
        {
            if (Math.Abs(head.x - tail.x) <= 1 && Math.Abs(head.y - tail.y) <= 1)
            {
                return;
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
            }
        }
    }
}
