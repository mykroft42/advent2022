public class Monkey
{
    public List<ulong> Items {get;set;}

    public Func<ulong, ulong> Process{get;set;}

    public ulong Test {get;set;}
    public int IndexTrue {get;set;}
    public int IndexFalse {get;set;}

    public ulong inspect = 0;
}