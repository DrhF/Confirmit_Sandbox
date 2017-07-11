using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

  

   
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

            public DoublyLinkedList(params T[] arr)
            {
                DoublyLinkedListNode curr = new DoublyLinkedListNode(default(T));
                foreach (var elem in arr)
                {
                    DoublyLinkedListNode node = new DoublyLinkedListNode(elem);
                    if (Count == 0)
                    {

                        curr = node;
                        First = curr;
                    }
                    else
                    {
                        curr.Next = node;
                        node.Prev = curr;
                        curr = node;
                    }

                    Last = curr;
                    Count++;
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
                DoublyLinkedListNode node = new DoublyLinkedListNode(item) { Next = First };
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
                if (arrayIndex > array.Length)
                    throw new ArgumentOutOfRangeException(nameof(arrayIndex), "arrayIndex should not exceed length of array");
                DoublyLinkedListNode curr = First;
                Array.Resize(ref array, arrayIndex + Count);
                while (curr != null)
                {
                    array[arrayIndex++] = curr.Value;
                    curr = curr.Next;
                }
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




    class Program
    {
        static void Main(string[] args)
        {
            /*DoublyLinkedList<int> lst = new DoublyLinkedList<int>();

            // act
            lst.Add(10);
            lst.Add(40);
            lst.Add(-10);

            
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(
                    @"C:\Users\Даниил\Documents\Visual Studio 2015\Projects\Confirmit_sandbox\LinkedList\WriteLines2.txt")
            )
            {
                IEnumerator<int> enumerator = lst.GetEnumerator();
                file.WriteLine(enumerator.Current);
                while (enumerator.MoveNext())
                    file.WriteLine(enumerator.Current);
            //Assert.AreEqual(10, enumerator.Current);
            //enumerator.MoveNext();
            //Assert.AreEqual(40, enumerator.Current);
            //enumerator.MoveNext();
            //Assert.AreEqual(-10, enumerator.Current);
            
                foreach (var item in lst)
                {
                    file.WriteLine(item);
                }
            }*/

            int[] arr = { 5, 4, 3, 2, 1, 5, 4, 3, 2, 1 };

            // act

            DoublyLinkedList<int> lst = new DoublyLinkedList<int>(arr);
            
            // assert

            lst.RemoveLast();

            IEnumerator<int> enumerator = lst.GetEnumerator();
            for (int i = 0; i < arr.Length && enumerator.MoveNext(); ++i)
                //Assert.AreEqual(arr[i], enumerator.Current);
                Console.Out.WriteLine(enumerator.Current);

            /*try
            {
                enumerator.MoveNext();
                Console.Out.WriteLine( enumerator.Current);
                if (enumerator.MoveNext())
                    //Assert.Fail();
                    Console.Out.WriteLine("Error");
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
                
            }*/
           
        }
    }
}
