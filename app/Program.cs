using System.Net.Sockets;

using Nefarius.Utilities.EnglishExceptions;

using var p = new Patcher();

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
