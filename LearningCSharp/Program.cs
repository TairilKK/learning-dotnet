using LearningCSharp.MyDataStructures;

Console.WriteLine("Test queue");

MyQueue<int> queue = new MyQueue<int>();

for (int i = 0; i < 10; i++)
{
   queue.Push(i);
}
Console.WriteLine($"Size: {queue.GetSize()}");

while (!queue.IsEmpty())
{
  Console.WriteLine(queue.Pop());
}

Console.WriteLine($"Size: {queue.GetSize()}");
