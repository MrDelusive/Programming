using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Server());
        }
    }
}
