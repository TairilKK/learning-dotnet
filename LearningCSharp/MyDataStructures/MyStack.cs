namespace LearningCSharp.MyDataStructures;

public class MyStack<T>
{
    class Node<T>
    {
        private readonly T _data;
        private Node<T>? _prev;

        public Node(T data)
        {
            _data = data;
        }

        public Node<T> Push(T data)
        {
            Node<T> newNode = new Node<T>(data);
            newNode._prev = this;
           return newNode;
        }

        public T GetData()
        {
            return _data;
        }

        public Node<T>? GetPrev()
        {
            return _prev;
        }
    }

    private Node<T>? _top;
    private int _size = 0;
    public void Push(T data)
    {
        _size++;
        if (_top == null)
        {
            _top = new Node<T>(data);
            return;
        }
        _top = _top.Push(data);
    }

    public T Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Stack is empty");
        }

        T res = _top.GetData();
        _top = _top.GetPrev();
        _size--;

        return res;
    }

    public bool IsEmpty()
    {
        return _top is null;
    }

    public int GetSize()
    {
        return _size;
    }

    public T Front()
    {
        return _top.GetData();
    }
}