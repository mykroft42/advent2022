using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace day13
{
    class Item 
    {
        public int Value{get;set;}
        public List<Item> Items{get;set;}

        public Item(string input)
        {

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("sample.txt");

            int j = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                foreach (var c in lines[i])
                {

                }
            }
        }
    }
}
