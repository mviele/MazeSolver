using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MazeSolver
{
    class Maze
    {
        private Bitmap mazeImage;
        private Node[,] mazeArray;
        private List<Node> startPixels;
        private Int32 finishX, finishY;

        public Maze(Bitmap mazeImage)
        {
            this.mazeImage = mazeImage;
            mazeArray = new Node[mazeImage.Height, mazeImage.Width];
            startPixels = new List<Node>();
            this.buildMazeArray();
        }


        public void solve()
        {
            
        }

        private void findFinishPixels()
        {
            for (int i = 0; i < mazeImage.Height; i++)
            {
                for (int j = 0; j < mazeImage.Width; j++)
                {
                    if (mazeImage.GetPixel(i, j).R == 0 && mazeImage.GetPixel(i, j).G == 0 && mazeImage.GetPixel(i, j).B == 255)
                    {
                        if (hasMove(i, j))
                        {
                            finishX = i;
                            finishY = j;
                            return;
                        }
                    }
                }
            }
        }

        private void buildMazeArray()
        {
            for (int i = 0; i < mazeImage.Height; i++)
            {
                for (int j = 0; j < mazeImage.Width; j++)
                {
                    //This is true if the pixel is red
                    if (mazeImage.GetPixel(i, j).R == 255 && mazeImage.GetPixel(i, j).G == 0 && mazeImage.GetPixel(i, j).B == 0)
                    {
                        Node n = new Node(2, i, j, finishX, finishY);
                        mazeArray[i, j] = n;
                        if (hasMove(i, j))
                        {
                            //We want this so we can initially populate the open queue
                            startPixels.Add(n);
                        }
                    }
                    //This is true if the pixel is blue
                    else if (mazeImage.GetPixel(i, j).R == 0 && mazeImage.GetPixel(i, j).G == 0 && mazeImage.GetPixel(i, j).B == 255)
                    {
                        mazeArray[i, j] = new Node(3, i, j, finishX, finishY);
                    }
                    //This is true if the pixel is black
                    else if (mazeImage.GetPixel(i, j).R == 0 && mazeImage.GetPixel(i, j).G == 0 && mazeImage.GetPixel(i, j).B == 0)
                    {
                        mazeArray[i, j] = new Node(1, i, j, finishX, finishY);
                    }
                    //This is true if the pixel is any other color (should all be white)
                    else
                    {
                        mazeArray[i, j] = new Node(0, i, j, finishX, finishY);
                    }
                }
            }
        }


        private bool hasMove(Int32 x, Int32 y)
        {
            bool up = false;
            bool down = false;
            bool left = false;
            bool right = false;

            if(x - 1 < 0)
            {
                left = false;
            }
            else if(mazeImage.GetPixel(x - 1, y).R == 255 && mazeImage.GetPixel(x - 1, y).G == 255 && mazeImage.GetPixel(x - 1, y).B == 255)
            {
                left = true;
            }

            if (y - 1 < 0)
            {
                up = false;
            }
            else if (mazeImage.GetPixel(x, y - 1).R == 255 && mazeImage.GetPixel(x, y - 1).G == 255 && mazeImage.GetPixel(x, y - 1).B == 255)
            {
                up = true;
            }

            if (x + 1 == mazeImage.Width)
            {
                right = false;
            }
            else if (mazeImage.GetPixel(x + 1, y).R == 255 && mazeImage.GetPixel(x + 1, y).G == 255 && mazeImage.GetPixel(x + 1, y).B == 255)
            {
                right = true;
            }

            if (y + 1 == mazeImage.Height)
            {
                down = false;
            }
            else if (mazeImage.GetPixel(x, y + 1).R == 255 && mazeImage.GetPixel(x, y + 1).G == 255 && mazeImage.GetPixel(x, y + 1).B == 255)
            {
                down = true;
            }

            return up || down || left || right;
        }
    }
}
