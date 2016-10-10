using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyPriorityQueueUnitTests
{
    [TestClass]
    public class MyPriorityQueueUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            MyPriorityQueue<int> q = new MyPriorityQueue<int>();
            q.Enqueue(2);
            q.Enqueue(4);
            q.Enqueue(1);
            q.Enqueue(3);
            Assert.IsTrue(q.Dequeue() == 1);
            Assert.IsTrue(q.Dequeue() == 2);
            Assert.IsTrue(q.Dequeue() == 3);
            Assert.IsTrue(q.Dequeue() == 4);

        }
    }
}
