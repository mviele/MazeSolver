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
        }


        public Bitmap solve()
        {
            MyPriorityQueue <Node> open = new MyPriorityQueue<Node>();
            for(int x = 0; x < startPixelsX.Count; x++)
            {
                Node node = mazeArray[startPixelsX.ElementAt(x), startPixelsY.ElementAt(x)];
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
                    if (mazeArray[n.x - 1, n.y].cellType == 3)
                    {
                        mazeArray[n.x - 1, n.y].parentX = n.x;
                        mazeArray[n.x - 1, n.y].parentY = n.y;
                        end = mazeArray[n.x - 1, n.y];
                        break;
                    }
                    //Found a white pixel we haven't visited yet, update it's info and add it to the queue
                    else if (mazeArray[n.x - 1, n.y].cellType == 0 && !mazeArray[n.x - 1, n.y].visited)
                    {
                        mazeArray[n.x - 1, n.y].visited = true;
                        mazeArray[n.x - 1, n.y].parentX = n.x;
                        mazeArray[n.x - 1, n.y].parentY = n.y;
                        mazeArray[n.x - 1, n.y].steps = n.steps + 1;
                        mazeArray[n.x - 1, n.y].score += n.steps;
                        open.Enqueue(mazeArray[n.x - 1, n.y]);
                    }
                }
                //check cell to the right
                if (n.x + 1 < mazeImage.Height)
                {
                    //Found a finishing node, so now we finish
                    if (mazeArray[n.x + 1, n.y].cellType == 3)
                    {
                        mazeArray[n.x + 1, n.y].parentX = n.x;
                        mazeArray[n.x + 1, n.y].parentY = n.y;
                        end = mazeArray[n.x + 1, n.y];
                        break;
                    }
                    //Found a white pixel we haven't visited yet, update it's info and add it to the queue
                    else if (mazeArray[n.x + 1, n.y].cellType == 0 && !mazeArray[n.x + 1, n.y].visited)
                    {
                        mazeArray[n.x + 1, n.y].visited = true;
                        mazeArray[n.x + 1, n.y].parentX = n.x;
                        mazeArray[n.x + 1, n.y].parentY = n.y;
                        mazeArray[n.x + 1, n.y].steps = n.steps + 1;
                        mazeArray[n.x + 1, n.y].score += n.steps;
                        open.Enqueue(mazeArray[n.x + 1, n.y]);
                    }
                }
                //check cell above
                if (n.y - 1 >= 0)
                {
                    //Found a finishing node, so now we finish
                    if (mazeArray[n.x, n.y - 1].cellType == 3)
                    {
                        mazeArray[n.x, n.y - 1].parentX = n.x;
                        mazeArray[n.x, n.y - 1].parentY = n.y;
                        end = mazeArray[n.x, n.y - 1];
                        break;
                    }
                    //Found a white pixel we haven't visited yet, update it's info and add it to the queue
                    else if (mazeArray[n.x, n.y - 1].cellType == 0 && !mazeArray[n.x, n.y - 1].visited)
                    {
                        mazeArray[n.x, n.y - 1].visited = true;
                        mazeArray[n.x, n.y - 1].parentX = n.x;
                        mazeArray[n.x, n.y - 1].parentY = n.y;
                        mazeArray[n.x, n.y - 1].steps = n.steps + 1;
                        mazeArray[n.x, n.y - 1].score += n.steps;
                        open.Enqueue(mazeArray[n.x, n.y - 1]);
                    }
                }
                //check cell above
                if (n.y + 1 < mazeImage.Width)
                {
                    //Found a finishing node, so now we finish
                    if (mazeArray[n.x, n.y + 1].cellType == 3)
                    {
                        mazeArray[n.x, n.y + 1].parentX = n.x;
                        mazeArray[n.x, n.y + 1].parentY = n.y;
                        end = mazeArray[n.x, n.y + 1];
                        break;
                    }
                    //Found a white pixel we haven't visited yet, update it's info and add it to the queue
                    else if (mazeArray[n.x, n.y + 1].cellType == 0 && !mazeArray[n.x, n.y + 1].visited)
                    {
                        mazeArray[n.x, n.y + 1].visited = true;
                        mazeArray[n.x, n.y + 1].parentX = n.x;
                        mazeArray[n.x, n.y + 1].parentY = n.y;
                        mazeArray[n.x, n.y + 1].steps = n.steps + 1;
                        mazeArray[n.x, n.y + 1].score += n.steps;
                        open.Enqueue(mazeArray[n.x, n.y + 1]);
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
            Node currNode = mazeArray[x, y];
            while (true)
            {
                if (currNode.cellType == 0)
                {
                    mazeImage.SetPixel(x, y, Color.Lime);
                    currNode = mazeArray[currNode.parentX, currNode.parentY];
                }
                else if (currNode.parentX >= 0 && currNode.parentY >= 0)
                {
                    currNode = mazeArray[currNode.parentX, currNode.parentY];
                }
                else
                {
                    return;
                }
            }
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
                            startPixelsX.Add(i);
                            startPixelsY.Add(j);
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
