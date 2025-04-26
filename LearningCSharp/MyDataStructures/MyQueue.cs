namespace LearningCSharp.MyDataStructures;

public class MyQueue<T>
{
    class Node<T>
    {
        private readonly T _data;
        private Node<T>? _next;

        public Node(T data)
        {
            _data = data;
        }

        public Node<T> Push(T data)
        {
            Node<T> newNode = new Node<T>(data);
            _next = newNode;
            return newNode;
        }

        public T GetData()
        {
            return _data;
        }

        public Node<T>? GetNext()
        {
            return _next;
        }
    }

    private Node<T>? _head, _tail;
    private int _size = 0;
    public void Push(T data)
    {
        _size++;
        if (_head == null)
        {
            _head = new Node<T>(data);
            _tail = _head;
            return;
        }
        _tail = _tail.Push(data);
    }

    public T Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Queue is empty");
        }

        T res = _head.GetData();
        _head = _head.GetNext();
        _size--;

        return res;
    }

    public bool IsEmpty()
    {
        return _head is null;
    }

    public int GetSize()
    {
        return _size;
    }

    public T Front()
    {
        return _head.GetData();
    }
}