using System;

namespace Lab01Zad5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WczytajISortuj();
        }

        static void WczytajISortuj()
        {
            Console.Write("ile liczb chcesz wprowadzić (n): ");
            int n = Convert.ToInt32(Console.ReadLine());

            if (n <= 0)
            {
                Console.WriteLine("liczba elementów musi być dodatnia");
                return;
            }

            int[] arr = new int[n];

            for (int i = 0; i < n; i++)
            {
                Console.Write($"podaj liczbę #{i + 1}: ");
                arr[i] = Convert.ToInt32(Console.ReadLine());
            }

            BubbleSort(arr);

            Console.WriteLine("posortowane liczby (rosnąco):");
            foreach (int x in arr)
            {
                Console.Write(x + " ");
            }

            Console.WriteLine();
        }

        static void BubbleSort(int[] arr)
        {
            // klasyczny bubble sort, bez udziwnien
            int n = arr.Length;
            bool zamiana;

            do
            {
                zamiana = false;

                for (int i = 0; i < n - 1; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        int temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        zamiana = true;
                    }
                }

                n--; // po każdej rundzie ostatni element jest juz na miejscu
            }
            while (zamiana);
        }
    }
}
