using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace QueueSolutionTests
{
    [TestFixture]
    public class QueueTests
    {
        [ExpectedException(typeof(InvalidOperationException))]
        public void Queue_UncorrectVersion_InvalidOperationException()
        {
            var queue = new QueueSolution.Queue<int>() { 1, 2, 3 };

            foreach (var item in queue)
            {
                queue.Add(5);
            }
        }

        [Test]
        public void Queue_DequeueEmpty_InvalidOperationException()
        {
            var queue = new QueueSolution.Queue<int>();

            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }

        [Test]
        public void Queue_AddNull_ArgumentNullException()
        {
            var queue = new QueueSolution.Queue<object>();
            Assert.Throws<ArgumentNullException>(() => queue.Add(null));
        }
        
        [TestCase(new int[] { 1, 2 }, ExpectedResult = new int[] { 1, 2, 10 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, ExpectedResult = new int[] { 1, 2, 3, 4, 10 })]
        public int[] Queue_Add_CorrectResult(int[] array)
        {
            var queue = new QueueSolution.Queue<int>();

            for (int i = 0; i < array.Length; i++)
            {
                queue.Add(array[i]);
            }

            queue.Add(10);

            return queue.ToArray();
        }

        [TestCase(new int[] { 1, 2, 3 }, ExpectedResult = true)]
        [TestCase(new int[] { 1, 2 }, ExpectedResult = false)]
        public bool Queue_Contains_CorrectResult(int[] array)
        {
            var queue = new QueueSolution.Queue<double>();

            for (int i = 0; i < array.Length; i++)
            {
                queue.Add(array[i]);
            }

            return queue.Contains(3);
        }
   
        [TestCase(new double[] { 1 }, ExpectedResult = new double[] { })]
        public double[] Queue_Dequeue_CorrectResult(double[] array)
        {
            var queue = new QueueSolution.Queue<double>();

            for (int i = 0; i < array.Length; i++)
            {
                queue.Add(array[i]);
            }

            queue.Dequeue();

            return queue.ToArray();
        }

        [TestCase(new object[] { "test", "test", "test" }, ExpectedResult = new object[] { })]
        public object[] Queue_Clear_CorrectResult(object[] array)
        {
            var queue = new QueueSolution.Queue<object>();

            for (int i = 0; i < array.Length; i++)
            {
                queue.Add(array[i]);
            }

            queue.Clear();

            return queue.ToArray();
        }
    }
}
