using CapitalGains.Handlers;


while (true)
{
    Console.WriteLine();
    Console.WriteLine("Press 'Q' to quit.");
    Console.WriteLine("Enter the operations list:");
    Console.WriteLine();

    var input = Console.ReadLine();

    if(input?.ToUpper() == "Q")
    {
        Console.WriteLine("Exiting ...");
        break;
    }

    Console.Clear();

    var result = OpertionsProcessor.ProcessOperations(input);

    Console.WriteLine(result);

}

