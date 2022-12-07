using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace day07
{
    class Dir
    {
        public string Name{get;set;}
        public List<Dir> Children{get;set;} = new List<Dir>();
        public List<(int size, string name)> Files{get;set;} = new List<(int size, string name)>();

        public Dir Parent{get;set;} = null;

        public int Size()
        {
            return Files.Select(f => f.size).Sum() + Children.Select(c => c.Size()).Sum();
        }

        public override string ToString()
        {
            return $"{{Name: {Name}}}";
        }
    }
    class Program
    {
        const int MAX_SIZE = 70000000;
        const int REQ_SIZE = 30000000;
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var root = new Dir();
            var current = root;

            foreach(var line in lines.Skip(1))
            {
                var parts = line.Split(" ");
                if (parts[0] == "$" && parts[1] == "ls")
                {
                    continue;
                }
                else if (parts[0] == "$" && parts[1] == "cd")
                {
                    if (parts.Last() == "..") current = current.Parent;
                    else current = current.Children.Single(c => c.Name == parts.Last());
                }
                else
                {
                    int size;
                    if (int.TryParse(parts[0], out size))
                    {
                        current.Files.Add((size, parts[1]));
                    }
                    else
                    {
                        current.Children.Add(new Dir { Name = parts[1], Parent = current });
                    }
                }
            }

            int avail = MAX_SIZE - root.Size();
            int needed = REQ_SIZE - avail;

            //Console.WriteLine(SumOfSums(root));
            Console.WriteLine(Flatten(root).Where(r => r.Size() > needed).OrderBy(r => r.Size()).First().Size());
        }

        static List<Dir> Flatten(Dir root)
        {
            return root.Children.Union(root.Children.SelectMany(c => Flatten(c))).OrderBy(c => c.Size()).ToList();
        }

        static int SumOfSums(Dir root)
        {
            return root.Children.Where(c => c.Size() <= 100000).Select(c => c.Size()).Sum() + root.Children.Select(c => SumOfSums(c)).Sum();
        }
    }
}
