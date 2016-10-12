using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MazeSolver
{
    class Maze
    {
        //Bitmap of the maze image
        private Bitmap mazeImage;

        //Matrix of nodes representing the maze
        private Node[,] mazeArray;

        //List of x coordinates of valid starting pixels
        private List<Int32> startPixelsX;

        //List of y coordinates of valid starting pixels
        private List<Int32> startPixelsY;

        //The finishing coordinates, these will be our 
        //reference for creating the heuristic scores of each node
        private Int32 finishX, finishY;

        //The offset for getting the r, g, and b pixels from
        //the byte array of the image pixels. This will change
        //depending on the endianess of your machine.
        private byte redOffset, blueOffset, greenOffset;

        //Step size for iterating through the byte array of
        //the image pixels. It is defined as 4 since we are
        //using the argb encoding when grabbing the array.
        private readonly int STEP_SIZE = 4;

        public Maze(Bitmap mazeImage)
        {
            this.mazeImage = mazeImage;
            mazeArray = new Node[mazeImage.Height, mazeImage.Width];
            startPixelsX = new List<Int32>();
            startPixelsY = new List<Int32>();
            if (BitConverter.IsLittleEndian)
            {
                redOffset = 2;
                greenOffset = 1;
                blueOffset = 0;
            }
            else
            {
                redOffset = 1;
                greenOffset = 2;
                blueOffset = 3;
            }
            this.findFinishPixels();
            this.buildMazeArray();
            this.buildStartList();
        }

        /**
         * O(N*M)
         * A* search to find a (nearly) optimal solution to the maze.
         * It then prints the solution to the image and returns it. 
         * 
         **/
        public Bitmap solve()
        {
            //PriorityQueue represents the open set of nodes to be checked
            MyPriorityQueue <Node> open = new MyPriorityQueue<Node>(mazeImage.Width * mazeImage.Height);

            //loop through all starting pixels, initialize their respective nodes as starting nodes, and enqueue them
            for(int x = 0; x < startPixelsX.Count; x++)
            {
                Node node = mazeArray[startPixelsY.ElementAt(x), startPixelsX.ElementAt(x)];
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
                    //Found a white pixel we haven't visited yet, 
                    //update it's info and add it to the queue
                    else if (mazeArray[n.y, n.x - 1].cellType == 0 
                        && !mazeArray[n.y, n.x - 1].visited)
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
                    //Found a white pixel we haven't visited yet, 
                    //update it's info and add it to the queue
                    else if (mazeArray[n.y, n.x + 1].cellType == 0 
                        && !mazeArray[n.y, n.x + 1].visited)
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
                    //Found a white pixel we haven't visited yet, 
                    //update it's info and add it to the queue
                    else if (mazeArray[n.y - 1, n.x].cellType == 0 
                        && !mazeArray[n.y - 1, n.x].visited)
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
                    //Found a white pixel we haven't visited yet, 
                    //update it's info and add it to the queue
                    else if (mazeArray[n.y + 1, n.x].cellType == 0 
                        && !mazeArray[n.y + 1, n.x].visited)
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

        /**
         * O(N*M)
         * Traces the solution path back using the parentX and parentY
         * variables of each node, and prints this path in green onto
         * the Maze's stored image.
         * 
         **/
        private void printSolution(Int32 x, Int32 y)
        {
            Node currNode = mazeArray[y, x];

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, mazeImage.Width, mazeImage.Height);
            System.Drawing.Imaging.BitmapData mazeImageData =
                mazeImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Get the address of the first line.
            IntPtr imagePointer = mazeImageData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int byteArraySize = Math.Abs(mazeImageData.Stride) * mazeImage.Height;
            byte[] argbValues = new byte[byteArraySize];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(imagePointer, argbValues, 0, byteArraySize);

            Int64 rgbCounter = (currNode.y * mazeImage.Width + currNode.x) * STEP_SIZE;
            while (true)
            {
                //A white cell
                if (currNode.cellType == 0)
                {
                    //Set the corresponding pixel to green in the image
                    argbValues[rgbCounter + redOffset] = 0;
                    argbValues[rgbCounter + greenOffset] = 255;
                    argbValues[rgbCounter + blueOffset] = 0;

                    //grab the next node to examine in our backward trace of solution path
                    currNode = mazeArray[currNode.parentY, currNode.parentX];

                    //readjust our argb byte array pointer
                    rgbCounter = (currNode.y * mazeImage.Width + currNode.x) * STEP_SIZE;
                }
                //A blue finish node
                else if (currNode.parentX >= 0 && currNode.parentY >= 0)
                {
                    currNode = mazeArray[currNode.parentY, currNode.parentX];
                    rgbCounter = (currNode.y * mazeImage.Width + currNode.x) * STEP_SIZE;
                }
                //A red starting node
                else
                {
                    break; 
                }
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(argbValues, 0, imagePointer, byteArraySize);

            // Unlock the bits.
            mazeImage.UnlockBits(mazeImageData);
        }

        /**
         * O(N*M)
         * Searches through the maze for the first instance 
         * of a blue pixel and sets that as our target pixel
         * during our A* search
         * 
         **/
        private void findFinishPixels()
        {
            for (int i = 0; i < mazeImage.Width; i++)
            {
                for (int j = 0; j < mazeImage.Height; j++)
                {
                    if (mazeImage.GetPixel(i, j).R == 0 
                        && mazeImage.GetPixel(i, j).G == 0 
                        && mazeImage.GetPixel(i, j).B == 255)
                    {
                        finishX = i;
                        finishY = j;
                        return;
                    }
                }
            }
        }

        /**
         * O(N*M)
         * Traverses the byte array of argb data from the image,
         * and constructs our matrix of nodes that we use to 
         * represent the maze.
         * 
         **/
        private void buildMazeArray()
        {
            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, mazeImage.Width, mazeImage.Height);
            System.Drawing.Imaging.BitmapData mazeImageData =
                mazeImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Get the address of the first line.
            IntPtr imagePointer = mazeImageData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int byteArraySize = Math.Abs(mazeImageData.Stride) * mazeImage.Height;
            byte[] argbValues = new byte[byteArraySize];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(imagePointer, argbValues, 0, byteArraySize);

            Int64 rgbCounter = 0;
            for (int j = 0; j < mazeImage.Height; j++)
            {
                for (int i = 0; i < mazeImage.Width; i++)
                {

                    //This is true if the pixel is red
                    if (argbValues[rgbCounter + redOffset] == 255 
                        && argbValues[rgbCounter + greenOffset] == 0 
                        && argbValues[rgbCounter + blueOffset] == 0)
                    {
                        Node n = new Node(2, i, j, finishX, finishY);
                        mazeArray[j, i] = n;

                    }
                    //This is true if the pixel is blue
                    else if (argbValues[rgbCounter + redOffset] == 0 
                        && argbValues[rgbCounter + greenOffset] == 0 
                        && argbValues[rgbCounter + blueOffset] == 255)
                    {
                        mazeArray[j, i] = new Node(3, i, j, finishX, finishY);
                    }
                    //This is true if the pixel is black
                    else if (argbValues[rgbCounter + redOffset] == 0 
                        && argbValues[rgbCounter + greenOffset] == 0 
                        && argbValues[rgbCounter + blueOffset] == 0)
                    {
                        mazeArray[j, i] = new Node(1, i, j, finishX, finishY);
                    }
                    //This is true if the pixel is any other color (should all be white)
                    else
                    {
                        mazeArray[j, i] = new Node(0, i, j, finishX, finishY);
                    }
                    rgbCounter += STEP_SIZE;
                    if(rgbCounter >= byteArraySize)
                    {
                        break;
                    }
                }
            }

            // Unlock the bits.
            mazeImage.UnlockBits(mazeImageData);
        }

        /**
         * O(N*M)
         * Searches through the maze for valid starting locations.
         * These locations are defined as red nodes that are 
         * adjacent to at least one white node. 
         * 
         **/
        private void buildStartList()
        {
            for (int j = 0; j < mazeImage.Height; j++)
            {
                for (int i = 0; i < mazeImage.Width; i++)
                {
                    if (mazeArray[j, i].cellType == 2)
                    {
                        if (hasMove(j, i))
                        {
                            startPixelsX.Add(i);
                            startPixelsY.Add(j);
                        }
                    }
                }
            }
        }

        /**
         * O(1)
         * Checks each of the cardinal directions from a given 
         * coordinate to see if a white node borders it.
         * 
         **/
        private bool hasMove(Int32 x, Int32 y)
        {
            if(mazeArray[y, x - 1].cellType == 0 && x - 1 >= 0)
            {
                return true;
            }

            if (mazeArray[y - 1, x].cellType == 0 && y - 1 >= 0)
            {
                return true;
            }

            if (mazeArray[y, x + 1].cellType == 0 && x + 1 < mazeImage.Width)
            {
                return true;
            }

            if (mazeArray[y + 1, x].cellType == 0 && y + 1 < mazeImage.Height)
            {
                return true;
            }

            return false;
        }
    }
}
