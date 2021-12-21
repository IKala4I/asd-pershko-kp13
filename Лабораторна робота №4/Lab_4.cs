using System;
using static System.Console;

namespace ASD_4_Lab
{
    class Node
    {
        public int data;
        public Node next;
        public Node(int data)
        {
            this.data = data;
        }
        public Node(int data, Node next)
        {
            this.data = data;
            this.next = next;
        }
    }
    class SinglyLinkedList
    {
        public Node tail;
        public Node head;
        public SinglyLinkedList(int data)
        {
            head = new Node(data);
            tail = head;
        }

        public void AddToPosition(int data, int pos)
        {
            Node current = head;
            int length = 0;
            while (current != null)
            {
                length++;
                current = current.next;
            }
            if (length > pos && pos > 0)
            {
                if (pos == 1)
                {
                    AddFirst(data);
                }
                else
                {
                    current = head;
                    for (int i = 1; i < pos - 1; i++)
                    {
                        current = current.next;
                    }
                    Node k = new Node(data);
                    k.next = current.next;
                    current.next = k;
                }
            }
            else
            {
                AddFirst(data);
            }
        }
        public void AddFirst(int data)
        {
            Node current = head;
            Node k = new Node(data);
            if (head == null)
            {
                k.next = head;
                head = k;
                tail = head;
            }
            else
            {
                k.next = head;
                head = k;
            }
        }
       
        public void AddLast(int data)
        {
            Node current = tail;
            Node k = new Node(data);
            if (head == null)
            {
                head = k;
                head = tail;
            }
            else
            {
                tail.next = k;
                tail = k;
            }
        }
        public void DeleteFromPosition(int pos)
        {
            Node current = head;
            if (current != null)
            {
                for (int i = 1; i < pos - 1; i++)
                {
                    current = current.next;
                }
                current.next = current.next.next;
            }
            else
                WriteLine("the list is empty");

        }

        public void DeleteFirst()
        {
            Node current = head;
            if (current != null)
            {
                head = current.next;
                current = null;
            }
            else
                WriteLine("the list is empty");
        }
        public void DeleteLast()
        {
            Node current = head;
            if (current != null)
            {
                if (tail == head)
                {
                    head = null;
                }
                else
                {
                    while (current.next != tail)
                    {
                        current = current.next;
                    }
                    tail = current;
                    current.next = null;
                }
            }
            else
                WriteLine("the list is empty");
            
        }
        public void Print()
        {
            if (head == null)
            {
                WriteLine("the list is empty");
            }
            else
            {
                Node current = head;

                string elem = "";

                while (current != null)
                {
                    elem = elem + " " + current.data;
                    current = current.next;
                }
                WriteLine(elem);
            }
        }
        public void Task_21(int val)
        {
            if (val > 0 && val < 51)
            {
                if (head == null)
                {
                    WriteLine("the list is empty");
                }
                else if (head == tail)
                {
                    Node current = head;
                    Node k = new Node(val);
                    k.next = head;
                    head = k;
                    tail = k.next;
                }
                else
                {
                    Node current = head;
                    while (current.next != tail)
                    {
                        current = current.next;
                    }
                    Node k = new Node(val);
                    k.next = current.next;
                    current.next = k;
                }

            }
            else
                WriteLine("The item is not in the range");
            
        }
    }

    class Lab_4
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            int count, value;

            WriteLine("Enter the value of the first item");

            while (!int.TryParse(ReadLine(), out value))
            {
                Write("try again\n");
            }

            SinglyLinkedList list = new SinglyLinkedList(value);

            WriteLine("Select from the range (0, 10) how many items to add to the list");

            while (!int.TryParse(ReadLine(), out count) || (count < 0 || count > 10))
            {
                Write("try again\n");
            }
           
            for (int i = 0; i < count; i++)
            {
                list.AddFirst(rnd.Next(1, 20));
            }
           
            while (true)
            {
                list.Print();

                WriteLine("\nChoose what to do\n");

                WriteLine("1 - Add the first item to the list"); 
                WriteLine("2 - Delete the first item in the list"); 
                WriteLine("3 - Add the last item to the list"); 
                WriteLine("4 - Delete the last item in the list");
                WriteLine("5 - Add an item to the list by number"); 
                WriteLine("6 - Delete an item in the list by number"); 
                WriteLine("7 - Task of laboratory work\n");

                string key = ReadLine();

                if (key == "1")
                {
                    Clear();
                    Write("element: "); int val = Convert.ToInt32(ReadLine());
                    list.AddFirst(val);
                }
                else if (key == "2")
                {
                    Clear();
                    list.DeleteFirst();
                    WriteLine("The first item has been removed");
                }
                else if (key == "3")
                {
                    Clear();
                    Write("element: "); int val = Convert.ToInt32(ReadLine());
                    list.AddLast(val);
                }
                else if (key == "4")
                {
                    Clear();
                    list.DeleteLast();
                    WriteLine("the last item was deleted");
                }
                else if (key == "5")
                {
                    Clear();
                    Write("number: "); int number = int.Parse(ReadLine());
                    Write("\nelement: "); int val = Convert.ToInt32(ReadLine());
                    list.AddToPosition(val, number);
                }
                else if (key == "6")
                {
                    Clear();
                    Write("number: "); int number = int.Parse(ReadLine());
                    list.DeleteFromPosition(number);
                }
                else if (key == "7")
                {
                    Clear();
                    Write("element: "); int val = Convert.ToInt32(ReadLine());
                    /*
                    if val in [1,50]
                        add val before tail
                    else
                        no need to add
                    */
                    list.Task_21(val);
                }

                else
                {
                    WriteLine("incorrect key");
                    ReadKey();
                    Clear();
                }

                Clear();
            }
        }
    }
}
