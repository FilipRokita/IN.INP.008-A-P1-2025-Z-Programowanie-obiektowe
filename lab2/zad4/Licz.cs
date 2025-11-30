using System;

namespace Lab02Zad4
{
    internal class Licz
    {
        // pole value trzymamy prywatnie
        private int value;

        // konstruktor inicjujący wartosc
        public Licz(int startValue)
        {
            value = startValue;
        }

        public void Dodaj(int x)
        {
            // prosta operacja dodawania do aktualnej wartosci
            value += x;
        }

        public void Odejmij(int x)
        {
            value -= x;
        }

        public void Wypisz()
        {
            Console.WriteLine($"aktualna wartość: {value}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // kilka obiektów i jakieś operacje, tak jak w zadaniu
            var a = new Licz(10);
            var b = new Licz(0);
            var c = new Licz(-5);

            a.Dodaj(5);
            a.Wypisz();

            b.Dodaj(100);
            b.Odejmij(20);
            b.Wypisz();

            c.Odejmij(5);
            c.Dodaj(2);
            c.Wypisz();
        }
    }
}
