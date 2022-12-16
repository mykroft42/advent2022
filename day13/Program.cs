using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace day13
{
    class Item : IComparable<Item>
    {
        public int? Value{get;set;}
        public List<Item> Items { get; set; } = new List<Item>();
        public Item Parent { get; set; }
        public string StringRep { get; set; }

        public int CompareTo(Item other)
        {
            bool? result = Program.Compare(this, other);
            if (result == true) return -1;
            return 1;
        }
    }
    public enum State
    {
        Open,
        Close,
        Number,
        Comma,
    }
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            int j = 0;
            List<int> positives = new List<int>();
            List<Item> items = new List<Item>()
            {
                ParseItem("[[2]]"),
                ParseItem("[[6]]"),
            };
            for(int i = 0; i < lines.Length; i++)
            {
                j++;
                Item item1 = ParseItem(lines[i++]);
                Item item2 = ParseItem(lines[i++]);
                items.Add(item1);
                items.Add(item2);

                var result = Compare(item1, item2);
                if (result == true) positives.Add(j);
            }
            positives.ForEach(Console.WriteLine);
            Console.WriteLine(positives.Sum());

            items = items.OrderBy(i => i).ToList();
            Console.WriteLine((items.IndexOf(items.Single(i => i.StringRep == "[[2]]")) + 1) * (items.IndexOf(items.Single(i => i.StringRep == "[[6]]")) + 1));
        }

        static Item ParseItem(string input)
        {
            Item current = new Item() { StringRep = input };
            current.Parent = current;

            State state = State.Open;
            string buffer = "";
            foreach(var c in input.Substring(1))
            {
                State newState;
                switch(c)
                {
                    case char v when int.TryParse(v.ToString(), out _):
                        newState = State.Number;
                        break;
                    case '[':
                        newState = State.Open;
                        break;
                    case ',':
                        newState = State.Comma;
                        break;
                    case ']':
                        newState = State.Close;
                        break;
                    default:
                        throw new Exception($"Bad Token: {c}");
                }

                if (newState == State.Number)
                {
                    buffer += c;
                    state = newState;
                    continue;
                }

                if (newState == State.Comma && state == State.Number)
                {
                    current.Items.Add(new Item { Value = int.Parse(buffer), Parent = current });
                    buffer = "";
                    state = newState;
                    continue;
                }

                if (newState == State.Open)
                {
                    Item temp = new Item { Parent = current };
                    current.Items.Add(temp);
                    current = temp;
                    state = newState;
                    continue;
                }

                if (newState == State.Close && state == State.Number)
                {
                    current.Items.Add(new Item { Value = int.Parse(buffer), Parent = current });
                    buffer = "";
                }

                if (newState == State.Close)
                {
                    current = current.Parent;
                    state = newState;
                    continue;
                }
            }

            return current;
        }

        public static bool? Compare(Item left, Item right)
        {
            if (left.Value.HasValue && right.Value.HasValue)
            {
                if (left.Value < right.Value) return true;
                else if (left.Value == right.Value) return null;
                else return false;
            }

            if (left.Value.HasValue) left = new Item { Items = new List<Item> { new Item { Value = left.Value } } };
            else if (right.Value.HasValue) right = new Item { Items = new List<Item> { new Item { Value = right.Value } } };

            int compares = 0;
            for (int i = 0; i < left.Items.Count; i++)
            {
                if (i >= right.Items.Count) return false;

                bool? result = Compare(left.Items[i], right.Items[i]);
                if (result == true) return true;
                else if (result == false) return false;
            }

            if (left.Items.Count == right.Items.Count) return null;

            return true;
        }
    }
}
