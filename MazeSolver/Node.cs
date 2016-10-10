using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeSolver
{
    class Node
    {
        private byte cellType { get; }
        private Int32 score { get; }

        public Node(byte cellType, Int32 this_x, Int32 this_y, Int32 goal_x, Int32 goal_y)
        {
            this.cellType = cellType;
            if (cellType > 0 && cellType < 3)
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
        }
    }
}
