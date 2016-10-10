using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeSolver
{
    class MyPriorityQueue<T> where T: IComparable<T>
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
            if (this.isEmpty())
            {
                list.AddFirst(t);
            }
            else
            {
                LinkedListNode<T> currNode = list.First;
                bool added = false;
                while (currNode != null)
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
        }

        public T Dequeue()
        {
            T value = list.First.Value;
            list.RemoveFirst();
            return value;
        }
    }
}
