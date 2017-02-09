using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTicTacToe
{
    public partial class Form1 : Form
    {
        //Create Bitmaps for X & O pics.
        Bitmap oimg, ximg;
        //Make text alternate per user turn.
        int turn = 0;
        IEnumerable<Button> buttons;
        public Form1()
        {
            //Initialize Bitmaps images of x&o.  Properties >> Resources. Resize to match form (88, 88).
            oimg = new Bitmap(Properties.Resources.o, new Size(88, 88));
            oimg = new Bitmap(Properties.Resources.x, new Size(88, 88));

            InitializeComponent();
            //Add nextMove to each of the buttons:
            buttons = this.Controls.OfType<Button>();
            //Sort buttons in correct order:
            buttons = buttons.OrderBy(b => b.Name);
            foreach(Button button in buttons)
            {
                button.Click += new System.EventHandler(nextMove);
            }

        }
        //Event occurs on button click. Prompt player X or O to take turn.
        public void nextMove(object sender, EventArgs e)
        {
            //Set background for button.
            Button senderButton = (Button)sender;
            //Logic to determine a winner. Also makes it impossible to change once selected.
            if (turn >= 9 || senderButton.Text.Length <= 1)
                return;
            //Check who's turn it is:
            if(turn % 2 == 0)
            {
                senderButton.BackgroundImage = ximg;
            }
            else
            {
                senderButton.BackgroundImage = oimg;
            }
            //Regardless ~ change text:
            senderButton.Text = "";
            //Increment turn.
            turn++;
            //Switch prompt from X ~ O.
            foreach (Button button in buttons)
            {
                if (button.Text != "")
                {
                    if (turn % 2 == 0)
                        button.Text = "X Choose a square.";
                    else
                        button.Text = "O Choose a square.";
                }
            }
            //Check the board.
            if (turn >= 5)
                checkBoard();
        }
        public void checkBoard()
        {
            Bitmap winner = null;
            //Assign winner to matching rows or columbs.
            for(int i = 0; i < 3; i++)
            {
                //Check row:
                if(buttons.ElementAt(i*3).BackgroundImage == buttons.ElementAt((i*3)+1).BackgroundImage && buttons.ElementAt(i * 3).BackgroundImage == buttons.ElementAt((i * 3) + 2).BackgroundImage)
                {
                    winner = (Bitmap)buttons.ElementAt(i * 3).BackgroundImage;
                }
                //Check columbs:
                if (buttons.ElementAt(i).BackgroundImage == buttons.ElementAt(i + 3).BackgroundImage && buttons.ElementAt(i).BackgroundImage == buttons.ElementAt(i + 6).BackgroundImage)
                {
                    winner = (Bitmap)buttons.ElementAt(i).BackgroundImage;
                }
                if(winner == null)
                {
                    if (buttons.ElementAt(0).BackgroundImage == buttons.ElementAt(4).BackgroundImage && buttons.ElementAt(0).BackgroundImage == buttons.ElementAt(8).BackgroundImage)
                    {
                        winner = (Bitmap)buttons.ElementAt(0).BackgroundImage;
                    }
                    if (buttons.ElementAt(2).BackgroundImage == buttons.ElementAt(4).BackgroundImage && buttons.ElementAt(2).BackgroundImage == buttons.ElementAt(6).BackgroundImage)
                    {
                        winner = (Bitmap)buttons.ElementAt(2).BackgroundImage;
                    }
                }
                //Catch winner before loser picks after losing:
                if (winner != null)
                    break;
            }
            //Check diagonal:
            if(winner != null)
            {
                //Show winning messages:
                if (winner == ximg)
                    MessageBox.Show("X WINS!");
                else if (winner == oimg)
                    MessageBox.Show("O WINS");
            }
        }
    }
}
