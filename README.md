#  Websocket Server

The server side is consists of a simple WebSocketSharp server with 4 different paths.  This was used to test how different paths can deliver messages to different clients that subscribe to them

The server can run on Windows, Linux, and Mac as a console program.

* Home
* Office
* Kitchen
* Heartbeat

From the server there are options to:

* Start the Server
* Stop the server
* Broadcast a message to all clients in home
* Broadcast a message to all clients in office
* Broadcast a message to all clients in kitchen