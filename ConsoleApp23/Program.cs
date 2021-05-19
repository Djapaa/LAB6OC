using System;
using System.IO;
using System.Threading;


class Пример
{

    public static AutoResetEvent evWriter;
    public static AutoResetEvent evReader;
    public static string str = "";
    public static string path = @"C:\Users\esteb\source\repos\ConsoleApp23\ConsoleApp23\bin\Debug\1.txt";
    public static bool reading = false;

    //Тело потока-писателя
    public static void Писатель()
    {
        string stroka = "";
        char[] symb;
        while (reading)
        {
            // После вызова функции WaitForSingleObject
            // поток-писатель приостанавливает свою работу
            // до освобождения события evWriter.
            // Затем, возобновляет работу, предварительно
            // переведя событие evWriter в состояние– занято
            evWriter.WaitOne();
            if (str != null)
            {
                symb = str.ToCharArray();
                foreach (char i in symb)
                {

                    stroka += char.ToUpper(i);

                }
                Console.WriteLine(stroka);
                stroka = null;
                str = null;
            }
            // Освобождение события evReader.
            // После его освобождения проснется поток-читатель
            evReader.Set();

        }


    }

    //Тело потока-читателя
    public static void Читатель()
    {
        reading = true;
        StreamReader sr = new StreamReader(path);
        while (!sr.EndOfStream)
        {
            //После вызова функции WaitForSingleObject
            //поток-читатель приостанавливает свою работу
            //до освобождения события evReader. Затем,
            //возобновляет работу, предварительно переведя
            //событие evReader в состояние– занято
            evReader.WaitOne();
            if (str == null)
                str = sr.ReadLine();
            // Освобождение события evWriter.
            // После его освобождения проснется поток-писатель
            evWriter.Set();
        }

        reading = false;
        sr.Close();

    }


    public static void Main()
    {

        // событие evWriter изначально создаётся в сигнальном состоянии
        evWriter = new AutoResetEvent(true);
        // событие evReader изначально создаётся в несигнальном состоянии
        evReader = new AutoResetEvent(false);

        Thread читатель = new Thread(Читатель);
        Thread писатель = new Thread(Писатель);

        читатель.Start();
        писатель.Start();

        evReader.Set();

        // основной поток дожидается завершения работы
        // читателя и писателя
        читатель.Join();
        писатель.Join();
        Console.ReadKey();
    }
}