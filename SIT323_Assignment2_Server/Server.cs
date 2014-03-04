using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// for using Sockets.
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
    public partial class Server : Form
    {
        bool clientRecieved = false;
        // create an array to store the solution.
        char[,] solution = new char[20, 20];


        // Create a listener object.
        listener listener;

        char[,] letterArray = new char[200, 20];
        string[] wordsArray;
        

        public Server()
        {
            InitializeComponent();
            listener = new listener(8);
            listener.SocketAccepted += new listener.SocketAcceptedHandler(listener_SocketAccepted);
            Load += new EventHandler(Server_Load);


            // set all values of solution to blank initially.
            for (int x = 0; x < 20; x++)
                for (int y = 0; y < 20; y++)
                    solution[x, y] = ' ';
        }

        // on run, start the listener
        void Server_Load(object sender, EventArgs e)
        {
            listener.Start();
        }

        void listener_SocketAccepted(System.Net.Sockets.Socket e)
        {
            Client client = new Client(e);
            client.Received += new Client.ClientReceivedHandler(client_Received);
            client.Disconnected += new Client.ClientDisconnectedHandler(client_Disconnected);


            // This was part of the first program I made following the tutorial, decided to keep in as it displays whether client is connected,
            // and also shows how I am sending the data from client to server.
            Invoke((MethodInvoker)delegate
            {
                // sending display to lstClient.
                ListViewItem i = new ListViewItem();
                i.Text = client.EndPoint.ToString();
                i.SubItems.Add(client.ID);
                i.SubItems.Add("XX");
                i.SubItems.Add("XX");
                i.Tag = client;

                lstClient.Items.Add(i);
            });
        }


        // when client disconnects, the item is removed from the list.
        // If it's needed, can clear the wordsearch too, however won't need to, this way client can connect, send info and then disconnect.
        
        void client_Disconnected(Client sender)
        {            
            Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < lstClient.Items.Count; i++)
                {
                    Client client = lstClient.Items[i].Tag as Client;

                    if (client.ID == sender.ID)
                    {
                        lstClient.Items.RemoveAt(i);
                        break;
                    }
                }
            });
        }


        // Once a connection is established from the client, use the data to populate the grid.
        void client_Received(Client sender, byte[] data)
        {
            clientRecieved = true;            

            // set the words array = to the data sent, split at each ,.
            wordsArray = Encoding.Default.GetString(data).Split(',');
            

            // This just shows details in listView for now.
            Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < lstClient.Items.Count; i++)
                {
                    Client client = lstClient.Items[i].Tag as Client;

                    if (client.ID == sender.ID)
                    {
                        lstClient.Items[i].SubItems[2].Text = Encoding.Default.GetString(data);
                        lstClient.Items[i].SubItems[3].Text = DateTime.Now.ToString();
                        break;
                    }
                }
                tbData.Text = Encoding.Default.GetString(data);



                for (int i = 0; i < wordsArray.Length; i++)
                {
                    // this shows the entire wordsArray.                  
                    tbDataMulti.Text += wordsArray[i];
                }

            });               
        }

        // The user pressing this button is what the server would do, so anything under the generate_Click would be Server side.       
        private void generate_Click(object sender, EventArgs e)
        {
            int Score = 0;
            

            // Check if the file exists
            if (clientRecieved == false)
            {
                MessageBox.Show("Error: No data has been sent.");
                return;
            }
            else
            {
                //StreamReader myFile = new StreamReader(search_Name.Text + ".csv");
                //string str = myFile.ReadToEnd();
                //string[] wordsArray = str.Split(',');

                // wordsArray[0] is title of wordsearch
                // wordsArray[1] is width
                // wordsArray[2] is height
                // wordsArray[3] is number of words
                // wordsArray[4] is difficulty
                // With the formatting of the example files, the first word will appear on the 6th location in the array or wordsArray[5]

                // Create 2D array able to store a maximum of 200 words, with a maximum of 20 letters for each word.
                char[,] letterArray = new char[200, 20];
                // Title set to whatever is in the file
                title.Text = wordsArray[0];                

                if (wordsArray[4] == "EASY")
                {
                    for (int x = 1; x < 21; x++)
                    {
                        // The loop for height. Up to max size of 20.
                        for (int y = 1; y < 21; y++)
                        {
                            // This line finds the name of the textbox. Information retrieved from :
                            // http://stackoverflow.com/questions/5143633/use-variable-as-part-of-textbox-name-in-c-sharp
                            // and fixed up to work with this program.
                            TextBox textbox = (TextBox)Controls.Find(string.Format("x{0}y{1}", x, y), false).FirstOrDefault();
                            // This sets the text for whatever textbox is assigned to in this loop, ie "x1y1" or "x1y2" as blank.
                            textbox.Text = "";
                            textbox.Font = new Font(textbox.Font, FontStyle.Regular);

                            // fill all values in solution array to a space.
                            solution[x - 1, y - 1] = ' ';
                            // set the entered_Words display to blank. Used to show what words were fit into the grid. Resets each time.
                            entered_Words.Text = "";
                        }
                    }


                    // loop through each word
                    for (int x = 0; x < Convert.ToInt32(wordsArray[3]) + 5; x++)
                    {
                        char[] currentLetter = wordsArray[x].ToCharArray();
                        // loop through each letter
                        for (int y = 0; y < wordsArray[x].ToCharArray().Length; y++)
                            letterArray[x, y] = currentLetter[y];
                    }
                    // So now each letter is stored in the 2D array, ready to be displayed on screen.

                    // create a random var
                    Random random = new Random((int)DateTime.Now.Ticks);
                    // create randomChar object.
                    // class to convert a random number to a random letter. This is used as filler for when there is no words filling the text box.
                    randomChar randomText = new randomChar();

                    Random rndmxPos = (new Random());
                    Random rndmyPos = (new Random(4));



                    // iterate through loop equal to the number of words there are in the file.    
                    for (int word = 0; word < Convert.ToInt16(wordsArray[3]); word++)
                    {

                        // create a random var
                        // with min at 1, max at the defined width.
                        int randomX = rndmxPos.Next(1, Convert.ToInt16(wordsArray[1]));
                        // with min at 1, max at defined height.
                        int randomY = rndmyPos.Next(1, Convert.ToInt16(wordsArray[2]));

                        // pick a textbox corresponding to the x and y values generated randomly.
                        TextBox textbox = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX, randomY), false).FirstOrDefault();







                        // place the first letter. As long as position is clear. Also need to check if the word goes over the edge of the 
                        // grid, by checking the y starting position (for the words as columns) and making sure y_start + word length < the height.
                        if (textbox.Text == "" && randomY + wordsArray[word + 5].Length < Convert.ToInt16(wordsArray[2]))
                        {
                            bool dontPlaceCol = false;
                            // A loop to check if the positions in the array have chars already in them.
                            for (int y = 0; y < wordsArray[word + 5].Length; y++)
                            {
                                TextBox testPositionCol;
                                testPositionCol = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX, randomY + y), false).FirstOrDefault();
                                // for any positions generated in the loop, if the position is not blank, set dontPlace to true.
                                if (testPositionCol.Text != "")
                                    dontPlaceCol = true;
                            }

                            // the bool checks were placed inside the existing if statement, because the existing if statement checks if the randomY + wordsArray.Length goes over
                            // the edge. Preventing the array from going outside of bounds.
                            if (dontPlaceCol == false)
                            {
                                // This will be the starting position of the word.
                                // Will need to then place each letter next to the starting position in order, and check if it goes over the edge.
                                textbox.Text = letterArray[word + 5, 0].ToString();
                                // also store in solution array in the same location.
                                solution[randomX - 1, randomY - 1] = letterArray[word + 5, 0];

                                TextBox wordPlacement;
                                for (int letter = 1; letter < wordsArray[word + 5].Length; letter++)
                                {
                                    // continue to place the letters of the same word into continuous boxes. This loop creates the word as a column
                                    wordPlacement = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX, randomY + letter), false).FirstOrDefault();
                                    wordPlacement.Text = letterArray[word + 5, letter].ToString();
                                    // also store in solution array in the same location.
                                    solution[randomX - 1, randomY + letter - 1] = letterArray[word + 5, letter];
                                }
                                // if this statement was true, then the word was placed, and score increments by 10.
                                Score += 10;
                                entered_Words.Text += wordsArray[word + 5] + "\r\n";
                            }
                        }
                        // or try to put the word as a row.
                        else if (textbox.Text == "" && randomX + wordsArray[word + 5].Length < Convert.ToInt16(wordsArray[1]))
                        {
                            bool dontPlaceRow = false;
                            // A repeat loop to check if the positions in the array have chars already in them.
                            for (int x = 0; x < wordsArray[word + 5].Length; x++)
                            {
                                // for any positions generated in the loop, if the position is not blank, set dontPlace to true.
                                TextBox testPositionRow;
                                testPositionRow = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX + x, randomY), false).FirstOrDefault();
                                // for any positions generated in the loop, if the position is not blank, set dontPlace to true.
                                if (testPositionRow.Text != "")
                                    dontPlaceRow = true;
                            }

                            if (dontPlaceRow == false)
                            {
                                textbox.Text = letterArray[word + 5, 0].ToString();
                                solution[randomX - 1, randomY - 1] = letterArray[word + 5, 0];

                                TextBox wordPlacement;
                                for (int letter = 1; letter < wordsArray[word + 5].Length; letter++)
                                {
                                    // continue to place the letters of the same word into continuous boxes. This loop creates the word as a row
                                    wordPlacement = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX + letter, randomY), false).FirstOrDefault();
                                    wordPlacement.Text = letterArray[word + 5, letter].ToString();
                                    solution[randomX + letter - 1, randomY - 1] = letterArray[word + 5, letter];
                                }
                                // if this statement was true, then the word was placed, and score increments by 10.
                                Score += 10;
                                entered_Words.Text += wordsArray[word + 5] + "\r\n";
                            }

                        }
                    }

                    // Generate a random number for each box, passing in the random number generated each time.
                    // Was going to do in seperate class, but it didn't recognise the textbox names.

                    // iterate until reaching height of the word search.
                    // This method was easier than writing 400 lines of text for each text box. This method also considers the width and height that was input by user.
                    // The loop for width.            
                    for (int x = 1; x < Convert.ToInt16(wordsArray[1]) + 1; x++)
                    {
                        // The loop for height.
                        for (int y = 1; y < Convert.ToInt16(wordsArray[2]) + 1; y++)
                        {
                            // This line finds the name of the textbox. Information retrieved from :
                            // http://stackoverflow.com/questions/5143633/use-variable-as-part-of-textbox-name-in-c-sharp
                            // and fixed up to work with this program.
                            TextBox textbox = (TextBox)Controls.Find(string.Format("x{0}y{1}", x, y), false).FirstOrDefault();
                            // This sets the text for whatever textbox is assigned to in this loop, ie "x1y1" or "x1y2" as a random letter.
                            // and Check if box is already filled with a char. If it is empty, put a random char in the textBox.
                            if (textbox.Text == "")
                            {
                                textbox.Text = randomText.charGen(random.Next(1, 27));
                            }
                        }
                    }
                    // Display score generated.
                    score.Text = Score.ToString();
                
                // end of easy difficulty.
                }

                else if (wordsArray[4] == "MEDIUM")
                {
                    for (int x = 1; x < 21; x++)
                    {
                        // The loop for height. Up to max size of 20.
                        for (int y = 1; y < 21; y++)
                        {
                            // This line finds the name of the textbox. Information retrieved from :
                            // http://stackoverflow.com/questions/5143633/use-variable-as-part-of-textbox-name-in-c-sharp
                            // and fixed up to work with this program.
                            TextBox textbox = (TextBox)Controls.Find(string.Format("x{0}y{1}", x, y), false).FirstOrDefault();
                            // This sets the text for whatever textbox is assigned to in this loop, ie "x1y1" or "x1y2" as blank.
                            textbox.Text = "";
                            textbox.Font = new Font(textbox.Font, FontStyle.Regular);

                            // fill all values in solution array to a space.
                            solution[x - 1, y - 1] = ' ';
                            // set the entered_Words display to blank. Used to show what words were fit into the grid. Resets each time.
                            entered_Words.Text = "";
                        }
                    }


                    // loop through each word
                    for (int x = 0; x < Convert.ToInt32(wordsArray[3]) + 5; x++)
                    {
                        char[] currentLetter = wordsArray[x].ToCharArray();
                        // loop through each letter
                        for (int y = 0; y < wordsArray[x].ToCharArray().Length; y++)
                            letterArray[x, y] = currentLetter[y];
                    }
                    // So now each letter is stored in the 2D array, ready to be displayed on screen.

                    // create a random var
                    Random random = new Random((int)DateTime.Now.Ticks);
                    // create randomChar object.
                    // class to convert a random number to a random letter. This is used as filler for when there is no words filling the text box.
                    randomChar randomText = new randomChar();

                    Random rndmxPos = (new Random());
                    Random rndmyPos = (new Random(4));



                    // iterate through loop equal to the number of words there are in the file.    
                    for (int word = 0; word < Convert.ToInt16(wordsArray[3]); word++)
                    {

                        // create a random var
                        // with min at 1, max at the defined width.
                        int randomX = rndmxPos.Next(1, Convert.ToInt16(wordsArray[1]));
                        // with min at 1, max at defined height.
                        int randomY = rndmyPos.Next(1, Convert.ToInt16(wordsArray[2]));

                        // pick a textbox corresponding to the x and y values generated randomly.
                        TextBox textbox = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX, randomY), false).FirstOrDefault();


                        // place the first letter. As long as position is clear. Also need to check if the word goes over the edge of the 
                        // grid, by checking the y starting position (for the words as columns) and making sure y_start + word length < the height.


                        // see if can place diagonally first for max points.
                        if (textbox.Text == "" && randomY + wordsArray[word + 5].Length < Convert.ToInt16(wordsArray[2]) && randomX + wordsArray[word + 5].Length < Convert.ToInt16(wordsArray[1]))
                        {
                            bool dontPlace = false;

                            // checking if any textbox already has a char in it.
                            for (int d = 0; d < wordsArray[word + 5].Length; d++)
                            {
                                TextBox testPosition;
                                testPosition = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX + d, randomY + d), false).FirstOrDefault();
                                // for any positions generated in the loop, if the position is not blank, set dontPlace to true.
                                if (testPosition.Text != "")
                                    dontPlace = true;
                            }
                            
                            // if dontplace wasnt flagged then place
                            if (dontPlace == false)
                            {
                                // This will be the starting position of the word.
                                // Will need to then place each letter next to the starting position in order, and check if it goes over the edge.
                                textbox.Text = letterArray[word + 5, 0].ToString();
                                // also store in solution array in the same location.
                                solution[randomX - 1, randomY - 1] = letterArray[word + 5, 0];

                                TextBox wordPlacement;
                                for (int letter = 1; letter < wordsArray[word + 5].Length; letter++)
                                {
                                    // continue to place the letters of the same word into continuous boxes. This loop creates the word as a column
                                    wordPlacement = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX + letter, randomY + letter), false).FirstOrDefault();
                                    wordPlacement.Text = letterArray[word + 5, letter].ToString();
                                    // also store in solution array in the same location.
                                    solution[randomX + letter - 1, randomY + letter - 1] = letterArray[word + 5, letter];
                                }
                                // if this statement was true, then the word was placed, and score increments by 30.
                                Score += 30;
                                entered_Words.Text += wordsArray[word + 5] + "\r\n";
                            }


                        }
                        // else try to place vertical. for 20 points.
                        else if (textbox.Text == "" && randomY + wordsArray[word + 5].Length < Convert.ToInt16(wordsArray[2]))
                        {
                            bool dontPlaceCol = false;
                            // A loop to check if the positions in the array have chars already in them.
                            for (int y = 0; y < wordsArray[word + 5].Length; y++)
                            {
                                TextBox testPositionCol;
                                testPositionCol = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX, randomY + y), false).FirstOrDefault();
                                // for any positions generated in the loop, if the position is not blank, set dontPlace to true.
                                if (testPositionCol.Text != "")
                                    dontPlaceCol = true;
                            }

                            // the bool checks were placed inside the existing if statement, because the existing if statement checks if the randomY + wordsArray.Length goes over
                            // the edge. Preventing the array from going outside of bounds.
                            if (dontPlaceCol == false)
                            {
                                // This will be the starting position of the word.
                                // Will need to then place each letter next to the starting position in order, and check if it goes over the edge.
                                textbox.Text = letterArray[word + 5, 0].ToString();
                                // also store in solution array in the same location.
                                solution[randomX - 1, randomY - 1] = letterArray[word + 5, 0];

                                TextBox wordPlacement;
                                for (int letter = 1; letter < wordsArray[word + 5].Length; letter++)
                                {
                                    // continue to place the letters of the same word into continuous boxes. This loop creates the word as a column
                                    wordPlacement = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX, randomY + letter), false).FirstOrDefault();
                                    wordPlacement.Text = letterArray[word + 5, letter].ToString();
                                    // also store in solution array in the same location.
                                    solution[randomX - 1, randomY + letter - 1] = letterArray[word + 5, letter];
                                }
                                // if this statement was true, then the word was placed, and score increments by 20.
                                Score += 20;
                                entered_Words.Text += wordsArray[word + 5] + "\r\n";
                            }
                        }
                        // or try to put the word as a row.
                        else if (textbox.Text == "" && randomX + wordsArray[word + 5].Length < Convert.ToInt16(wordsArray[1]))
                        {
                            bool dontPlaceRow = false;
                            // A repeat loop to check if the positions in the array have chars already in them.
                            for (int x = 0; x < wordsArray[word + 5].Length; x++)
                            {
                                // for any positions generated in the loop, if the position is not blank, set dontPlace to true.
                                TextBox testPositionRow;
                                testPositionRow = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX + x, randomY), false).FirstOrDefault();
                                // for any positions generated in the loop, if the position is not blank, set dontPlace to true.
                                if (testPositionRow.Text != "")
                                    dontPlaceRow = true;
                            }

                            if (dontPlaceRow == false)
                            {
                                textbox.Text = letterArray[word + 5, 0].ToString();
                                solution[randomX - 1, randomY - 1] = letterArray[word + 5, 0];

                                TextBox wordPlacement;
                                for (int letter = 1; letter < wordsArray[word + 5].Length; letter++)
                                {
                                    // continue to place the letters of the same word into continuous boxes. This loop creates the word as a row
                                    wordPlacement = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX + letter, randomY), false).FirstOrDefault();
                                    wordPlacement.Text = letterArray[word + 5, letter].ToString();
                                    solution[randomX + letter - 1, randomY - 1] = letterArray[word + 5, letter];
                                }
                                // if this statement was true, then the word was placed, and score increments by 10.
                                Score += 10;
                                entered_Words.Text += wordsArray[word + 5] + "\r\n";
                            }

                        }
                    }

                    // Generate a random number for each box, passing in the random number generated each time.
                    // Was going to do in seperate class, but it didn't recognise the textbox names.

                    // iterate until reaching height of the word search.
                    // This method was easier than writing 400 lines of text for each text box. This method also considers the width and height that was input by user.
                    // The loop for width.            
                    for (int x = 1; x < Convert.ToInt16(wordsArray[1]) + 1; x++)
                    {
                        // The loop for height.
                        for (int y = 1; y < Convert.ToInt16(wordsArray[2]) + 1; y++)
                        {
                            // This line finds the name of the textbox. Information retrieved from :
                            // http://stackoverflow.com/questions/5143633/use-variable-as-part-of-textbox-name-in-c-sharp
                            // and fixed up to work with this program.
                            TextBox textbox = (TextBox)Controls.Find(string.Format("x{0}y{1}", x, y), false).FirstOrDefault();
                            // This sets the text for whatever textbox is assigned to in this loop, ie "x1y1" or "x1y2" as a random letter.
                            // and Check if box is already filled with a char. If it is empty, put a random char in the textBox.
                            if (textbox.Text == "")
                            {
                                textbox.Text = randomText.charGen(random.Next(1, 27));
                            }
                        }
                    }
                    // Display score generated.
                    score.Text = Score.ToString();


                // end of medium difficulty.
                }

                else if (wordsArray[4] == "HARD")
                {
                    for (int x = 1; x < 21; x++)
                    {
                        // The loop for height. Up to max size of 20.
                        for (int y = 1; y < 21; y++)
                        {
                            // This line finds the name of the textbox. Information retrieved from :
                            // http://stackoverflow.com/questions/5143633/use-variable-as-part-of-textbox-name-in-c-sharp
                            // and fixed up to work with this program.
                            TextBox textbox = (TextBox)Controls.Find(string.Format("x{0}y{1}", x, y), false).FirstOrDefault();
                            // This sets the text for whatever textbox is assigned to in this loop, ie "x1y1" or "x1y2" as blank.
                            textbox.Text = "";
                            textbox.Font = new Font(textbox.Font, FontStyle.Regular);

                            // fill all values in solution array to a space.
                            solution[x - 1, y - 1] = ' ';
                            // set the entered_Words display to blank. Used to show what words were fit into the grid. Resets each time.
                            entered_Words.Text = "";
                        }
                    }


                    // loop through each word
                    for (int x = 0; x < Convert.ToInt32(wordsArray[3]) + 5; x++)
                    {
                        char[] currentLetter = wordsArray[x].ToCharArray();
                        // loop through each letter
                        for (int y = 0; y < wordsArray[x].ToCharArray().Length; y++)
                            letterArray[x, y] = currentLetter[y];
                    }
                    // So now each letter is stored in the 2D array, ready to be displayed on screen.

                    // create a random var
                    Random random = new Random((int)DateTime.Now.Ticks);
                    // create randomChar object.
                    // class to convert a random number to a random letter. This is used as filler for when there is no words filling the text box.
                    randomChar randomText = new randomChar();

                    Random rndmxPos = (new Random((int)DateTime.Now.Ticks));
                    Random rndmyPos = (new Random((int)DateTime.Now.Ticks * 4));



                    // iterate through loop equal to the number of words there are in the file.  
                    
                    for (int word = 0; word < Convert.ToInt16(wordsArray[3]); word++)
                    {

                        // create a random var
                        // with min at 1, max at the defined width.
                        int randomX = rndmxPos.Next(1, Convert.ToInt16(wordsArray[1]));
                        // with min at 1, max at defined height.
                        int randomY = rndmyPos.Next(1, Convert.ToInt16(wordsArray[2]));

                        // pick a textbox corresponding to the x and y values generated randomly.
                        TextBox textbox = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX, randomY), false).FirstOrDefault();

                        // first check if there are letters in the position. (placing as a row) // CHANGE THIS TO A FOR LOOP.
                        if (textbox.Text != "" && randomX + wordsArray[word + 5].Length < Convert.ToInt16(wordsArray[1]))
                        {                            
                            // go through the positions (checking the placed word as a column first, so that it can place the new word as a row.)
                            for (int placedLetter = 0; placedLetter < wordsArray[word + 5].Length; placedLetter++)
                            {
                                TextBox testPositionCol;
                                testPositionCol = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX, randomY + placedLetter), false).FirstOrDefault();
                                for (int newLetter = 0; newLetter < wordsArray[word + 5].Length; newLetter++)
                                {
                                    // if one of the letters in the word matches any of the words it has checked as a row, place as a column, and intersect the two letters.
                                    if (letterArray[word + 5, newLetter].ToString() == testPositionCol.Text)
                                    {
                                        // if the word is running on the top left, downward, and the new word can intersect like this:
                                           /* H
                                            * E
                                          H E L L O
                                            * L
                                            * O
                                            * 
                                            Where the stars represent the edge, then the word is going to go off the grid and cannot be placed. Will need to check for that as well.
                                            */

                                        // if its the first letter of the new word, then it is easy to place.
                                        if (newLetter == 0)
                                        {
                                            // again check if the positions where the word is going to be placed are free, besides where the 2 words intersect.
                                            bool dontPlaceNewWord = false;
                                            // starting at 1 because the randomX + 0 in this case is going to have a letter in it already.
                                            for (int y = 1; y < wordsArray[word + 5].Length; y++)
                                            {
                                                TextBox testNewWord;
                                                // iterate as a row now, seeings that it is placing against a word that is a column.
                                                testNewWord = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX + y, randomY), false).FirstOrDefault();
                                                // for any positions generated in the loop, if the position is not blank, set dontPlace to true.
                                                if (testNewWord.Text != "")
                                                    dontPlaceNewWord = true;
                                            }
                                            // if its ok, then place
                                            if (dontPlaceNewWord == false)
                                            {
                                                textbox.Text = letterArray[word + 5, 0].ToString();
                                                // places the first letter again and stores int the solution array.
                                                solution[randomX - 1, randomY - 1] = letterArray[word + 5, 0];

                                                TextBox wordPlacement;
                                                for (int letter = 1; letter < wordsArray[word + 5].Length; letter++)
                                                {
                                                    // continue to place the letters of the same word into continuous boxes. This loop creates the word as a column
                                                    wordPlacement = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX + letter, randomY + placedLetter), false).FirstOrDefault();
                                                    wordPlacement.Text = letterArray[word + 5, letter].ToString();
                                                    // also store in solution array in the same location.
                                                    solution[randomX - 1, randomY + letter - 1] = letterArray[word + 5, letter];
                                                }
                                                // if this statement was true, then the word was placed, and score increments by 10.
                                                Score += 10;
                                                entered_Words.Text += wordsArray[word + 5] + "\r\n";
                                            }
                                        }
                                    }
                                }
                            }
                            
                        }

                        // vertical
                        else if (textbox.Text == "" && randomY + wordsArray[word + 5].Length < Convert.ToInt16(wordsArray[2]))
                        {
                            bool dontPlaceCol = false;
                            // A loop to check if the positions in the array have chars already in them.
                            for (int y = 0; y < wordsArray[word + 5].Length; y++)
                            {
                                TextBox testPositionCol;
                                testPositionCol = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX, randomY + y), false).FirstOrDefault();
                                // for any positions generated in the loop, if the position is not blank, set dontPlace to true.
                                if (testPositionCol.Text != "")
                                    dontPlaceCol = true;
                            }

                            // try to place new word on the old word first. For max points.
                            
                            // the bool checks were placed inside the existing if statement, because the existing if statement checks if the randomY + wordsArray.Length goes over
                            // the edge. Preventing the array from going outside of bounds.
                            if (dontPlaceCol == false)
                            {
                                // This will be the starting position of the word.
                                // Will need to then place each letter next to the starting position in order, and check if it goes over the edge.
                                textbox.Text = letterArray[word + 5, 0].ToString();
                                // also store in solution array in the same location.
                                solution[randomX - 1, randomY - 1] = letterArray[word + 5, 0];

                                TextBox wordPlacement;
                                for (int letter = 1; letter < wordsArray[word + 5].Length; letter++)
                                {
                                    // continue to place the letters of the same word into continuous boxes. This loop creates the word as a column
                                    wordPlacement = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX, randomY + letter), false).FirstOrDefault();
                                    wordPlacement.Text = letterArray[word + 5, letter].ToString();
                                    // also store in solution array in the same location.
                                    solution[randomX - 1, randomY + letter - 1] = letterArray[word + 5, letter];
                                }
                                // if this statement was true, then the word was placed, and score increments by 20.
                                Score += 10;
                                entered_Words.Text += wordsArray[word + 5] + "\r\n";
                            }
                        }
                        // horizontal
                        else if (textbox.Text == "" && randomX + wordsArray[word + 5].Length < Convert.ToInt16(wordsArray[1]))
                        {
                            bool dontPlaceRow = false;
                            // A repeat loop to check if the positions in the array have chars already in them.
                            for (int x = 0; x < wordsArray[word + 5].Length; x++)
                            {
                                // for any positions generated in the loop, if the position is not blank, set dontPlace to true.
                                TextBox testPositionRow;
                                testPositionRow = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX + x, randomY), false).FirstOrDefault();
                                // for any positions generated in the loop, if the position is not blank, set dontPlace to true.
                                if (testPositionRow.Text != "")
                                    dontPlaceRow = true;
                            }

                            if (dontPlaceRow == false)
                            {
                                textbox.Text = letterArray[word + 5, 0].ToString();
                                solution[randomX - 1, randomY - 1] = letterArray[word + 5, 0];

                                TextBox wordPlacement;
                                for (int letter = 1; letter < wordsArray[word + 5].Length; letter++)
                                {
                                    // continue to place the letters of the same word into continuous boxes. This loop creates the word as a row
                                    wordPlacement = (TextBox)Controls.Find(string.Format("x{0}y{1}", randomX + letter, randomY), false).FirstOrDefault();
                                    wordPlacement.Text = letterArray[word + 5, letter].ToString();
                                    solution[randomX + letter - 1, randomY - 1] = letterArray[word + 5, letter];
                                }
                                // if this statement was true, then the word was placed, and score increments by 10.
                                Score += 10;
                                entered_Words.Text += wordsArray[word + 5] + "\r\n";
                            }

                        }
                    }

                    // Generate a random number for each box, passing in the random number generated each time.
                    // Was going to do in seperate class, but it didn't recognise the textbox names.

                    // iterate until reaching height of the word search.
                    // This method was easier than writing 400 lines of text for each text box. This method also considers the width and height that was input by user.
                    // The loop for width.            
                    for (int x = 1; x < Convert.ToInt16(wordsArray[1]) + 1; x++)
                    {
                        // The loop for height.
                        for (int y = 1; y < Convert.ToInt16(wordsArray[2]) + 1; y++)
                        {
                            // This line finds the name of the textbox. Information retrieved from :
                            // http://stackoverflow.com/questions/5143633/use-variable-as-part-of-textbox-name-in-c-sharp
                            // and fixed up to work with this program.
                            TextBox textbox = (TextBox)Controls.Find(string.Format("x{0}y{1}", x, y), false).FirstOrDefault();
                            // This sets the text for whatever textbox is assigned to in this loop, ie "x1y1" or "x1y2" as a random letter.
                            // and Check if box is already filled with a char. If it is empty, put a random char in the textBox.
                            if (textbox.Text == "")
                            {
                                textbox.Text = randomText.charGen(random.Next(1, 27));
                            }
                        }
                    }
                    // Display score generated.
                    score.Text = Score.ToString();

                // end of hard difficulty.
                }
       
            // end of else statement
            }
            
        // end of generate click.
        }

        private void solution_Button_Click(object sender, EventArgs e)
        {
            for (int x = 1; x < 21; x++)
            {
                // The loop for height.
                for (int y = 1; y < 21; y++)
                {
                    TextBox textbox = (TextBox)Controls.Find(string.Format("x{0}y{1}", x, y), false).FirstOrDefault();
                    if (solution[x-1,y-1] != ' ')
                    {
                        textbox.Font = new Font(textbox.Font, FontStyle.Bold);
                        textbox.Text = "(" + solution[x-1,y-1].ToString() + ")";
                    }
                }
            }

            //StreamWriter file = new StreamWriter(title.Text.ToLower() + "_grid_solution.csv");
            /*for (int y = 1; y < 21; y++)
            {
                // The loop for height.
                for (int x = 1; x < 21; x++)
                {
                    TextBox textbox = (TextBox)Controls.Find(string.Format("x{0}y{1}", x, y), false).FirstOrDefault();
                    //file.Write(textbox.Text + ",");
                }
                //file.WriteLine();
            }*/
            MessageBox.Show("Solution shown.");
            //file.Close();
        }


        private void x7y19_TextChanged(object sender, EventArgs e)
        {

        }

        private void lstClient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void title_Click(object sender, EventArgs e)
        {

        }

       
    }
}
