using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.IO;

// Used for sockets.
using System.Net;
using System.Net.Sockets;

// Used for pinging server.
using System.Net.NetworkInformation;

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


// Run Assignment2_Server before running Assignment2_Client, if Assignment2_Server is running, it will allow the connection. If Assignment2_Server isn't running, the connection will be refused.
namespace SIT323_Assignment2_Client
{
    public partial class Client : Form
    {
        // create new socket object.
        Socket sck;
        public Client()
        {
            InitializeComponent();
            // Assign socket information
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        // This is the user saving the form to a .csv file.
        // equivalent of the client side of the program

        private void save_Wordsearch_Click_1(object sender, EventArgs e)
        {
           
            string Height = wordsearch_Height.Text;
            string Width = ws_Width.Text;

            // Convert the height and width to ints and test if they are ints
            int num_Height;
            bool isHeight = int.TryParse(Height, out num_Height);
            int num_Width;
            bool isWidth = int.TryParse(Width, out num_Width);
            int num_IP1;
            bool isGoodIP1 = int.TryParse(tb_IP1.Text, out num_IP1);
            int num_IP2;
            bool isGoodIP2 = int.TryParse(tb_IP2.Text, out num_IP2);
            int num_IP3;
            bool isGoodIP3 = int.TryParse(tb_IP3.Text, out num_IP3);
            int num_IP4;
            bool isGoodIP4 = int.TryParse(tb_IP4.Text, out num_IP4);

            if (!isGoodIP1 || num_IP1 < 0 || num_IP1 > 255)
                MessageBox.Show("Error: First IP block is invalid. Has to be between 0-255.");
            else if (!isGoodIP2 || num_IP2 < 0 || num_IP2 > 255)
                MessageBox.Show("Error: Second IP block is invalid. Has to be between 0-255.");
            else if (!isGoodIP3 || num_IP3 < 0 || num_IP3 > 255)
                MessageBox.Show("Error: Third IP block is invalid. Has to be between 0-255.");
            else if (!isGoodIP4 || num_IP4 < 0 || num_IP4 > 255)
                MessageBox.Show("Error: Fourth IP block is invalid. Has to be between 0-255.");
            else if (wordsearch_Name.Text == "")
                MessageBox.Show("Error: Word Search Name cannot be blank.");
            else if (wordsearch_Difficulty.Text.ToUpper() != "EASY" && wordsearch_Difficulty.Text.ToUpper() != "MEDIUM" && wordsearch_Difficulty.Text.ToUpper() != "HARD")
                MessageBox.Show("Error: Difficulty must be easy, medium or hard."); 
            // if it isn't an int or if int less than 3 or greater than 20..
            else if (!isWidth || num_Width < 3 || num_Width > 20)
                MessageBox.Show("Error: Invalid Width.");
            else if (!isHeight || num_Height < 3 || num_Height > 20)
                MessageBox.Show("Error: Invalid Height.");  
            else if (word_List.Text == "")
                MessageBox.Show("Error: Need at least 1 word to create the word search."); 
               
            else 
            {

                string hostAddress = tb_IP1.Text + "." + tb_IP2.Text + "." + tb_IP3.Text + "." + tb_IP4.Text;
                    
                string[] line = word_List.Text.Split('\n');
                // get the number of words that are in the list.
                int number_Words = line.Length;

                // on click, connect to IP on port.
                sck.Connect(hostAddress, 8);
                // send data to the socket.

                string lineTotal = "";
                for (int i = 0; i < line.Length; i++)
                {
                    line[i] = line[i].Replace("\r", "");
                    line[i] = line[i].Replace("\n", "");
                    line[i] = line[i].Replace("\t", "");
                    lineTotal += (line[i] + ",");

                }

                // was having a problem originally where the server would only remember the data that was sent inside the loop. Now this way sending the data as one chunk,
                // so that there is no data over-write when there is a delay in sending.
                int wsData = sck.Send(Encoding.Default.GetBytes(wordsearch_Name.Text.ToUpper() + "," + ws_Width.Text + "," + wordsearch_Height.Text + "," + number_Words.ToString() + "," + wordsearch_Difficulty.Text.ToUpper() + "," + lineTotal.ToUpper()));               
                
                MessageBox.Show("Data Sent");
  

                // not writing to file now. Instead writing data to sockets.

                /*StreamWriter file = new StreamWriter(wordsearch_Name.Text + ".csv");
                // Add the name, width, height and difficulty to file
                file.Write(wordsearch_Name.Text.ToUpper() + "," + ws_Width.Text + "," + wordsearch_Height.Text);
                // get each word in the list.
                string[] line = word_List.Text.Split('\n');
                // get the number of words that are in the list.
                int number_Words = line.Length;
                file.Write("," + number_Words + "," + wordsearch_Difficulty.Text.ToUpper());
                for (int i = 0; i < line.Length; i++)
                {
                    line[i] = line[i].Replace("\r", "");
                    line[i] = line[i].Replace("\n", "");
                    line[i] = line[i].Replace("\t", "");
                    file.Write("," + line[i].ToUpper());
                }


                MessageBox.Show(wordsearch_Name.Text + ".csv Saved Successfully.");
                file.Close();*/
            }
        }

        private void wordsearch_Difficulty_TextChanged(object sender, EventArgs e)
        {

        }

        private void wordsearch_Width_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
