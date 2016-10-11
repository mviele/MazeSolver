using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeSolver
{
    class Node : IComparable
    {
        public Int32 x { get; set; }
        public Int32 y { get; set; }
        public Int32 parentX { get; set; }
        public Int32 parentY { get; set; }
        public byte cellType { get; }
        public Int32 score { get; set; }
        public Int32 steps { get; set; }
        public bool visited { get; set; }

        public Node(byte cellType, Int32 this_y, Int32 this_x, Int32 goal_x, Int32 goal_y)
        {
            this.cellType = cellType;
            this.x = this_x;
            this.y = this_y;
            if (cellType == 1)
            {
                this.score = Int32.MaxValue;
            }
            else if (cellType == 3)
            {
                this.score = 0;
            }
            else
            {
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
