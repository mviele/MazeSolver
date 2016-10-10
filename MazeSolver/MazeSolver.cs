using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MazeSolver
{
    class MazeSolver
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args[0]);
            Bitmap mazeImage = (Bitmap) Image.FromFile(args[0]);
            Maze maze = new Maze(mazeImage);
            maze.solve().Save(args[1], System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
