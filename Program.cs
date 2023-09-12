using System;
using consoleInfo;

internal class Program : ConsoleInfo
{
    private static void Main()
    {
        _ = new Program();
    }
    Program()
    {
        Start();
    }
    public void Start()
    {
        StartMeun();
        Add("Test 2", TEst2);
        Add("Test 3", TEst3);
        Add("Test 1", TEst1);
        Add("Test 4", TEst4);

        AddSub("Sub Test 2", TEst2, 0);
        AddSub("Sub Test 3", TEst3, 0);
        AddSub("Sub Test 4", TEst4, 0);

        do
        {
            Update();
        } while (true);
    }
    public bool TEst1()
    {
        GetInSub(0);
        return false;
    }
    public bool TEst2()
    {
        Console.WriteLine("Test2");
        return false;
    }
    public bool TEst3()
    {
        Console.WriteLine("Test3");
        return false;
    }
    public bool TEst4()
    {
        Console.WriteLine("Test4");
        return false;
    }
}
