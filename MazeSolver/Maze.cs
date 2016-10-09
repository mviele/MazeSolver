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
                    if(mazeImage.GetPixel(i, j).Equals(Color.Red))
                    {
                        startPixels.Add(new Coordinate(i, j));
                    }
                }
            }
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
                    if (mazeImage.GetPixel(i, j).Equals(Color.Blue))
                    {
                        startPixels.Add(new Coordinate(i, j));
                    }
                }
            }
        }
    }
}
