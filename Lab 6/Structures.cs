using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASD_Lab_6
{
    class Stack<T>
    {

        private LinkedListNode<T> head;

        private int size = 0;
        public int Count
        {
            get { return size; }
        }

        public Stack()
        {

        }

        public void Push(T value)
        {
            head = new LinkedListNode<T>(value, head);
            size++;
        }

        public T Pop()
        {
            T value = head.Value;

            head = head.Next;
            size--;

            return value;
        }

        public T Front()
        {
            if (size == 0)
            {
                return default(T);
            }
            else
                return head.Value;
        }

        public void PrintStack()
        {
            LinkedListNode<T> node = head;

            if (head == null)
            {
                Console.WriteLine("Stack is Empty");
            }
            else
            {
                int i = Count;

                Console.WriteLine("Stack Items: ");

                while (node != null)
                {
                    Console.WriteLine($"{i}.{node.Value}");
                    node = node.Next;
                    i--;
                }

                Console.WriteLine();
            }

        }
    }
}
