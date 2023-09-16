using System.Diagnostics;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebSocketServer;
public class HeartbeatBehavior : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        switch (e.Data)
        {
            case "\"Ping\"":
                Send("\"Pong\"");
                break;
            default:
                break;
        }
    }
    
    protected override void OnOpen()
    {
        Debug.WriteLine($"Heartbeat Opened Websocket [{ID.Remove(0,26)}]");
    }
    
    protected override void OnClose(CloseEventArgs e)
    {
        Debug.WriteLine($"Heartbeat Closed Websocket [{ID.Remove(0,26)}]. Code {e.Code}. Reason {e.Reason} Was Clean: {e.WasClean}");
    }
}