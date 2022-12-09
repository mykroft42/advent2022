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
                // if (trees.Where(t => t.x == tree.x && t.y < tree.y).All(t => t.height < tree.height)) vis++;
                // else if (trees.Where(t => t.x == tree.x && t.y > tree.y).All(t => t.height < tree.height)) vis++;
                // else if (trees.Where(t => t.y == tree.y && t.x < tree.x).All(t => t.height < tree.height)) vis++;
                // else if (trees.Where(t => t.y == tree.y && t.x > tree.x).All(t => t.height < tree.height)) vis++;

                int visx1 = 0;
                for (int x = tree.x - 1; x >= 0; x--)
                {
                    visx1++;
                    if (trees.Single(t => t.x == x && t.y == tree.y).height >= tree.height) break;
                }

                int visx2 = 0;
                for (int x = tree.x + 1; x <= trees.Select(t => t.x).Max(); x++)
                {
                    visx2++;
                    if (trees.Single(t => t.x == x && t.y == tree.y).height >= tree.height) break;
                }

                int visy1 = 0;
                for (int ly = tree.y - 1; ly >= 0; ly--)
                {
                    visy1++;
                    if (trees.Single(t => t.y == ly && t.x == tree.x).height >= tree.height) break;
                }

                int visy2 = 0;
                for (int ly = tree.y + 1; ly <= trees.Select(t => t.y).Max(); ly++)
                {
                    visy2++;
                    if (trees.Single(t => t.y == ly && t.x == tree.x).height >= tree.height) break;
                }

                int newVis = visx1 * visx2 * visy1 * visy2;
                if (newVis > vis) vis = newVis;
            }

            Console.WriteLine(vis);
        }
    }
}
