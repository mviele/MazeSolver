using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeSolver
{
    /// <summary>
    /// Coordinate class represents a Cartesian XY coordinate.
    /// </summary>
    class Coordinate
    {
        private Int32 x; 
        private Int32 y;

        /// <summary>
        /// Constructor for Coordinate
        /// </summary>
        /// <param name="x">X value</param>
        /// <param name="y">Y value</param>
        public Coordinate(Int32 x, Int32 y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Getter for the X value.
        /// </summary>
        /// <returns>32 bit signed X value</returns>
        public Int32 getX()
        {
            return this.x;
        }

        /// <summary>
        /// Getter for the Y value.
        /// </summary>
        /// <returns>32 bit signed Y value</returns>
        public Int32 getY()
        {
            return this.y;
        }

        /// <summary>
        /// Setter for the X value.
        /// </summary>
        /// <param name="x">The new X value</param>
        public void setX(Int32 x)
        {
            this.x = x;
        }

        /// <summary>
        /// Setter for the Y value
        /// </summary>
        /// <param name="y">The new Y value</param>
        public void setY(Int32 y)
        {
            this.y = y;
        }

        override
        public String ToString()
        {
            return "(" + this.x + "," + this.y + ")";
        }
    }
}
