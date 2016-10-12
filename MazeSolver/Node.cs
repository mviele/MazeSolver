using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeSolver
{
    class Node : IComparable
    {
        //x coordinate in the maze
        public Int32 x { get; set; }

        //y coordinate in the maze
        public Int32 y { get; set; }

        //x coordinate of the node reached immediately before this node 
        public Int32 parentX { get; set; }

        //y coordinate of the node reached immediately before this node 
        public Int32 parentY { get; set; }

        //blue = 3
        //red = 2
        //black = 1
        //white = 0
        public byte cellType { get; }

        //heuristic score of the node, used during the A* search
        public Int32 score { get; set; }

        //number of nodes away from the starting node
        public Int32 steps { get; set; }

        //whether or not this node has already been considered during the A* search
        public bool visited { get; set; }

        public Node(byte cellType, Int32 this_x, Int32 this_y, Int32 goal_x, Int32 goal_y)
        {
            this.cellType = cellType;
            this.x = this_x;
            this.y = this_y;
            //black nodes
            if (cellType == 1)
            {
                this.score = Int32.MaxValue;
            }
            //blue nodes
            else if (cellType == 3)
            {
                this.score = 0;
            }
            //White and red nodes
            else
            {
                //heuristic score is the shortest path, regardless of walls, to the finishing node
                this.score = Math.Abs(this_x - goal_x) + Math.Abs(this_y - goal_y);
            }
            visited = false;
        }

        public int CompareTo(object obj)
        {
            Node n = (Node)obj;
            return this.score - n.score;
        }
    }
}
