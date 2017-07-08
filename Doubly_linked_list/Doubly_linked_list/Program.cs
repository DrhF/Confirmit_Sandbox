using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;

namespace Doubly_linked_list
{
    public class DoublyLinkedList<T> : ICollection<T>
    {
        public class DoublyLinkedListNode
        {
            public T Value { set; get; }
            public DoublyLinkedListNode Prev { set; get; }
            public DoublyLinkedListNode Next { set; get; }

            public DoublyLinkedListNode(T value)
            {
                Value = value;
                Prev = null;
                Next = null;
            }
        }

        private DoublyLinkedListNode First { set; get; }
        private DoublyLinkedListNode Last { set; get; }
        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            DoublyLinkedListNode curr = First;
            while (curr != null)
            {
                yield return curr.Value;
                curr = curr.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            DoublyLinkedListNode node = new DoublyLinkedListNode(item);

            if (First == null)
            {
                First = node;
                Last = node;
            }
            else
            {
                node.Prev = Last;
                Last.Next = node;
                Last = node;
            }

            
            Count++;
        }

        public void AddLast(T item)
        {
            Add(item);
        }

        public void AddFirst(T item)
        {
            DoublyLinkedListNode node = new DoublyLinkedListNode(item) {Next = First};
            if (Count == 0)
                Last = node;
            First = node;
            Count++;
        }

        public void Clear()
        {
            Count = 0;
            DoublyLinkedListNode curr = First;
            while (curr != null)
            {
                curr = curr.Next;
                curr.Prev = null;
            }
            First = Last = null;
        }

        public bool Contains(T item)
        {
            DoublyLinkedListNode curr = First;
            while (curr != null)
            {
                if (curr.Value.Equals(item))
                    return true;
                curr = curr.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            DoublyLinkedListNode prev = null, curr = First, next = First.Next;

            while (curr != null)
            {
                if (curr.Value.Equals(item))
                {
                    if (prev != null)
                    {
                        prev.Next = next;
                        if (next == null)
                        {
                            Last = prev;
                        }
                        else
                        {
                            next.Prev = prev;
                        }
                    }
                    else
                    {
                        First = First.Next;
                        if (First == null)
                            Last = null;
                        else
                            First.Prev = null;
                    }
                    Count--;
                    return true;
                }
                prev = curr;
                curr = next;
                next = next?.Next;
            }

            return false;
        }

        public void RemoveFirst()
        {
            if (Count == 0) return;
            First = First.Next;
            if (First == null)
                Last = First;
            else
                First.Prev = null;
            Count--;
        }

        public void RemoveLast()
        {
            switch (Count)
            {
                case 0:
                    return;
                case 1:
                    First = Last = null;
                    break;
                default:
                    Last = Last.Prev;
                    Last.Next = null;
                    break;
            }
            Count--;
        }
    }

    /*class Program
    {
        static void Main(string[] args)
        {
        }
    }*/
}
