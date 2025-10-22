using System;
using System.Collections.Generic;

class Node
{
    public int Data;
    public Node Prev;
    public Node Next;

    public Node(int data)
    {
        Data = data;
        Prev = null;
        Next = null;
    }
}

class DoublyLinkedList
{
    private Node head;
    private Node tail;

    public DoublyLinkedList()
    {
        head = null;
        tail = null;
    }

    // Başa ekleme
    public void AddFirst(int data)
    {
        Node newNode = new Node(data);
        if (head == null)
            head = tail = newNode;
        else
        {
            newNode.Next = head;
            head.Prev = newNode;
            head = newNode;
        }
    }

    // Sona ekleme
    public void AddLast(int data)
    {
        Node newNode = new Node(data);
        if (tail == null)
            head = tail = newNode;
        else
        {
            tail.Next = newNode;
            newNode.Prev = tail;
            tail = newNode;
        }
    }

    // Araya herhangi bir veriden sonra ekleme
    public void AddAfter(int target, int data)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Data == target)
            {
                Node newNode = new Node(data);
                newNode.Next = current.Next;
                newNode.Prev = current;

                if (current.Next != null)
                    current.Next.Prev = newNode;
                else
                    tail = newNode;

                current.Next = newNode;
                return;
            }
            current = current.Next;
        }
    }

    // Araya herhangi bir veriden önce ekleme
    public void AddBefore(int target, int data)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Data == target)
            {
                Node newNode = new Node(data);
                newNode.Next = current;
                newNode.Prev = current.Prev;

                if (current.Prev != null)
                    current.Prev.Next = newNode;
                else
                    head = newNode;

                current.Prev = newNode;
                return;
            }
            current = current.Next;
        }
    }

    // Baştan silme
    public void RemoveFirst()
    {
        if (head == null) return;
        if (head == tail)
            head = tail = null;
        else
        {
            head = head.Next;
            head.Prev = null;
        }
    }

    // Sondan silme
    public void RemoveLast()
    {
        if (tail == null) return;
        if (head == tail)
            head = tail = null;
        else
        {
            tail = tail.Prev;
            tail.Next = null;
        }
    }

    // Aradan arayarak silme
    public void Remove(int data)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Data == data)
            {
                if (current.Prev != null)
                    current.Prev.Next = current.Next;
                else
                    head = current.Next;

                if (current.Next != null)
                    current.Next.Prev = current.Prev;
                else
                    tail = current.Prev;

                return;
            }
            current = current.Next;
        }
    }

    // Arama
    public bool Search(int data)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Data == data)
                return true;
            current = current.Next;
        }
        return false;
    }

    // Listeleme
    public void PrintList()
    {
        Node current = head;
        while (current != null)
        {
            Console.Write(current.Data + " ");
            current = current.Next;
        }
        Console.WriteLine();
    }

    // Tümünü silme
    public void Clear()
    {
        head = tail = null;
    }

    // Linked listi diziye aktarma
    public int[] ToArray()
    {
        List<int> list = new List<int>();
        Node current = head;
        while (current != null)
        {
            list.Add(current.Data);
            current = current.Next;
        }
        return list.ToArray();
    }
}
class Program
{
        
    static void Main()
    {
        DoublyLinkedList dll = new DoublyLinkedList();

        dll.AddFirst(10);
        dll.AddLast(20);
        dll.AddAfter(10, 15);
        dll.AddBefore(20, 18);
        dll.PrintList(); // 10 15 18 20

        dll.RemoveFirst();
        dll.RemoveLast();
        dll.PrintList(); // 15 18

        dll.Remove(15);
        dll.PrintList(); // 18

        Console.WriteLine(dll.Search(18)); // True
        Console.WriteLine(dll.Search(99)); // False

        int[] array = dll.ToArray();
        Console.WriteLine(string.Join(",", array)); // 18

        dll.Clear();
        dll.PrintList(); // (boş)
    }
}