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
using System.Diagnostics;

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

        private PictureBox[] ImagesW = new PictureBox[12];
        private PictureBox[] ImagesB = new PictureBox[12];

        private Color FirstColour = Color.Azure;
        private Color SecondColour = Color.Black;
        private Color SelectedColour = Color.Orange;

        private Boolean selected = false;
        private int[] Piece_Selected;

        private void Initialize()
        {
            SetUpButtons();
            SetUpColours();
            SetUpSidePictures();
            IntialPositions();
            ShowPieces();
            Debug.WriteLine("Done");

            ImagesB[0].BackgroundImage = Properties.Resources.CheckerBlack;
        }

        private new void Click(object sender, EventArgs e)
        {
            Button Btn = (Button)sender;
            int x, y;

            //Subtract 48 to convert Char to Int
            x = Convert.ToInt16(Btn.Name[3]) - 48;
            y = Convert.ToInt16(Btn.Name[4]) - 48;

            if (selected && Piece_Selected[0] == x && Piece_Selected[1] == y)
            {
                selected = false;
                SetUpColours();
            }
            else if (!selected)
            {
                if (Board[x, y] != null)
                {
                    selected = true;
                    Piece_Selected = new int[] { x, y };
                    Buttons[x, y].BackColor = SelectedColour;
                    Highlightpossiblemoves();
                }
            }
            else if (selected)
            {
                int from_x, from_y, to_x, to_y;

                if (Buttons[x, y].BackColor == SelectedColour)
                {
                    from_x = Piece_Selected[0];
                    from_y = Piece_Selected[1];

                    to_x = x;
                    to_y = y;

                    Board = Piece.Move(Board, from_x, from_y, to_x, to_y);
                    selected = false;

                    UpdatePiecesTaken();
                    SetUpColours();
                    ShowPieces();
                }
            }

            //Buttons[x, y].BackColor = Buttons[x, y].BackColor == FirstColour ? SecondColour : FirstColour;
        }

        private void Highlightpossiblemoves()
        {
            int x, y;

            List<int[]> PossibleMoves = Piece.GetLegalMoves(Board, Piece_Selected[0], Piece_Selected[1]);

            foreach (int[] Move in PossibleMoves)
            {
                x = Move[0];
                y = Move[1];

                Buttons[x, y].BackColor = SelectedColour;
            }
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

        private void SetUpSidePictures()
        {
            //White Pieces
            ImagesW[0] = PicWhite0;
            ImagesW[1] = PicWhite1;
            ImagesW[2] = PicWhite2;
            ImagesW[3] = PicWhite3;
            ImagesW[4] = PicWhite4;
            ImagesW[5] = PicWhite5;
            ImagesW[6] = PicWhite6;
            ImagesW[7] = PicWhite7;
            ImagesW[8] = PicWhite8;
            ImagesW[9] = PicWhite9;
            ImagesW[10] = PicWhite10;
            ImagesW[11] = PicWhite11;

            //Black Pieces
            ImagesB[0] = PicBlack0;
            ImagesB[1] = PicBlack1;
            ImagesB[2] = PicBlack2;
            ImagesB[3] = PicBlack3;
            ImagesB[4] = PicBlack4;
            ImagesB[5] = PicBlack5;
            ImagesB[6] = PicBlack6;
            ImagesB[7] = PicBlack7;
            ImagesB[8] = PicBlack8;
            ImagesB[9] = PicBlack9;
            ImagesB[10] = PicBlack10;
            ImagesB[11] = PicBlack11;
        }

        private void UpdatePiecesTaken()
        {
            int B = 12;
            int W = 12;

            foreach (Piece Piece in Board)
            {
                if (Piece != null)
                {
                    if (Piece.Colour == 0)
                    {
                        B--;
                    }
                    else if (Piece.Colour == 1)
                    {
                        W--;
                    }
                }
            }

            for (int i = 0; W > i ;i++)
            {
                ImagesW[i].Visible = true;
            }

            for (int i = 0; B > i ; i++)
            {
                ImagesB[i].Visible = true;
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

        private void IntialPositions()
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

        private void ShowPieces()
        {
            int colour;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (Board[x, y] != null)
                    {
                        colour = Board[x, y].Colour;

                        switch (colour)
                        {
                            case 0: Buttons[x, y].BackgroundImage = Properties.Resources.CheckerBlack; break;
                            case 1: Buttons[x, y].BackgroundImage = Properties.Resources.CheckerWhite; break;
                        }
                    }
                    else
                    {
                        Buttons[x, y].BackgroundImage = null;
                    }
                }
            }
        }
    }
}
