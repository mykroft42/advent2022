using day12;
using Graphalo;
using Graphalo.Traversal;

internal class Program
{
    private static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");

        List<Node> nodes = new List<Node>();

        int x = 0, y = 0;
        foreach (var line in lines)
        {
            foreach(var node in line)
            {
                Node item = new Node { X = x, Y = y, height = node };
                nodes.Add(item);
                x++;
            }
            y++;
            x = 0;
        }

        nodes.ForEach(item => item.Neighbors = nodes.Where(n =>
                    ((Math.Abs(n.X - item.X) == 1 && n.Y == item.Y) ||
                     (Math.Abs(n.Y - item.Y) == 1 && n.X == item.X)) &&
                    ((item.height >= n.height) || (n.height - item.height == 1))).ToList());

        DirectedGraph<Node> graph = new DirectedGraph<Node>();
        graph.AddEdges(nodes.SelectMany(n => n.Neighbors.Select(nn => new Edge<Node>(n, nn))));

        //var result = graph.Traverse(Graphalo.Traversal.TraversalKind.Dijkstra, nodes.Single(n => n.RealHeight == 'S'), nodes.Single(n => n.RealHeight == 'E'));
        Node end = nodes.Single(n => n.RealHeight == 'E');
        int steps = int.MaxValue;
        foreach (var item in nodes.Where(n => n.height == 'a'))
        {
            var result = graph.Traverse(TraversalKind.Dijkstra, item, end);
            if (result.Success && (result.Results.Count() - 1) < steps) steps = result.Results.Count() - 1;
        }

        Console.WriteLine(steps);
    }
}