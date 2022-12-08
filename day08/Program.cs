using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace day08
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            List<(int x, int y, int height)> trees = new List<(int x, int y, int height)>();

            int y = 0;
            foreach (var line in lines)
            {
                int x = 0;
                foreach (var t in line)
                {
                    trees.Add((x, y, int.Parse(t.ToString())));
                    x++;
                }
                y++;
            }

            int vis = 0;
            foreach (var tree in trees)
            {
                if (trees.Where(t => t.x == tree.x && t.y < tree.y).All(t => t.height < tree.height)) vis++;
                else if (trees.Where(t => t.x == tree.x && t.y > tree.y).All(t => t.height < tree.height)) vis++;
                else if (trees.Where(t => t.y == tree.y && t.x < tree.x).All(t => t.height < tree.height)) vis++;
                else if (trees.Where(t => t.y == tree.y && t.x > tree.x).All(t => t.height < tree.height)) vis++;
            }

            Console.WriteLine(vis);
        }
    }
}
