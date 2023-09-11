// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using WebSocketServer;
using WebSocketSharp;

Debug.WriteLine("Creating WebSocket Server Behaviors!");
var paths = new List<string>() { "home", "office", "kitchen" };
var server = new WebSocketSharp.Server.WebSocketServer($"ws://0.0.0.0:58000") { ReuseAddress = true };
foreach (var path in paths)
{
    server.AddWebSocketService<Behavior>($"/{path}", b => b.Path = path);
}
var quit = false;
while (!quit)
{
    Console.Clear();
    Console.WriteLine("Websocket State: {0}", server.IsListening ? "Listening" : "Not Listening");
    Console.WriteLine("1. Start WebSocket Server");
    Console.WriteLine("2. Stop WebSocket Server");
    Console.WriteLine("3. Broadcast message to all clients in home");
    Console.WriteLine("4. Broadcast message to all clients in office");
    Console.WriteLine("5. Broadcast message to all clients in kitchen");
    Console.WriteLine("Q. Quit Program");
    var key = Console.ReadKey();
    switch (key.Key)
    {
        case ConsoleKey.D1:
        case ConsoleKey.NumPad1:
            server.Start();
            Console.WriteLine("Server Started!");
            break;
        case ConsoleKey.D2:
        case ConsoleKey.NumPad2:
            server.Stop(CloseStatusCode.Away, "Server Stopped!");
            Console.WriteLine("Server Stopped!");
            break;
        case ConsoleKey.D3:
        case ConsoleKey.NumPad3:
            server.WebSocketServices["/home"].Sessions.Broadcast("\"Home Broadcast Message\"");
            break;
        case ConsoleKey.D4:
        case ConsoleKey.NumPad4:
            server.WebSocketServices["/office"].Sessions.Broadcast("\"Office Broadcast Message\"");
            break;
        case ConsoleKey.D5:
        case ConsoleKey.NumPad6:
            server.WebSocketServices["/kitchen"].Sessions.Broadcast("\"Kitchen1 Broadcast Message\"");
            break;
        case ConsoleKey.Q:
            quit = true;
            break;
    }
}
Console.WriteLine("Quitting Program!");