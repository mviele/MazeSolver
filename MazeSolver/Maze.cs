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
        private List<Coordinate> startPixels;
        private List<Coordinate> finishPixels;

        public Maze(Bitmap mazeImage)
        {
            this.mazeImage = mazeImage;
            startPixels = new List<Coordinate>();
            finishPixels = new List<Coordinate>();
        }

        public void solve()
        {
            findStart();
            findFinish();
        }

        /// <summary>
        /// The findStart method searches through the mazeImage bitmap to find all the
        /// instances of red pixels, and places their coordinates in an List.
        /// </summary>
        private void findStart()
        {
            for(int i = 0; i < mazeImage.Height; i++)
            {
                for(int j = 0; j < mazeImage.Width; j++)
                {
                    if(mazeImage.GetPixel(i, j).R == 255 && mazeImage.GetPixel(i, j).G == 0 && mazeImage.GetPixel(i, j).B == 0)
                    {
                        if (hasMove(i, j))
                        {
                            startPixels.Add(new Coordinate(i, j));
                        }
                    }
                }
            }
            //Start Debugging apparati
            Console.Write("[START POINTS]\n");
            for (int x = 0; x < startPixels.Count; x++)
            {
                Console.Write(startPixels[x].ToString() + "\n");
            }
            Console.Write("\n");
            //End Debugging apparati
        }

        /// <summary>
        /// The findFinish method searches through the mazeImage bitmap to find all the
        /// instances of blue pixels, and places their coordinates in an List.
        /// </summary>
        private void findFinish()
        {
            for (int i = 0; i < mazeImage.Height; i++)
            {
                for (int j = 0; j < mazeImage.Width; j++)
                {
                    if (mazeImage.GetPixel(i, j).R == 0 && mazeImage.GetPixel(i, j).G == 0 && mazeImage.GetPixel(i, j).B == 255)
                    {
                        if (hasMove(i, j))
                        {
                            finishPixels.Add(new Coordinate(i, j));
                        }
                    }
                }
            }

            //Start Debugging apparati
            Console.Write("[FINISH POINTS]\n");
            for (int x = 0; x < finishPixels.Count; x++)
            {
                Console.Write(finishPixels[x].ToString() + "\n");
            }
            Console.Write("\n");
            //End Debugging apparati
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
