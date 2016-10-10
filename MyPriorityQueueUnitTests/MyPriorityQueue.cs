using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPriorityQueueUnitTests
{
    class MyPriorityQueue<T> where T: IComparable
    {
        private LinkedList<T> list;

        public MyPriorityQueue() {
            list = new LinkedList<T>();
        }

        public bool isEmpty()
        {
            if(list.Count == 0)
            {
                return true;
            }
            return false;
        }

        public void Enqueue (T t)
        {  
            LinkedListNode<T> currNode = list.First;
            bool added = false;
            while (currNode != null && !added)
            {
                if(currNode.Value.CompareTo(t) > 0)
                {
                    list.AddBefore(currNode, t);
                    added = true;
                }
                currNode = currNode.Next;
            }
            if (!added)
            {
                list.AddLast(t);
            }
        }

        public T Dequeue()
        {
            T value = list.First.Value;
            list.RemoveFirst();
            return value;
        }
    }
}
