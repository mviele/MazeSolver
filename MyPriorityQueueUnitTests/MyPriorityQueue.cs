using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPriorityQueueUnitTests
{
    class MyPriorityQueue<T> where T : IComparable
    {
        private List<T> list;

        public MyPriorityQueue() {
            list = new List<T>();
        }

        public bool isEmpty()
        {
            if (list.Count == 0)
            {
                return true;
            }
            return false;
        }

        public void Enqueue(T t)
        {
            list.Add(t);
            int position = list.Count - 1;
            while (position > 0 && t.CompareTo(list.ElementAt(position / 2)) < 0)
            {
                swap(position, position / 2);
                position /= 2;
            }
        }

        public T Dequeue()
        {
            swap(0, list.Count - 1);
            T value = list.Last();
            list.RemoveAt(list.Count - 1);
            minHeapify(0);

            return value;
        }

        private void swap(int x1, int x2){
            var temp = list.ElementAt(x1);
            list.Insert(x1, list.ElementAt(x2));
            list.RemoveAt(x1 + 1);
            list.Insert(x2, temp);
            list.RemoveAt(x2 + 1);
        }

        private void minHeapify(int index)
        {
            int smallest = index;
            if (list.Count - 1 >= 2*index && list.ElementAt(2*index).CompareTo(list.ElementAt(smallest)) < 0)
            {
                smallest = 2*index;
            }
            if (list.Count - 1 >= (2 * index) + 1 && list.ElementAt((2 * index) + 1).CompareTo(list.ElementAt(smallest)) < 0)
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
