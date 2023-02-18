using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikCubeSolver
{
    internal class Cube
    {
        private Face top;
        private Face left;
        private Face right;
        private Face back;
        private Face bottom;
        private Face front;


        public Cube(Face top, Face front, Face left, Face right, Face back, Face bottom) 
        {
            this.top = top;
            this.front = front;
            this.left = left;
            this.right = right;
            this.back = back;
            this.bottom = bottom;
        }

        /// <summary>
        /// rotates the front face clockwise
        /// </summary>
        public void Front()
        {
            front.RotateClock();

            Color[] bottom = new Color[] { this.bottom.Tiles[0, 0], this.bottom.Tiles[0, 1], this.bottom.Tiles[0, 2] };

            Color[] left = new Color[] { this.left.Tiles[0, 2], this.left.Tiles[1, 2], this.left.Tiles[2, 2] };

            Array.Reverse(left);

            Color[] top = new Color[] { this.bottom.Tiles[2, 0], this.bottom.Tiles[2, 1], this.bottom.Tiles[2, 2] };

            Color[] right = new Color[] { this.right.Tiles[0, 0], this.right.Tiles[1, 0], this.right.Tiles[2, 0] };

            Array.Reverse(right);

            this.top.ChangeTopRow(left);

            this.right.ChangeLeftCol(top);

            this.bottom.ChangeTopRow(right);

            this.left.ChangeRightCol(bottom);

        }

        /// <summary>
        /// rotates the back face clockwise
        /// </summary>
        public void Back()
        {
            back.RotateClock();

            Color[] top = new Color[] { this.bottom.Tiles[2, 0], this.bottom.Tiles[2, 1], this.bottom.Tiles[2, 2] };

            Array.Reverse(top);

            Color[] right = new Color[] { this.right.Tiles[0, 2], this.right.Tiles[1, 2], this.right.Tiles[2, 2] };

            Color[] left = new Color[] { this.left.Tiles[0, 0], this.left.Tiles[1, 0], this.left.Tiles[2, 0] };

            Array.Reverse(left);

            Color[] bottom = new Color[] { this.bottom.Tiles[0, 0], this.bottom.Tiles[0, 1], this.bottom.Tiles[0, 2] };

            this.top.ChangeTopRow(right);

            this.right.ChangeRightCol(bottom);

            this.bottom.ChangeTopRow(left);

            this.left.ChangeLeftCol(top);
        }

        /// <summary>
        /// rotates the right face clockwise
        /// </summary>
        public void Right()
        {
            right.RotateClock();

            Color[] top = new Color[] { this.top.Tiles[0,2], this.top.Tiles[1, 2], this.top.Tiles[2, 2] };

            Array.Reverse(top);

            Color[] front = new Color[] { this.front.Tiles[0, 2], this.front.Tiles[1, 2], this.front.Tiles[2, 2] };

            Color[] bottom = new Color[] { this.bottom.Tiles[0, 2], this.bottom.Tiles[1, 2], this.bottom.Tiles[2, 2] };

            Color[] back = new Color[] { this.back.Tiles[0, 2], this.back.Tiles[1, 2], this.back.Tiles[2, 2] };

            Array.Reverse(back);

            this.top.ChangeRightCol(front);
            this.back.ChangeRightCol(top);
            this.bottom.ChangeLeftCol(back);
            this.front.ChangeRightCol(bottom);
        }

        /// <summary>
        /// roates the left face clockwise
        /// </summary>
        public void Left()
        {
            left.RotateClock();

            Color[] top = new Color[] { this.top.Tiles[0, 0], this.top.Tiles[1, 0], this.top.Tiles[2, 0] };
            Color[] front = new Color[] { this.front.Tiles[0, 0], this.front.Tiles[1, 0], this.front.Tiles[2, 0] };
            Color[] bottom = new Color[] { this.front.Tiles[0, 0], this.front.Tiles[1, 0], this.front.Tiles[2, 0] };

            Array.Reverse(bottom);

            Color[] back = new Color[] { this.back.Tiles[2, 2], this.back.Tiles[1, 2], this.back.Tiles[0, 2] };

            Array.Reverse(back);

            this.top.ChangeLeftCol(back);
            this.front.ChangeLeftCol(top);
            this.bottom.ChangeLeftCol(front);
            this.back.ChangeRightCol(bottom);
        }

        /// <summary>
        /// rotates the top face clockwise
        /// </summary>
        public void Top()
        {
            top.RotateClock();

            Color[] front = new Color[] { this.top.Tiles[0, 0], this.top.Tiles[0, 1], this.top.Tiles[0, 2] };
            Color[] left = new Color[] { this.left.Tiles[0, 0], this.left.Tiles[0, 1], this.left.Tiles[0, 2] };
            Color[] back = new Color[] { this.back.Tiles[0, 0], this.back.Tiles[0, 1], this.back.Tiles[0, 2] };
            Color[] right = new Color[] { this.right.Tiles[0, 0], this.right.Tiles[0, 1], this.right.Tiles[0, 2] };

            this.front.ChangeTopRow(right);
            this.left.ChangeTopRow(front);
            this.back.ChangeTopRow(left);
            this.right.ChangeTopRow(back);
        }

        /// <summary>
        /// rotates the bottom face clockwise
        /// </summary>
        public void Bottom()
        {
            this.bottom.RotateClock();

            Color[] front = new Color[] { this.top.Tiles[2, 0], this.top.Tiles[2, 1], this.top.Tiles[2, 2] };
            Color[] left = new Color[] { this.left.Tiles[2, 0], this.left.Tiles[2, 1], this.left.Tiles[2, 2] };
            Color[] back = new Color[] { this.back.Tiles[2, 0], this.back.Tiles[2, 1], this.back.Tiles[2, 2] };
            Color[] right = new Color[] { this.right.Tiles[2, 0], this.right.Tiles[2, 1], this.right.Tiles[2, 2] };

            this.front.ChangeBottomRow(left);
            this.right.ChangeBottomRow(front);
            this.back.ChangeBottomRow(right);
            this.back.ChangeBottomRow(front);
        }

        /// <summary>
        /// rotates the front face counter
        /// </summary>
        public void FrontPrime()
        {
            Front();
            Front();
            Front();
        }

        /// <summary>
        /// rotate the back face counter
        /// </summary>
        public void BackPrime()
        {
            Back();
            Back();
            Back();
        }

        /// <summary>
        /// rotates the right face counter
        /// </summary>
        public void RightPrime()
        {
            Right();
            Right();
            Right();
        }

        /// <summary>
        /// rotates the left face counter
        /// </summary>
        public void LeftPrime()
        {
            Left();
            Left();
            Left();
        }

        /// <summary>
        /// rotates the top face counter
        /// </summary>
        public void TopPrime()
        {
            Top();
            Top();
            Top();
        }

        /// <summary>
        /// rotates the bottom face counter
        /// </summary>
        public void BottomPrime()
        {
            Bottom();
            Bottom();
            Bottom();
        }

        


    }
}
