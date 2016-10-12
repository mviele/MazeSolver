using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeSolver
{
    class MyPriorityQueue<T> where T : IComparable
    {
        //heap array
        private T[] list;
        //tail pointer
        private int size;

        /**
         * O(1)
         * Creates the heap array with the specified size.
         * Tail pointer is set to the head of the array, 
         * signifying an empty queue. 
         * 
         **/
        public MyPriorityQueue(int heapSize)
        {
            this.size = 0;
            list = new T[heapSize];
        }

        /**
         * O(1)
         * Returns true if the tail pointer points to the 
         * head of the heap array. 
         * 
         **/
        public bool isEmpty()
        {
            if (size == 0)
            {
                return true;
            }
            return false;
        }

        /**
         * O(log N)
         * Adds the new element t to the bottom of the heap, 
         * and then percolates it up to the approriate spot. 
         * 
         **/
        public void Enqueue(T t)
        {
            if (size == list.Length - 1)
            {
                throw new Exception("The queue is full");
            }
            list[size] = t;
            int position = size;
            size++;
            while (position > 0 && t.CompareTo(list[position / 2]) < 0)
            {
                swap(position, position / 2);
                position /= 2;
            }
        }

        /**
         * O(log N)
         * Returns the highest priority element in the heap, 
         * and reheapifies the heap. 
         * 
         **/
        public T Dequeue()
        {
            if (size == 0)
            {
                return default(T);
            }
            swap(0, size - 1);
            T value = list[size - 1];
            size--;
            minHeapify(0);

            return value;
        }

        /**
         * O(1)
         * Standard swap function, when past two indices in 
         * the heap array, this function swaps the values of 
         * those two locations.
         * 
         **/
        private void swap(int x1, int x2)
        {
            var temp = list[x1];
            list[x1] = list[x2];
            list[x2] = temp;
        }

        /**
         * O(log N)
         * Ensures the array meets the heap criteria, 
         * ensuring the tree created with the given 
         * index at the root is a min heap. 
         * 
         **/
        private void minHeapify(int index)
        {
            int smallest = index;
            //true if the given index has a left child, and that child is smaller than it
            if (size - 1 >= 2 * index && list[2 * index].CompareTo(list[smallest]) < 0)
            {
                smallest = 2 * index;
            }
            //true if the given index has a right child, and that child is smaller than it and the left child
            if (size - 1 >= (2 * index) + 1 && list[(2 * index) + 1].CompareTo(list[smallest]) < 0)
            {
                smallest = 2 * index + 1;
            }
            if (smallest != index)
            {
                swap(index, smallest);
                minHeapify(smallest);
            }
        }

    }
}