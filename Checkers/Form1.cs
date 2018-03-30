using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Linq.Expressions;

namespace Checkers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private Button[,] Buttons = new Button[8, 8];
        private Piece[,] Board = new Piece[8, 8];

        private Color FirstColour = Color.Azure;
        private Color SecondColour = Color.Black;


        private void Initialize()
        {
            SetUpButtons();
            SetUpColours();
            IntialPositions();
            ShowPieces();
        }

        private new void Click(object sender, EventArgs e)
        {
            Button Btn = (Button)sender;
            int x, y;

            //Subtract 48 to convert Char to Int
            x = Convert.ToInt16(Btn.Name[3]) - 48;
            y = Convert.ToInt16(Btn.Name[4]) - 48;

            //Buttons[x, y].BackColor = Buttons[x, y].BackColor == FirstColour ? SecondColour : FirstColour;
        }

        private void SetUpButtons()
        {
            var bindingFlags = BindingFlags.Instance |
                               BindingFlags.NonPublic |
                               BindingFlags.Public;

            var regex = new Regex(
            "btn(\\d)(\\d)$",
            RegexOptions.CultureInvariant
            | RegexOptions.Compiled);

            this.GetType()
            .GetFields(bindingFlags)
            .ToList()
            .Where(fi => fi.FieldType.Name == "Button" && regex.IsMatch(fi.Name))
            .ToList()
            .ForEach(fi =>
            {
                var m = regex.Match(fi.Name);
                var x = Convert.ToInt16(m.Groups[1].Value);
                var y = Convert.ToInt16(m.Groups[2].Value);

                Buttons[x, y] = (Button)fi.GetValue(this);
            });

            foreach (Button btn in Buttons)
            {
                btn.Click += new System.EventHandler(this.Click);
                btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
        }

        private void SetUpColours()
        {
           for (int x = 0; x < 8 ; x++)
           {
                for (int y = 0; y < 8; y++)
                {
                    Buttons[x, y].BackColor = (x + 1 + y + 1) % 2 == 0 ? FirstColour : SecondColour; 
                }
           }
        }

        private new void IntialPositions()
        {
            //Player 1
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if ((x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0))
                    {
                        Board[x, y] = new Piece(0);
                    }
                }
            }

            //Player 2
            for (int x = 5; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if ((x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0))
                    {
                        Board[x, y] = new Piece(1);
                    }
                }
            }
        }

        private new void ShowPieces()
        {
            int colour;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (Board[x,y] != null)
                    {
                        colour = Board[x, y].Colour;

                        switch (colour)
                        {
                            case 0: Buttons[x, y].BackgroundImage = Properties.Resources.CheckerBlack; break;
                            case 1: Buttons[x, y].BackgroundImage = Properties.Resources.CheckerWhite; break;
                        }
                    }
                }
            }
        }
    }
}
