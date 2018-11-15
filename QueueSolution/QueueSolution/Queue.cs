using System;
using System.Collections;
using System.Collections.Generic;

namespace QueueSolution
{
    /// <summary>
    /// Class performs methods of queue.
    /// </summary>
    /// <typeparam name="T">Type of objcts.</typeparam>
    public class Queue<T> : IEnumerable<T>
    {
        private const int DEFAULT_CAPACITY = 4;

        private T[] arrayQueue;

        private int head;
        private int tail;
        private int currentCapacity;

        /// <summary>
        /// Current count of all elements.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Constructor initializes default values.
        /// </summary>
        public Queue()
        {
            currentCapacity = DEFAULT_CAPACITY;
            arrayQueue = new T[currentCapacity];
            Count = 0;
        }

        /// <summary>
        /// Constructor takes a count of elements in queue.
        /// </summary>
        /// <param name="capacity">Count of elements in queue.</param>
        public Queue(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException($"{nameof(capacity)} can't be less than 1");
            }

            currentCapacity = capacity;
            arrayQueue = new T[currentCapacity];
            Count = 0;
        }

        /// <summary>
        /// Clears all elements in queue.
        /// </summary>
        public void Clear()
        {
            if(Count == 0)
            {
                return;
            }

            head = tail = Count = 0;
            Array.Clear(arrayQueue, 0, arrayQueue.Length);
        }

        /// <summary>
        /// Checks if collection contains passed element.
        /// </summary>
        /// <param name="item">Passed element.</param>
        /// <returns>Result of finding.</returns>
        public bool Contains(T item)
        {
            if(Count == 0)
            {
                return false;
            }

            for (int i = 0; i < Count; i++)
            {
                if(EqualityComparer<T>.Default.Equals(arrayQueue[i], item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Takes element from the collection.
        /// </summary>
        /// <returns>Taken element.</returns>
        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException($"Collection is empty.");
            }

            T value = arrayQueue[head];
            arrayQueue[head] = default;
            head = (head + 1) % Count;
            Count--;

            return value;
        }

        /// <summary>
        /// Converts collection to an array.
        /// </summary>
        /// <returns>Converted collection to an array.</returns>
        public T[] ToArray()
        {
            T[] save = new T[Count];

            for (int i = 0; i < Count; i++)
            {
                save[i] = arrayQueue[i];
            }

            return save;
        }

        /// <summary>
        /// Inserts element inside the collection.
        /// </summary>
        /// <param name="value">Passed element.</param>
        public void Add(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"{nameof(value)} can't be equal to null.");
            }

            if (Count == currentCapacity)
            {
                Increase();
            }

            arrayQueue[tail] = value;
            tail = (tail + 1) % arrayQueue.Length;
            Count++;
        }

        private void Increase()
        {
            T[] newArray = new T[currentCapacity * 2];

            for (int i = 0; i < Count; i++)
            {
                newArray[i] = arrayQueue[i];
            }

            arrayQueue = newArray;
            currentCapacity *= 2;
            head = 0;
            tail = Count == currentCapacity ? 0 : Count;
        }

        /// <summary>
        /// Takes enumerator.
        /// </summary>
        /// <returns>Enumerator of the current collection.</returns>
        public IEnumerator<T> GetEnumerator()
            => new QueueEnumerator();

        /// <summary>
        /// Takes enumerator.
        /// </summary>
        /// <returns>Enumerator of the current collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary>
        /// Enumerator of the current collection.
        /// </summary>
        public struct QueueEnumerator : IEnumerator<T>
        {
            private readonly Queue<T> queue;
            private int index;
            private T current;

            /// <summary>
            /// Constructor which takes passed link.
            /// </summary>
            /// <param name="queue">Passed link.</param>
            public QueueEnumerator(Queue<T> queue)
            {
                this.queue = queue;
                index = -1;
                current = default;
            }

            /// <summary>
            /// Current element of the iteration.
            /// </summary>
            public T Current
            {
                get
                {
                    switch (index)
                    {
                        case -1:
                            {
                                throw new InvalidOperationException($"Enumerator can't be used.");
                            }
                        default:
                            return current;
                    }
                }
            }

            /// <summary>
            /// Current element of the iteration.
            /// </summary>
            object IEnumerator.Current => Current;

            /// <summary>
            /// Makes system resources free.
            /// </summary>
            public void Dispose()
            {
                index = -1;
                current = default;
            }

            /// <summary>
            /// Moves to the next element in the iteration.
            /// </summary>
            /// <returns>Result of the moving.</returns>
            public bool MoveNext()
            {
                if (index == -1)
                {
                    return false;
                }

                index++;

                if (index == queue.Count)
                {
                    index = -1;
                    current = default;

                    return false;
                }

                current = queue.arrayQueue[index];

                return true;
            }

            /// <summary>
            /// Resets index of the iterator.
            /// </summary>
            public void Reset() => index = -1;
        }
    }
}
