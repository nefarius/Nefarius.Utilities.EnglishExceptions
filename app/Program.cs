using System.Net.Sockets;

try
{
    throw new SocketException(10014);
}
catch (Exception ex)
{
    Console.WriteLine($"Example exception: {ex}");
}

Console.WriteLine();
Console.WriteLine("Press any key to close");
Console.ReadKey();
