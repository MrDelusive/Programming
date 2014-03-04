using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

// All programming done by Luke Parisi - 211389724              //
// The following program creates a client and server program    //
// of the word search generation for Assignment 2 for SIT323.   //
// C# Socket programming learnt from tutorial at:
// http://www.youtube.com/watch?v=cHq2lYLA4XY, Followed tutorial//
// to create firstly a program that could accept multiple       //
// connections. and have the client and server work             //
// asynchronously, then implemented that into the assignment.   //

// With this setup, it is possible to have multiple clients     //
// connecting to the server as well, not that it is needed.     //
// At this stage if multiple clients connect to the server, only//
// the information from the last client is active, so if client1//
// sends words to server, then client2 sends words, only the    //
// words from client2 will be generated.                        //


namespace SIT323_Assignment2_Server
{
    class listener
    {
        // Create a new socket object
        Socket s;

        
        public bool listening
        {
            get;
            private set;
        }

        public int Port
        {
            get;
            private set;
        }

        // Constructor
        public listener(int port)
        {

            Port = port;
            // new socket of tcp
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }

        // on start set up listening for new connections.
        public void Start()
        {
            if (listening)
                return;

            s.Bind(new IPEndPoint(0, Port));
            s.Listen(0);

            s.BeginAccept(callback, null);
            listening = true;
        }


        // on stop, close sockets
        public void Stop()
        {
            if (!listening)
                return;

            s.Close();
            s.Dispose();
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }


        // This keeps the server and client running asynchronously, although the data is only sent once from client to server.
        // This would be useful if server and client needed to keep communicating data to eachother. So if server had to send the results
        // back to client, this would be used.
        void callback(IAsyncResult ar)
        {
            try
            {
                Socket s = this.s.EndAccept(ar);
                if (SocketAccepted != null)
                {
                    SocketAccepted(s);
                }

                this.s.BeginAccept(callback, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public delegate void SocketAcceptedHandler(Socket e);
        public event SocketAcceptedHandler SocketAccepted;

    }
}
