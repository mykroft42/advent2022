using System.Linq.Expressions;

internal class Program
{
    private static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        List<Monkey> monkeys = new List<Monkey>();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i++];

            var items = line.Split(": ")[1].Split(", ").Select(l => ulong.Parse(l)).ToList();

            line = lines[i++];

            Expression param1 = Expression.Parameter(typeof(ulong), "x");
            var operations = line.Split(" = ")[1].Split(" ").ToList();
            Expression param2 = param1;
            if (operations[2] != "old")
            {
                param2 = Expression.Constant(ulong.Parse(operations[2]), typeof(ulong));
            }

            Expression res;
            if (operations[1] == "+")
            {
                res = Expression.Add(param1, param2);
            }
            else if (operations[1] == "*")
            {
                res = Expression.Multiply(param1, param2);
            }
            else
                throw new Exception("bad operator");

            line = lines[i++];
            ulong test = ulong.Parse(line.Split(" ").Last());
            line = lines[i++];
            int doT = int.Parse(line.Split(" ").Last());
            line = lines[i++];
            int doF = int.Parse(line.Split(" ").Last());

            monkeys.Add(new Monkey
            {
                Items = items,
                Process = (Func<ulong, ulong>)Expression.Lambda(res, (ParameterExpression)param1).Compile(),
                Test = test,
                IndexTrue = doT,
                IndexFalse = doF,
            });
            i++;
        }

        ulong common = monkeys.Select(m => m.Test).Aggregate((ulong)1, (a, m) => a * m);
        for (ulong i = 0; i < 10000; i++)
        {
            foreach (var monkey in monkeys)
            {
                foreach(var item in monkey.Items)
                {
                    ulong num = monkey.Process(item);
                    num = num % common;
                    //num = num / 3;

                    if (num % monkey.Test == 0)
                    {
                        monkeys[monkey.IndexTrue].Items.Add(num);
                    }
                    else
                    {
                        monkeys[monkey.IndexFalse].Items.Add(num);
                    }

                    monkey.inspect++;
                }

                monkey.Items = new List<ulong>();

            }

            //Console.WriteLine("------------------------------");
            //Console.WriteLine(i.ToString());
            //monkeys.ForEach(m => Console.WriteLine(m.inspect));
        }

        var temp = monkeys.OrderByDescending(m => m.inspect).Take(2).ToList();

        Console.Write(temp[0].inspect * temp[1].inspect);
    }
}