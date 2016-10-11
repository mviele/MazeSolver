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
        private List<Int32> startPixelsX;
        private List<Int32> startPixelsY;
        private Int32 finishX, finishY;

        public Maze(Bitmap mazeImage)
        {
            this.mazeImage = mazeImage;
            mazeArray = new Node[mazeImage.Height, mazeImage.Width];
            startPixelsX = new List<Int32>();
            startPixelsY = new List<Int32>();
            this.findFinishPixels();
            this.buildMazeArray();
            this.buildStartList();
        }


        public Bitmap solve()
        {
            MyPriorityQueue <Node> open = new MyPriorityQueue<Node>();
            for(int x = 0; x < startPixelsX.Count; x++)
            {
                Node node = mazeArray[startPixelsY.ElementAt(x), startPixelsX.ElementAt(x)];
                if(node.cellType != 2)
                {
                    throw new Exception("Invalid Start Location");
                }
                node.visited = true;
                node.steps = 0;
                node.parentX = -1;
                node.parentY = -1;
                open.Enqueue(node);
            }

            Node end = null;
            Node n;
            while (!open.isEmpty())
            {
                n = open.Dequeue();
                //check cell to the left
                if(n.x - 1 >= 0)
                {
                    //Found a finishing node, so now we finish
                    if (mazeArray[n.y, n.x - 1].cellType == 3)
                    {
                        mazeArray[n.y, n.x - 1].parentX = n.x;
                        mazeArray[n.y, n.x - 1].parentY = n.y;
                        end = mazeArray[n.y, n.x - 1];
                        break;
                    }
                    //Found a white pixel we haven't visited yet, update it's info and add it to the queue
                    else if (mazeArray[n.y, n.x - 1].cellType == 0 && !mazeArray[n.y, n.x - 1].visited)
                    {
                        mazeArray[n.y, n.x - 1].visited = true;
                        mazeArray[n.y, n.x - 1].parentX = n.x;
                        mazeArray[n.y, n.x - 1].parentY = n.y;
                        mazeArray[n.y, n.x - 1].steps = n.steps + 1;
                        mazeArray[n.y, n.x - 1].score += n.steps;
                        open.Enqueue(mazeArray[n.y, n.x - 1]);
                    }
                }
                //check cell to the right
                if (n.x + 1 < mazeImage.Width)
                {
                    //Found a finishing node, so now we finish
                    if (mazeArray[n.y, n.x + 1].cellType == 3)
                    {
                        mazeArray[n.y, n.x + 1].parentX = n.x;
                        mazeArray[n.y, n.x + 1].parentY = n.y;
                        end = mazeArray[n.y, n.x + 1];
                        break;
                    }
                    //Found a white pixel we haven't visited yet, update it's info and add it to the queue
                    else if (mazeArray[n.y, n.x + 1].cellType == 0 && !mazeArray[n.y, n.x + 1].visited)
                    {
                        mazeArray[n.y, n.x + 1].visited = true;
                        mazeArray[n.y, n.x + 1].parentX = n.x;
                        mazeArray[n.y, n.x + 1].parentY = n.y;
                        mazeArray[n.y, n.x + 1].steps = n.steps + 1;
                        mazeArray[n.y, n.x + 1].score += n.steps;
                        open.Enqueue(mazeArray[n.y, n.x + 1]);
                    }
                }
                //check cell above
                if (n.y - 1 >= 0)
                {
                    //Found a finishing node, so now we finish
                    if (mazeArray[n.y - 1, n.x].cellType == 3)
                    {
                        mazeArray[n.y - 1, n.x].parentX = n.x;
                        mazeArray[n.y - 1, n.x].parentY = n.y;
                        end = mazeArray[n.y - 1, n.x];
                        break;
                    }
                    //Found a white pixel we haven't visited yet, update it's info and add it to the queue
                    else if (mazeArray[n.y - 1, n.x].cellType == 0 && !mazeArray[n.y - 1, n.x].visited)
                    {
                        mazeArray[n.y - 1, n.x].visited = true;
                        mazeArray[n.y - 1, n.x].parentX = n.x;
                        mazeArray[n.y - 1, n.x].parentY = n.y;
                        mazeArray[n.y - 1, n.x].steps = n.steps + 1;
                        mazeArray[n.y - 1, n.x].score += n.steps;
                        open.Enqueue(mazeArray[n.y - 1, n.x]);
                    }
                }
                //check cell above
                if (n.y + 1 < mazeImage.Height)
                {
                    //Found a finishing node, so now we finish
                    if (mazeArray[n.y + 1, n.x].cellType == 3)
                    {
                        mazeArray[n.y + 1, n.x].parentX = n.x;
                        mazeArray[n.y + 1, n.x].parentY = n.y;
                        end = mazeArray[n.y + 1, n.x];
                        break;
                    }
                    //Found a white pixel we haven't visited yet, update it's info and add it to the queue
                    else if (mazeArray[n.y + 1, n.x].cellType == 0 && !mazeArray[n.y + 1, n.x].visited)
                    {
                        mazeArray[n.y + 1, n.x].visited = true;
                        mazeArray[n.y + 1, n.x].parentX = n.x;
                        mazeArray[n.y + 1, n.x].parentY = n.y;
                        mazeArray[n.y + 1, n.x].steps = n.steps + 1;
                        mazeArray[n.y + 1, n.x].score += n.steps;
                        open.Enqueue(mazeArray[n.y + 1, n.x]);
                    }
                }
            }

            if(end != null)
            {
                this.printSolution(end.x, end.y);
                return mazeImage;
            }
            else
            {
                throw new Exception("No Solution Found");
            }
        }

        private void printSolution(Int32 x, Int32 y)
        {
            Node currNode = mazeArray[y, x];

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, mazeImage.Width, mazeImage.Height);
            System.Drawing.Imaging.BitmapData mazeImageData =
                mazeImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                mazeImage.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = mazeImageData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(mazeImageData.Stride) * mazeImage.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            int stepSize = 0;
            switch (mazeImage.PixelFormat)
            {
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                    stepSize = 3;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    stepSize = 4;
                    break;
                default:
                    throw new Exception("Unsupported Pixel format");

            }

            Int64 rgbCounter = (currNode.y * mazeImage.Width + currNode.x) * stepSize;
            while (true)
            {
                if (currNode.cellType == 0 && currNode.parentX >= 0 && currNode.parentY >= 0)
                {
                    rgbValues[rgbCounter + 2] = 0;
                    rgbValues[rgbCounter + 1] = 255;
                    rgbValues[rgbCounter ] = 0;
                    currNode = mazeArray[currNode.parentY, currNode.parentX];
                    rgbCounter = (currNode.y * mazeImage.Width + currNode.x) * stepSize;
                }
                else if (currNode.parentX >= 0 && currNode.parentY >= 0)
                {
                    currNode = mazeArray[currNode.parentY, currNode.parentX];
                    rgbCounter = (currNode.y * mazeImage.Width + currNode.x) * stepSize;
                }
                else
                {
                    break;
                }
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            mazeImage.UnlockBits(mazeImageData);
        }

        private void findFinishPixels()
        {
            for (int i = 0; i < mazeImage.Height; i++)
            {
                for (int j = 0; j < mazeImage.Width; j++)
                {
                    if (mazeImage.GetPixel(i, j).R == 0 && mazeImage.GetPixel(i, j).G == 0 && mazeImage.GetPixel(i, j).B == 255)
                    {
                        if (hasMove(j, i))
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
            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, mazeImage.Width, mazeImage.Height);
            System.Drawing.Imaging.BitmapData mazeImageData =
                mazeImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                mazeImage.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = mazeImageData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(mazeImageData.Stride) * mazeImage.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            int stepSize = 0;
            switch (mazeImage.PixelFormat)
            {
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                    stepSize = 3;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    stepSize = 4;
                    break;
                default:
                    throw new Exception("Unsupported Pixel format");

            }
            Int64 rgbCounter = 0;
            for (int j = 0; j < mazeImage.Height; j++)
            {
                for (int i = 0; i < mazeImage.Width; i++)
                {

                    //This is true if the pixel is red
                    if (rgbValues[rgbCounter + 2] == 255 && rgbValues[rgbCounter + 1] == 0 && rgbValues[rgbCounter] == 0)
                    {
                        Node n = new Node(2, i, j, finishX, finishY);
                        mazeArray[j, i] = n;

                    }
                    //This is true if the pixel is blue
                    else if (rgbValues[rgbCounter + 2] == 0 && rgbValues[rgbCounter + 1] == 0 && rgbValues[rgbCounter] == 255)
                    {
                        mazeArray[j, i] = new Node(3, i, j, finishX, finishY);
                    }
                    //This is true if the pixel is black
                    else if (rgbValues[rgbCounter + 2] == 0 && rgbValues[rgbCounter + 1] == 0 && rgbValues[rgbCounter] == 0)
                    {
                        mazeArray[j, i] = new Node(1, i, j, finishX, finishY);
                    }
                    //This is true if the pixel is any other color (should all be white)
                    else
                    {
                        mazeArray[j, i] = new Node(0, i, j, finishX, finishY);
                    }
                    rgbCounter += stepSize;
                    if(rgbCounter >= bytes)
                    {
                        break;
                    }
                }
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            mazeImage.UnlockBits(mazeImageData);
        }

        private void buildStartList()
        {
            for (int j = 0; j < mazeImage.Height; j++)
            {
                for (int i = 0; i < mazeImage.Width; i++)
                {
                    if (mazeArray[j, i].cellType == 2)
                    {
                        if (hasMove(i, j))
                        {
                            //We want this so we can initially populate the open queue
                            startPixelsX.Add(i);
                            startPixelsY.Add(j);
                        }
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
