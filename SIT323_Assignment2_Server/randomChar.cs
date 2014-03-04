using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    class randomChar
    {
        public string charGen(int rand)
        {
            char ch = ' ';

            
            // convert to a letter based on the random number generated.
            if (rand == 1)
                ch = 'A';
            if (rand == 2)
                ch = 'B';
            if (rand == 3)
                ch = 'C';
            if (rand == 4)
                ch = 'D';
            if (rand == 5)
                ch = 'E';
            if (rand == 6)
                ch = 'F';
            if (rand == 7)
                ch = 'G';
            if (rand == 8)
                ch = 'H';
            if (rand == 9)
                ch = 'I';
            if (rand == 10)
                ch = 'J';
            if (rand == 11)
                ch = 'K';
            if (rand == 12)
                ch = 'L';
            if (rand == 13)
                ch = 'M';
            if (rand == 14)
                ch = 'N';
            if (rand == 15)
                ch = 'O';
            if (rand == 16)
                ch = 'P';
            if (rand == 17)
                ch = 'Q';
            if (rand == 18)
                ch = 'R';
            if (rand == 19)
                ch = 'S';
            if (rand == 20)
                ch = 'T';
            if (rand == 21)
                ch = 'U';
            if (rand == 22)
                ch = 'V';
            if (rand == 23)
                ch = 'W';
            if (rand == 24)
                ch = 'X';
            if (rand == 25)
                ch = 'Y';
            if (rand == 26)
                ch = 'Z';

            return ch.ToString();
        }
    }
}
