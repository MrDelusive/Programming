namespace SIT323_Assignment2_Client
{
    partial class Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.save_Wordsearch = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.word_List = new System.Windows.Forms.TextBox();
            this.wordsearch_Height = new System.Windows.Forms.TextBox();
            this.ws_Width = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.wordsearch_Name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.wordsearch_Difficulty = new System.Windows.Forms.TextBox();
            this.tb_IP1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_IP2 = new System.Windows.Forms.TextBox();
            this.tb_IP3 = new System.Windows.Forms.TextBox();
            this.tb_IP4 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // save_Wordsearch
            // 
            this.save_Wordsearch.Location = new System.Drawing.Point(197, 669);
            this.save_Wordsearch.Name = "save_Wordsearch";
            this.save_Wordsearch.Size = new System.Drawing.Size(75, 23);
            this.save_Wordsearch.TabIndex = 425;
            this.save_Wordsearch.Text = "Send Data";
            this.save_Wordsearch.UseVisualStyleBackColor = true;
            this.save_Wordsearch.Click += new System.EventHandler(this.save_Wordsearch_Click_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(44, 259);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(172, 13);
            this.label7.TabIndex = 424;
            this.label7.Text = "Words (200 max) and 20 char max:";
            // 
            // word_List
            // 
            this.word_List.Location = new System.Drawing.Point(158, 276);
            this.word_List.Multiline = true;
            this.word_List.Name = "word_List";
            this.word_List.Size = new System.Drawing.Size(143, 386);
            this.word_List.TabIndex = 423;
            // 
            // wordsearch_Height
            // 
            this.wordsearch_Height.Location = new System.Drawing.Point(156, 222);
            this.wordsearch_Height.Name = "wordsearch_Height";
            this.wordsearch_Height.Size = new System.Drawing.Size(121, 20);
            this.wordsearch_Height.TabIndex = 422;
            // 
            // ws_Width
            // 
            this.ws_Width.Location = new System.Drawing.Point(156, 193);
            this.ws_Width.Name = "ws_Width";
            this.ws_Width.Size = new System.Drawing.Size(121, 20);
            this.ws_Width.TabIndex = 421;
            this.ws_Width.TextChanged += new System.EventHandler(this.wordsearch_Width_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 420;
            this.label6.Text = "Grid Height (3-20):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 419;
            this.label5.Text = "Grid Width (3-20):";
            // 
            // wordsearch_Name
            // 
            this.wordsearch_Name.Location = new System.Drawing.Point(156, 128);
            this.wordsearch_Name.Name = "wordsearch_Name";
            this.wordsearch_Name.Size = new System.Drawing.Size(121, 20);
            this.wordsearch_Name.TabIndex = 418;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 417;
            this.label3.Text = "Word Search Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 25);
            this.label2.TabIndex = 416;
            this.label2.Text = "CLIENT SIDE";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 426;
            this.label1.Text = "Difficulty:";
            // 
            // wordsearch_Difficulty
            // 
            this.wordsearch_Difficulty.Location = new System.Drawing.Point(156, 159);
            this.wordsearch_Difficulty.Name = "wordsearch_Difficulty";
            this.wordsearch_Difficulty.Size = new System.Drawing.Size(121, 20);
            this.wordsearch_Difficulty.TabIndex = 427;
            this.wordsearch_Difficulty.TextChanged += new System.EventHandler(this.wordsearch_Difficulty_TextChanged);
            // 
            // tb_IP1
            // 
            this.tb_IP1.Location = new System.Drawing.Point(15, 71);
            this.tb_IP1.Name = "tb_IP1";
            this.tb_IP1.Size = new System.Drawing.Size(36, 20);
            this.tb_IP1.TabIndex = 429;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 428;
            this.label4.Text = "Host IP Address:";
            // 
            // tb_IP2
            // 
            this.tb_IP2.Location = new System.Drawing.Point(73, 71);
            this.tb_IP2.Name = "tb_IP2";
            this.tb_IP2.Size = new System.Drawing.Size(36, 20);
            this.tb_IP2.TabIndex = 430;
            // 
            // tb_IP3
            // 
            this.tb_IP3.Location = new System.Drawing.Point(131, 71);
            this.tb_IP3.Name = "tb_IP3";
            this.tb_IP3.Size = new System.Drawing.Size(36, 20);
            this.tb_IP3.TabIndex = 431;
            // 
            // tb_IP4
            // 
            this.tb_IP4.Location = new System.Drawing.Point(189, 71);
            this.tb_IP4.Name = "tb_IP4";
            this.tb_IP4.Size = new System.Drawing.Size(36, 20);
            this.tb_IP4.TabIndex = 432;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(57, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 433;
            this.label8.Text = ".";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(115, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 13);
            this.label9.TabIndex = 434;
            this.label9.Text = ".";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(173, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(10, 13);
            this.label10.TabIndex = 435;
            this.label10.Text = ".";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 704);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tb_IP4);
            this.Controls.Add(this.tb_IP3);
            this.Controls.Add(this.tb_IP2);
            this.Controls.Add(this.tb_IP1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.wordsearch_Difficulty);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.save_Wordsearch);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.word_List);
            this.Controls.Add(this.wordsearch_Height);
            this.Controls.Add(this.ws_Width);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.wordsearch_Name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "Client";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button save_Wordsearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox word_List;
        private System.Windows.Forms.TextBox wordsearch_Height;
        private System.Windows.Forms.TextBox ws_Width;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox wordsearch_Name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox wordsearch_Difficulty;
        private System.Windows.Forms.TextBox tb_IP1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_IP2;
        private System.Windows.Forms.TextBox tb_IP3;
        private System.Windows.Forms.TextBox tb_IP4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}

