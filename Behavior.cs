using System.Diagnostics;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebSocketServer;
public class Behavior : WebSocketBehavior
{
    public string Path { get; set; } = "";    
    
    protected override void OnMessage(MessageEventArgs e)
    {
        Debug.WriteLine($"Received Message from {Path}-{ID.Remove(0,26)}: [{e.Data}]");
        switch (e.Data)
        {
            case "\"Full\"":
                Debug.WriteLine($"Server Sending Full State to {Path}-{ID.Remove(0,26)}");
                Send("\"Server sent Full State\"");
                break;
            case "\"Partial\"":
                Debug.WriteLine($"Server Sending Partial State to {Path}-{ID.Remove(0,26)}");
                Send("\"Server sent Partial State\"");
                break;
            default:
                Debug.WriteLine("Unknown Message Received. Ignoring.");
                break;
        }
    }
    
    protected override void OnOpen()
    {
        Debug.WriteLine($"Server Opened Websocket {Path} with Id {ID.Remove(0,26)}");
    }
    
    protected override void OnClose(CloseEventArgs e)
    {
        Debug.WriteLine($"Server Closed Websocket {Path}. Id {ID.Remove(0,26)}. Code {e.Code}. Reason {e.Reason} Was Clean: {e.WasClean}");
    }
    
    protected override void OnError(WebSocketSharp.ErrorEventArgs e)
    {
        Debug.WriteLine($"Error {Path}-{ID.Remove(0,26)}: {e.Message}");
    }
}