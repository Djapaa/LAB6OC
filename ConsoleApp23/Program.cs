using System;
using System.IO;
using System.Threading;


//class Program
//{
//    static AutoResetEvent waitEvent = new AutoResetEvent(true); // Переводим объект в сигнальное состояние в конструкторе.
//    static int x;

//    static void Main(string[] args)
//    {
//        for (int i = 0; i < 5; i++) // Пять итераций в которых будет запущено 5 потоков.
//        {
//            Thread newThread = new Thread(CountMethod); // Создаем обьект потока и передаем ему ссылку на метод.
//            newThread.Name = "Поток №" + i.ToString();// Присваиваем имя каждому потоку.
//            newThread.Start(); // Запускаем потоки.
//        }

//        Console.ReadLine();
//    }
//    public static void CountMethod()
//    {
//        waitEvent.WaitOne(); // Из метода, который выполняется в каждом потоке запускаем WaitOne, чтобы перевести потоки в состояние ожидания сигнального состояния. Теперь все потоки в состоянии ожидания.
//        for (x = 1; x < 9; x++)
//        {
//            Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, x);
//            Thread.Sleep(100);
//        }
//        waitEvent.Reset(); // Переводим объект в сигнальное состоянии, чтобы поток мог захватить ресурс.
//    }
//}








public class AutoResetEventSample
{
    private static AutoResetEvent autoReset = new AutoResetEvent(false);


    public static void Main()
    {
        for (int i = 0; i < 5; i++)
        {

        new Thread(Worker1).Start();
        new Thread(Worker2).Start();
        new Thread(Worker3).Start();
        new Thread(Worker4).Start();
        autoReset.Set();
            
        Thread.Sleep(200);
            Console.WriteLine('\t');
        }
        Console.WriteLine("Main thread reached to end.");
        Console.ReadLine();
    }
  

    public static void Worker1()
    {
        autoReset.WaitOne();
        Console.WriteLine("Worker1 is running {0}", 1);
        Thread.Sleep(20);
        autoReset.Set();
    }
    public static void Worker2()
    {
        autoReset.WaitOne();
        Console.WriteLine("Worker2 is running {0}", 2);
            Thread.Sleep(20);
        autoReset.Set();
    }
    public static void Worker3()
    {
        autoReset.WaitOne();
        Console.WriteLine("Worker3 is running {0}", 3);
        Thread.Sleep(20);
        autoReset.Set();
    }
    public static void Worker4()
    {
        autoReset.WaitOne();
        Console.WriteLine("Worker4 is running {0}", 4);
        Thread.Sleep(20);
        autoReset.Set();
    }
}