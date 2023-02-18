using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RubikCubeSolver
{
    public partial class Form1 : Form
    {
        Button[,] topButtons;
        Button[,] leftButtons;
        Button[,] frontButtons;
        Button[,] rightButtons;
        Button[,] bottomButtons;
        Button[,] backButtons;

        Cube cube;


        bool tilesEnabled;

        public Form1()
        {
            InitializeComponent();

            cube = null;

            topButtons = new Button[,]
            {
                {topTopLeft,  topTopMiddle, topTopRight},
                { topMiddleLeft, topMiddleMiddle, topMiddleRight},
                { topBottomLeft, topBottomMiddle, topBottomRight},
            };

            leftButtons = new Button[,]
            {
                {leftTopLeft, leftTopMiddle, leftTopRight },
                {leftMiddleLeft, leftMiddleMiddle, leftMiddleRight },
                {leftBottomLeft, leftBottomMiddle, leftBottomRight },

            };

            frontButtons = new Button[,]
            {
                {frontTopLeft, frontTopMiddle, frontTopRight },
                { frontMiddleLeft, frontMiddleMiddle, frontMiddleRight},
                { frontBottomLeft,frontBottomMiddle,frontBottomRight },
            };

            rightButtons = new Button[,]
            {
                {rightTopLeft, rightTopMiddle, rightTopRight },
                { rightMiddleLeft, rightMiddleMiddle, rightMiddleRight},
                { rightBottomLeft,rightBottomMiddle,rightBottomRight },
            };

            backButtons = new Button[,]
            {
                {backTopLeft, backTopMiddle, backTopRight },
                { backMiddleLeft, backMiddleMiddle, backMiddleRight},
                { backBottomLeft,backBottomMiddle,backBottomRight },
            };

            bottomButtons = new Button[,]
            {
                {bottomTopLeft, bottomTopMiddle, bottomTopRight },
                { bottomMiddleLeft, bottomMiddleMiddle, bottomMiddleRight},
                { bottomBottomLeft,bottomBottomMiddle,bottomBottomRight },
            };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    topButtons[i, j].Click += Tile_Click;
                    leftButtons[i, j].Click += Tile_Click;
                    frontButtons[i, j].Click += Tile_Click;
                    rightButtons[i, j].Click += Tile_Click;
                    backButtons[i, j].Click += Tile_Click;
                    bottomButtons[i, j].Click += Tile_Click;
                }
            }

            ResetColors();
            ToggleTileClick(true);
        }

        private void ResetColors()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    topButtons[i, j].BackColor = Color.White;
                    leftButtons[i, j].BackColor = Color.Orange;
                    frontButtons[i, j].BackColor = Color.Green;
                    rightButtons[i, j].BackColor = Color.Red;
                    backButtons[i, j].BackColor = Color.Blue;
                    bottomButtons[i, j].BackColor = Color.Yellow;
                }
            }

        }

        private bool ValidCube()
        {
            List<Color> centerList = new List<Color>()
            { 
                topButtons[0,0].BackColor, 
                leftButtons[0,0].BackColor,
                frontButtons[0, 0].BackColor,
                rightButtons[0, 0].BackColor,
                backButtons[0, 0].BackColor,
                bottomButtons[0, 0].BackColor,
            };

            if (centerList.Distinct().Count() != centerList.Count())
            {
                ShowErrorMessage("There is at least one duplicate center square.\n" +
                                 "Please fix this before moving on", "");
                return false;
            }
            
            List<Color> colorList = new List<Color>();

            colorList.AddRange(GetButtonColors(topButtons));
            colorList.AddRange(GetButtonColors(leftButtons));
            colorList.AddRange(GetButtonColors(frontButtons));
            colorList.AddRange(GetButtonColors(rightButtons));
            colorList.AddRange(GetButtonColors(backButtons));
            colorList.AddRange(GetButtonColors(bottomButtons));


            Dictionary<Color, int> colorDictionary = new Dictionary<Color, int>
            {
                { Color.Red, colorList.Where(x => x == Color.Red).Count() },
                { Color.Orange, colorList.Where(x => x == Color.Orange).Count() },
                { Color.Yellow, colorList.Where(x => x == Color.Yellow).Count() },
                { Color.Green, colorList.Where(x => x == Color.Green).Count() },
                { Color.Blue, colorList.Where(x => x == Color.Blue).Count() },
                { Color.White, colorList.Where(x => x == Color.White).Count() },
            };

            Dictionary<Color, int> invalidColors = new Dictionary<Color, int>();

            foreach (Color key in colorDictionary.Keys)
            {
                if (colorDictionary[key] != 9)
                {
                    invalidColors[key] = colorDictionary[key];
                }
            }

            if (invalidColors.Count > 0)
            {
                string[] invalidStrings = invalidColors.Keys.Select(x => x.ToString()).ToArray();
                
                ShowErrorMessage("The count of the following colors does not equal to 9:\n"
                               + string.Join(", ", invalidStrings) 
                               + "\nPlease fix this before moving on", "");
                
                return false;
            }

            return true;
        }

        private Color[] GetButtonColors(Button[,] buttons)
        { 
            List<Color> list = new List<Color>();

            foreach(Button button in buttons)
            {
                list.Add(button.BackColor);
            }

            return list.ToArray();
        }

        private void ToggleTileClick(bool b)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    topButtons[i, j].Enabled = b;
                    leftButtons[i, j].Enabled = b;
                    frontButtons[i, j].Enabled = b;
                    rightButtons[i, j].Enabled = b;
                    backButtons[i, j].Enabled = b;
                    bottomButtons[i, j].Enabled = b;
                }
            }

            tilesEnabled = b;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetColors();
            ToggleTileClick(true);
        }

        /// <summary>
        /// Changes the color of the tile when clicked
        /// </summary>
        private void Tile_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;

            //white to red
            //orange to yellow
            //yellow to green
            //green to blue
            //blue to white

            Color[] colorArr = { Color.White, Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue };

            int index = Array.IndexOf(colorArr, button.BackColor);

            button.BackColor = colorArr[(index + 1) % colorArr.Length];
        }

        private void frontButton_Click(object sender, EventArgs e)
        {
            if (!ValidCube())
            {
                return;
            }

            if (cube == null)
            {
                InitializeCube();
            }

            cube.Front();
            UpdateDrawing();

        }

        private void frontPrimeButton_Click(object sender, EventArgs e)
        {
            if (!ValidCube())
            {
                return;
            }

            if(cube == null)
            {
                InitializeCube();
            }

            cube.FrontPrime();
            UpdateDrawing();
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            if (!ValidCube())
            {
                return;
            }

            if (cube == null)
            {
                InitializeCube();
            }

            cube.Up();
            UpdateDrawing();
        }

        private void upPrimeButton_Click(object sender, EventArgs e)
        {
            if (!ValidCube())
            {
                return;
            }

            if (cube == null)
            {
                InitializeCube();
            }

            cube.UpPrime();
            UpdateDrawing();
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            if (!ValidCube())
            {
                return;
            }

            if (cube == null)
            {
                InitializeCube();
            }

            cube.Right();
            UpdateDrawing();
        }

        private void rightPrimeButton_Click(object sender, EventArgs e)
        {
            if (!ValidCube())
            {
                return;
            }

            if (cube == null)
            {
                InitializeCube();
            }

            cube.RightPrime();
            UpdateDrawing();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (!ValidCube())
            {
                return;
            }

            if (cube == null)
            {
                InitializeCube();
            }

            cube.Back();
            UpdateDrawing();
        }

        private void backPrimeButton_Click(object sender, EventArgs e)
        {
            if (!ValidCube())
            {
                return;
            }

            if (cube == null)
            {
                InitializeCube();
            }

            cube.BackPrime();
            UpdateDrawing();
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            if (!ValidCube())
            {
                return;
            }

            if (cube == null)
            {
                InitializeCube();
            }

            cube.Down();
            UpdateDrawing();
        }

        private void downPrimeButton_Click(object sender, EventArgs e)
        {
            if (!ValidCube())
            {
                return;
            }

            if (cube == null)
            {
                InitializeCube();
            }

            cube.DownPrime();
            UpdateDrawing();
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            if (!ValidCube())
            {
                return;
            }

            if (cube == null)
            {
                InitializeCube();
            }

            cube.Left();
            UpdateDrawing();
        }

        private void leftPrimeButton_Click(object sender, EventArgs e)
        {
            if (!ValidCube())
            {
                return;
            }

            if (cube == null)
            {
                InitializeCube();
            }

            cube.LeftPrime();
            UpdateDrawing();
        }

        private void InitializeCube()
        {
            Color[,] topColors = new Color[3,3];
            Color[,] leftColors = new Color[3,3];
            Color[,] rightColors = new Color[3,3];
            Color[,] bottomColors = new Color[3,3];
            Color[,] frontColors = new Color[3,3];
            Color[,] backColors = new Color[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    topColors[i, j] = topButtons[i, j].BackColor;
                    leftColors[i, j] = leftButtons[i, j].BackColor;
                    rightColors[i, j] = rightButtons[i, j].BackColor;
                    bottomColors[i, j] = bottomButtons[i, j].BackColor;
                    frontColors[i, j] = frontButtons[i, j].BackColor;
                    backColors[i, j] = backButtons[i, j].BackColor;
                }
            }

            Face top = new Face(topColors);
            Face left = new Face(leftColors);
            Face right = new Face(rightColors);
            Face bottom = new Face(bottomColors);
            Face front = new Face(frontColors);
            Face back = new Face(backColors);

            cube = new Cube(top, front, left, right, back, bottom);
        }

        private void UpdateDrawing()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    topButtons[i, j].BackColor = cube.top.Tiles[i, j];
                    bottomButtons[i, j].BackColor = cube.bottom.Tiles[i, j];
                    rightButtons[i, j].BackColor = cube.right.Tiles[i, j];
                    leftButtons[i, j].BackColor = cube.left.Tiles[i, j];
                    frontButtons[i, j].BackColor = cube.front.Tiles[i, j];
                    backButtons[i, j].BackColor = cube.back.Tiles[i, j];
                }
            }

        }

        private void ShowErrorMessage(string message, string caption)
        { 
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
