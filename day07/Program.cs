using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

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
    }
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var root = new Dir();
            var current = root;

            foreach(var line in lines.Skip(1))
            {
                if (line.StartsWith("$") && line.Contains("ls"))
                {
                    continue;
                }
                else if (line.StartsWith("$") && line.Contains("cd"))
                {
                    var parts = line.Split(" ");
                    if (parts.Last() == "..") current = current.Parent;
                    else current = current.Children.Single(c => c.Name == parts.Last());
                }
                else
                {
                    var parts = line.Split(" ");
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

            
        }
    }
}
