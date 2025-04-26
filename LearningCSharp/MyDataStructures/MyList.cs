namespace LearningCSharp.MyDataStructures;

public class MyList<T>
{
    class Node<T>
    {
        private readonly T _data;
        private Node<T>? _prev, _next;

        public Node(T data, Node<T>? prev = null, Node<T>? next = null)
        {
            _data = data;
            _prev = prev;
            _next = next;
        }

        public T GetData()
        {
            return _data;
        }

        public Node<T> GetPrev()
        {
            return _prev;
        }

        public void SetPrev(Node<T>? prev)
        {
            _prev = prev;
        }

        public Node<T> GetNext()
        {
            return _next;
        }

        public void SetNext(Node<T>? next)
        {
            _next = next;
        }

    }
    private Node<T>? _head, _tail;
    private int _size;
    public bool IsEmpty()
    {
        return _head is null;
    }
    public void PushBack(T data)
    {
        if (IsEmpty())
        {
            _head = new Node<T>(data, null, null);
            _tail = _head;
            _size++;
            return;
        }

        var newTail = new Node<T>(data, _tail, null);
        _tail = newTail;
        _size++;
    }
    public T PopBack()
    {
        T popData = _tail.GetData();
        _tail = _tail.GetPrev();
        _tail.SetNext(null);
        _size--;
        return popData;
    }
    public void PushFront(T data)
    {
        if (IsEmpty())
        {
            PushBack(data);
            return;
        }

        var newFront = new Node<T>(data, null, _head);
        _head = newFront;
        _size++;
    }
    public T PopFront()
    {
        T popData = _head.GetData();
        _head = _head.GetNext();
        _head.SetPrev(null);
        _size--;
        return popData;
    }

    public void Remove(int idx)
    {
        if (idx >= _size)
        {
            return;
        }
        var node = _head;
        for (int i = 0; i < idx; i++)
        {
            if (node is null)
            {
                return;
            }
            node = node.GetNext();
        }
        var prev = node.GetPrev();
        if (prev is not null)
        {
            prev.SetNext(node.GetNext());
        }

        var next = node.GetNext();
        if (next is not null)
        {
            next.SetPrev(node.GetPrev());
        }

        node.SetPrev(null);
        node.SetNext(null);

        _size--;
    }
}