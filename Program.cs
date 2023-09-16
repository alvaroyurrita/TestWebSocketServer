// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using WebSocketServer;
using WebSocketSharp;
using Timer = System.Timers.Timer;

Debug.WriteLine("Creating WebSocket Server Behaviors!");
var paths = new List<string>() { "home", "office", "kitchen" };
var server = new WebSocketSharp.Server.WebSocketServer($"ws://0.0.0.0:58000") { ReuseAddress = true };
var log = server.Log;
log.Output = (data, s) => { }; //mutes all websocket error logs.
foreach (var path in paths)
{
    server.AddWebSocketService<Behavior>($"/{path}", b => b.Path = path);
}
server.AddWebSocketService<HeartbeatBehavior>("/Heartbeat");
var quit = false;
while (!quit)
{

    var t = new Timer(500);
    t.Elapsed += (sender, args) => { PrintMenu(server); };
    t.Start();
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
        case ConsoleKey.NumPad5:
            server.WebSocketServices["/kitchen"].Sessions.Broadcast("\"Kitchen1 Broadcast Message\"");
            break;
        case ConsoleKey.Q:
            quit = true;
            break;
    }
}
Console.WriteLine("Quitting Program!");
return;

void PrintMenu(WebSocketSharp.Server.WebSocketServer s){
    Console.Clear();
    Console.WriteLine("Websocket State: {0}", server.IsListening ? "Listening" : "Not Listening");
    Console.WriteLine("1. Start WebSocket Server");
    Console.WriteLine("2. Stop WebSocket Server");
    Console.WriteLine("3. Broadcast message to all clients in home");
    Console.WriteLine("4. Broadcast message to all clients in office");
    Console.WriteLine("5. Broadcast message to all clients in kitchen");
    Console.WriteLine("Q. Quit Program");
     foreach (var host in s.WebSocketServices.Hosts)
     {
         Console.WriteLine($"{host.Path}:");
         foreach (var session in host.Sessions.Sessions)
         {
             Console.WriteLine($"    {session.ID.Remove(0, 26)}-{session.Context.UserEndPoint.Address}");
         }
     }
}