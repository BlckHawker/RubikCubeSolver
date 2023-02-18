using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RubikCubeSolver
{
    public class Face
    {
        private Color[,] tiles;

        public Color[,] Tiles
        {
            get
            {
                return CopyFace();
            }
        }


        public Face(Color topLeft, Color topMiddle, Color topRight,
                    Color middleLeft, Color middleMiddle, Color middleRight,
                    Color bottomLeft, Color bottomMiddle, Color bottomRight)
        {
            tiles = new Color[,]{ { topLeft, topMiddle,topRight, },
                                { middleLeft,  middleMiddle,  middleRight},
                                { bottomLeft,  bottomMiddle,  bottomRight }
            };
        }

        public Face(Color[,] tiles)
        {
            this.tiles = new Color[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.tiles[i,j] = tiles[i,j];
                }
            }
        }

        public void RotateCounter()
        {
            RotateClock();
            RotateClock();
            RotateClock();
        }

        public void RotateClock()
        {
            Color[,] temp = CopyFace();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tiles[i, j] = temp[2-j, i];
                }
            }
        }

        private Color[,] CopyFace()
        {
            Color[,] temp = new Color[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp[i, j] = tiles[i, j];
                }
            }

            return temp;
        }

        public void ChangeTopRow(Color[] newRow)
        {
            for (int i = 0; i < 3; i++)
            {
                tiles[0,i] = newRow[i];
            }
        }

        public void ChangeBottomRow(Color[] newRow)
        {
            for (int i = 0; i < 3; i++)
            {
                tiles[2, i] = newRow[i];
            }
        }

        public void ChangeLeftCol(Color[] newCol)
        {
            for (int i = 0; i < 3; i++)
            {
                tiles[i, 0] = newCol[i];
            }
        }

        public void ChangeRightCol(Color[] newCol)
        {
            for (int i = 0; i < 3; i++)
            {
                tiles[i, 2] = newCol[i];
            }
        }
    }
}
