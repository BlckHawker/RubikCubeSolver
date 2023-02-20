using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        bool buttonEnabled;

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

            cube = null;
            
        }

        private bool ValidCube(Color[,] top, Color[,] left, Color[,] front, Color[,] right, Color[,] back, Color[,] bottom)
        {
            List<Color> centerList = new List<Color>()
            {
                top[1,1],
                left[1,1],
                front[1, 1],
                right[1, 1],
                back[1, 1],
                bottom[1, 1],
            };

            if (centerList.Distinct().Count() != centerList.Count())
            {
                ShowErrorMessage("There is at least one duplicate center square.\n" +
                                 "Please fix this before moving on", "");
                return false;
            }
            
            List<Color> colorList = new List<Color>();

            colorList.AddRange(GetColors(top));
            colorList.AddRange(GetColors(left));
            colorList.AddRange(GetColors(front));
            colorList.AddRange(GetColors(right));
            colorList.AddRange(GetColors(back));
            colorList.AddRange(GetColors(bottom));


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

        private Color[] GetColors(Color[,] colors)
        { 
            List<Color> list = new List<Color>();

            foreach(Color c in colors)
            {
                list.Add(c);
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

            buttonEnabled = b;
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

        private Color[,] ConvertButtonsToColors(Button[,] buttons)
        {
            Color[,] colors = new Color[3, 3];

            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    colors[i,j] = buttons[i,j].BackColor;
                }
            }

            return colors;
        }

        private void frontButton_Click(object sender, EventArgs e)
        {
            Color[,] top = ConvertButtonsToColors(topButtons);
            Color[,] left = ConvertButtonsToColors(leftButtons);
            Color[,] front = ConvertButtonsToColors(frontButtons);
            Color[,] right = ConvertButtonsToColors(rightButtons);
            Color[,] bottom = ConvertButtonsToColors(bottomButtons);
            Color[,] back = ConvertButtonsToColors(backButtons);

            if (!ValidCube(top, left, front, right, back, bottom))
            {
                return;
            }

            if (cube == null)
            {
                cube = InitializeCube(top, left, front, right, bottom, back);
                ToggleTileClick(false);
            }

            cube.Front();
            UpdateDrawing(cube);

        }

        private void frontPrimeButton_Click(object sender, EventArgs e)
        {
            Color[,] top = ConvertButtonsToColors(topButtons);
            Color[,] left = ConvertButtonsToColors(leftButtons);
            Color[,] front = ConvertButtonsToColors(frontButtons);
            Color[,] right = ConvertButtonsToColors(rightButtons);
            Color[,] bottom = ConvertButtonsToColors(bottomButtons);
            Color[,] back = ConvertButtonsToColors(backButtons);


            if (!ValidCube(top, left, front, right, back, bottom))
            {
                return;
            }

            if (cube == null)
            {
                cube = InitializeCube(top, left, front, right, bottom, back);
                ToggleTileClick(false);
            }

            cube.Front();
            cube.Front();
            cube.Front();

            UpdateDrawing(cube);
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            Color[,] top = ConvertButtonsToColors(topButtons);
            Color[,] left = ConvertButtonsToColors(leftButtons);
            Color[,] front = ConvertButtonsToColors(frontButtons);
            Color[,] right = ConvertButtonsToColors(rightButtons);
            Color[,] bottom = ConvertButtonsToColors(bottomButtons);
            Color[,] back = ConvertButtonsToColors(backButtons);


            if (!ValidCube(top, left, front, right, back, bottom))
            {
                return;
            }

            if (cube == null)
            {
                cube = InitializeCube(top, left, front, right, bottom, back);
                ToggleTileClick(false);
            }

            cube.Up();
            UpdateDrawing(cube);
        }

        private void upPrimeButton_Click(object sender, EventArgs e)
        {
            Color[,] top = ConvertButtonsToColors(topButtons);
            Color[,] left = ConvertButtonsToColors(leftButtons);
            Color[,] front = ConvertButtonsToColors(frontButtons);
            Color[,] right = ConvertButtonsToColors(rightButtons);
            Color[,] bottom = ConvertButtonsToColors(bottomButtons);
            Color[,] back = ConvertButtonsToColors(backButtons);


            if (!ValidCube(top, left, front, right, back, bottom))
            {
                return;
            }

            if (cube == null)
            {
                cube = InitializeCube(top, left, front, right, bottom, back);
                ToggleTileClick(false);
            }

            cube.Up();
            cube.Up();
            cube.Up();

            UpdateDrawing(cube);
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            Color[,] top = ConvertButtonsToColors(topButtons);
            Color[,] left = ConvertButtonsToColors(leftButtons);
            Color[,] front = ConvertButtonsToColors(frontButtons);
            Color[,] right = ConvertButtonsToColors(rightButtons);
            Color[,] bottom = ConvertButtonsToColors(bottomButtons);
            Color[,] back = ConvertButtonsToColors(backButtons);


            if (!ValidCube(top, left, front, right, back, bottom))
            {
                return;
            }

            if (cube == null)
            {
                cube = InitializeCube(top, left, front, right, bottom, back);
                ToggleTileClick(false);
            }

            cube.Right();
            UpdateDrawing(cube);
        }

        private void rightPrimeButton_Click(object sender, EventArgs e)
        {
            Color[,] top = ConvertButtonsToColors(topButtons);
            Color[,] left = ConvertButtonsToColors(leftButtons);
            Color[,] front = ConvertButtonsToColors(frontButtons);
            Color[,] right = ConvertButtonsToColors(rightButtons);
            Color[,] bottom = ConvertButtonsToColors(bottomButtons);
            Color[,] back = ConvertButtonsToColors(backButtons);


            if (!ValidCube(top, left, front, right, back, bottom))
            {
                return;
            }

            if (cube == null)
            {
                cube = InitializeCube(top, left, front, right, bottom, back);
                ToggleTileClick(false);
            }

            cube.Right();
            cube.Right();
            cube.Right();

            UpdateDrawing(cube);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Color[,] top = ConvertButtonsToColors(topButtons);
            Color[,] left = ConvertButtonsToColors(leftButtons);
            Color[,] front = ConvertButtonsToColors(frontButtons);
            Color[,] right = ConvertButtonsToColors(rightButtons);
            Color[,] bottom = ConvertButtonsToColors(bottomButtons);
            Color[,] back = ConvertButtonsToColors(backButtons);


            if (!ValidCube(top, left, front, right, back, bottom))
            {
                return;
            }

            if (cube == null)
            {
                cube = InitializeCube(top, left, front, right, bottom, back);
                ToggleTileClick(false);
            }

            cube.Back();
            UpdateDrawing(cube);
        }

        private void backPrimeButton_Click(object sender, EventArgs e)
        {
            Color[,] top = ConvertButtonsToColors(topButtons);
            Color[,] left = ConvertButtonsToColors(leftButtons);
            Color[,] front = ConvertButtonsToColors(frontButtons);
            Color[,] right = ConvertButtonsToColors(rightButtons);
            Color[,] bottom = ConvertButtonsToColors(bottomButtons);
            Color[,] back = ConvertButtonsToColors(backButtons);


            if (!ValidCube(top, left, front, right, back, bottom))
            {
                return;
            }

            if (cube == null)
            {
                cube = InitializeCube(top, left, front, right, bottom, back);
                ToggleTileClick(false);
            }

            cube.Back();
            cube.Back();
            cube.Back();

            UpdateDrawing(cube);
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            Color[,] top = ConvertButtonsToColors(topButtons);
            Color[,] left = ConvertButtonsToColors(leftButtons);
            Color[,] front = ConvertButtonsToColors(frontButtons);
            Color[,] right = ConvertButtonsToColors(rightButtons);
            Color[,] bottom = ConvertButtonsToColors(bottomButtons);
            Color[,] back = ConvertButtonsToColors(backButtons);


            if (!ValidCube(top, left, front, right, back, bottom))
            {
                return;
            }

            if (cube == null)
            {
                cube = InitializeCube(top, left, front, right, bottom, back);
                ToggleTileClick(false);
            }

            cube.Down();
            UpdateDrawing(cube);
        }

        private void downPrimeButton_Click(object sender, EventArgs e)
        {
            Color[,] top = ConvertButtonsToColors(topButtons);
            Color[,] left = ConvertButtonsToColors(leftButtons);
            Color[,] front = ConvertButtonsToColors(frontButtons);
            Color[,] right = ConvertButtonsToColors(rightButtons);
            Color[,] bottom = ConvertButtonsToColors(bottomButtons);
            Color[,] back = ConvertButtonsToColors(backButtons);


            if (!ValidCube(top, left, front, right, back, bottom))
            {
                return;
            }

            if (cube == null)
            {
                cube = InitializeCube(top, left, front, right, bottom, back);
                ToggleTileClick(false);
            }

            cube.Down();
            cube.Down();
            cube.Down();

            UpdateDrawing(cube);
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            Color[,] top = ConvertButtonsToColors(topButtons);
            Color[,] left = ConvertButtonsToColors(leftButtons);
            Color[,] front = ConvertButtonsToColors(frontButtons);
            Color[,] right = ConvertButtonsToColors(rightButtons);
            Color[,] bottom = ConvertButtonsToColors(bottomButtons);
            Color[,] back = ConvertButtonsToColors(backButtons);


            if (!ValidCube(top, left, front, right, back, bottom))
            {
                return;
            }

            if (cube == null)
            {
                cube = InitializeCube(top, left, front, right, bottom, back);
                ToggleTileClick(false);
            }

            cube.Left();
            UpdateDrawing(cube);
        }

        private void leftPrimeButton_Click(object sender, EventArgs e)
        {
            Color[,] top = ConvertButtonsToColors(topButtons);
            Color[,] left = ConvertButtonsToColors(leftButtons);
            Color[,] front = ConvertButtonsToColors(frontButtons);
            Color[,] right = ConvertButtonsToColors(rightButtons);
            Color[,] bottom = ConvertButtonsToColors(bottomButtons);
            Color[,] back = ConvertButtonsToColors(backButtons);


            if (!ValidCube(top, left, front, right, back, bottom))
            {
                return;
            }

            if (cube == null)
            {
                cube = InitializeCube(top, left, front, right, bottom, back);
                ToggleTileClick(false);
            }

            cube.Left();
            cube.Left();
            cube.Left();

            UpdateDrawing(cube);
        }

        private Cube InitializeCube(Color[,] topColors, Color[,] leftColors, Color[,] frontColors, Color[,] rightColors, Color[,] bottomColors,  Color[,] backColors)
        {

            Face top = new Face(topColors);
            Face left = new Face(leftColors);
            Face front = new Face(frontColors);
            Face right = new Face(rightColors);
            Face back = new Face(backColors);
            Face bottom = new Face(bottomColors);

            return new Cube(top, front, left, right, back, bottom);
        }

        private void UpdateDrawing(Cube cube)
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

        private void loadButton_Click(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader("read.txt");

            //reads faces in this order: top, left, front, right, back, bottom

            List<Color[,]> faceColors = new List<Color[,]>()
            {
                new Color[3,3],
                new Color[3,3],
                new Color[3,3],
                new Color[3,3],
                new Color[3,3],
                new Color[3,3],
            };

            

            for (int i = 0; i < 6; i++)
            {
                string line = reader.ReadLine();

                for (int j = 0; j < 9; j++)
                {
                    int row = j / 3;
                    int col = j % 3;
                    faceColors[i][row, col] = CharToColor(line[j]);
                }
            }

            //CHECK VALID
            if (!ValidCube(faceColors[0], faceColors[1], faceColors[2], faceColors[3], faceColors[4], faceColors[5]))
            {
                return;
                
            }

            //update each face to it's respective colors
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    topButtons[i, j].BackColor = faceColors[0][i, j];
                    leftButtons[i, j].BackColor = faceColors[1][i, j];
                    frontButtons[i, j].BackColor = faceColors[2][i, j];
                    rightButtons[i, j].BackColor = faceColors[3][i, j];
                    backButtons[i, j].BackColor = faceColors[4][i, j];
                    bottomButtons[i, j].BackColor = faceColors[5][i, j];
                }
            }

            ToggleTileClick(true);


        }

        private Color CharToColor(char c)
        { 
            switch (c) 
            {
                case 'w':
                case 'W':
                    return Color.White;


                case 'r':
                case 'R':
                    return Color.Red;


                case 'y':
                case 'Y':
                    return Color.Yellow;


                case 'g':
                case 'G':
                    return Color.Green;


                case 'b':
                case 'B':
                    return Color.Blue;


                default:
                    return Color.Orange;

            }
        }
    }
}
