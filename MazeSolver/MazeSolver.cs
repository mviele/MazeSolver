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
            Bitmap mazeImage = (Bitmap) Image.FromFile(args[0]);
            Maze maze = new Maze(mazeImage);
        }
    }
}
