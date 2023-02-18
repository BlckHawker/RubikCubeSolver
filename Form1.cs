using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RubikCubeSolver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            topTopLeft.BackColor = Color.White;
            topTopMiddle.BackColor = Color.White;
            topTopRight.BackColor = Color.White;
            topMiddleLeft.BackColor = Color.White;
            topMiddleMiddle.BackColor = Color.White;
            topMiddleRight.BackColor = Color.White;
            topBottomLeft.BackColor = Color.White;
            topBottomMiddle.BackColor = Color.White;
            topBottomRight.BackColor = Color.White;
            leftTopLeft.BackColor = Color.White;
            leftTopMiddle.BackColor = Color.White;
            leftTopRight.BackColor = Color.White;
            leftMiddleLeft.BackColor = Color.White;
            leftMiddleMiddle.BackColor = Color.White;
            leftMiddleRight.BackColor = Color.White;
            leftBottomLeft.BackColor = Color.White;
            leftBottomMiddle.BackColor = Color.White;
            leftBottomRight.BackColor = Color.White;
            frontTopLeft.BackColor = Color.White;
            frontTopMiddle.BackColor = Color.White;
            frontTopRight.BackColor = Color.White;
            frontMiddleLeft.BackColor = Color.White;
            frontMiddleMiddle.BackColor = Color.White;
            frontMiddleRight.BackColor = Color.White;
            frontBottomLeft.BackColor = Color.White;
            frontBottomMiddle.BackColor = Color.White;
            frontBottomRight.BackColor = Color.White;
            rightTopLeft.BackColor = Color.White;
            rightTopMiddle.BackColor = Color.White;
            rightTopRight.BackColor = Color.White;
            rightMiddleLeft.BackColor = Color.White;
            rightMiddleMiddle.BackColor = Color.White;
            rightMiddleRight.BackColor = Color.White;
            rightBottomLeft.BackColor = Color.White;
            rightBottomMiddle.BackColor = Color.White;
            rightBottomRight.BackColor = Color.White;
            backTopLeft.BackColor = Color.White;
            backTopMiddle.BackColor = Color.White;
            backTopRight.BackColor = Color.White;
            backMiddleLeft.BackColor = Color.White;
            backMiddleMiddle.BackColor = Color.White;
            backMiddleRight.BackColor = Color.White;
            backBottomLeft.BackColor = Color.White;
            backBottomMiddle.BackColor = Color.White;
            backBottomRight.BackColor = Color.White;
            bottomTopLeft.BackColor = Color.White;
            bottomTopMiddle.BackColor = Color.White;
            bottomTopRight.BackColor = Color.White;
            bottomMiddleLeft.BackColor = Color.White;
            bottomMiddleMiddle.BackColor = Color.White;
            bottomMiddleRight.BackColor = Color.White;
            bottomBottomLeft.BackColor = Color.White;
            bottomBottomMiddle.BackColor = Color.White;
            bottomBottomRight.BackColor = Color.White;

            topTopLeft.Click += Tile_Click;
            topTopMiddle.Click += Tile_Click;
            topTopRight.Click += Tile_Click;
            topMiddleLeft.Click += Tile_Click;
            topMiddleMiddle.Click += Tile_Click;
            topMiddleRight.Click += Tile_Click;
            topBottomLeft.Click += Tile_Click;
            topBottomMiddle.Click += Tile_Click;
            topBottomRight.Click += Tile_Click;
            leftTopLeft.Click += Tile_Click;
            leftTopMiddle.Click += Tile_Click;
            leftTopRight.Click += Tile_Click;
            leftMiddleLeft.Click += Tile_Click;
            leftMiddleMiddle.Click += Tile_Click;
            leftMiddleRight.Click += Tile_Click;
            leftBottomLeft.Click += Tile_Click;
            leftBottomMiddle.Click += Tile_Click;
            leftBottomRight.Click += Tile_Click;
            frontTopLeft.Click += Tile_Click;
            frontTopMiddle.Click += Tile_Click;
            frontTopRight.Click += Tile_Click;
            frontMiddleLeft.Click += Tile_Click;
            frontMiddleMiddle.Click += Tile_Click;
            frontMiddleRight.Click += Tile_Click;
            frontBottomLeft.Click += Tile_Click;
            frontBottomMiddle.Click += Tile_Click;
            frontBottomRight.Click += Tile_Click;
            rightTopLeft.Click += Tile_Click;
            rightTopMiddle.Click += Tile_Click;
            rightTopRight.Click += Tile_Click;
            rightMiddleLeft.Click += Tile_Click;
            rightMiddleMiddle.Click += Tile_Click;
            rightMiddleRight.Click += Tile_Click;
            rightBottomLeft.Click += Tile_Click;
            rightBottomMiddle.Click += Tile_Click;
            rightBottomRight.Click += Tile_Click;
            backTopLeft.Click   += Tile_Click;
            backTopMiddle.Click += Tile_Click;
            backTopRight.Click += Tile_Click;
            backMiddleLeft.Click += Tile_Click;
            backMiddleMiddle.Click += Tile_Click;
            backMiddleRight.Click += Tile_Click;
            backBottomLeft.Click += Tile_Click;
            backBottomMiddle.Click += Tile_Click;
            backBottomRight.Click += Tile_Click;
            bottomTopLeft.Click += Tile_Click;
            bottomTopMiddle.Click += Tile_Click;
            bottomTopRight.Click += Tile_Click;
            bottomMiddleLeft.Click += Tile_Click;
            bottomMiddleMiddle.Click += Tile_Click;
            bottomMiddleRight.Click += Tile_Click;
            bottomBottomLeft.Click += Tile_Click;
            bottomBottomMiddle.Click += Tile_Click;
            bottomBottomRight.Click += Tile_Click;

            //
        }

        /// <summary>
        /// Changes the color of the tile when clicked
        /// </summary>
        public void Tile_Click(object sender, EventArgs e)
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

    }
}
