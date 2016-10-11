using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPriorityQueueUnitTests
{
    class MyPriorityQueue<T> where T : IComparable
    {
        private T[] list;
        private int size;

        public MyPriorityQueue(int size) {
            this.size = 0;
            list = new T[size];
        }

        public bool isEmpty()
        {
            if (list.Length == 0)
            {
                return true;
            }
            return false;
        }

        public void Enqueue(T t)
        {
            if(size == list.Length - 1)
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

        public T Dequeue()
        {
            if(size == 0)
            {
                return default(T);
            }
            swap(0, size - 1);
            T value = list[size - 1];
            size--;
            minHeapify(0);

            return value;
        }

        private void swap(int x1, int x2){
            var temp = list[x1];
            list[x1] = list[x2];
            list[x2] = temp;
        }

        private void minHeapify(int index)
        {
            int smallest = index;
            if (size - 1 >= 2*index && list[2*index].CompareTo(list[smallest]) < 0)
            {
                smallest = 2*index;
            }
            if (size - 1 >= (2 * index) + 1 && list[(2 * index) + 1].CompareTo(list[smallest]) < 0)
            {
                smallest = 2*index + 1;
            }
            if (smallest != index)
            {
                swap(index, smallest);
                minHeapify(smallest);
            }
        }

    }
}
