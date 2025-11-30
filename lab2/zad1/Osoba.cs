using System;

namespace Lab02Zad1
{
    internal class Osoba
    {
        // takie podstawowe pola opisujące osobe
        private string imie;
        private string nazwisko;
        private int wiek;

        public string Imie
        {
            get => imie;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 2)
                {
                    throw new ArgumentException("Imię musi mieć co najmniej 2 znaki.");
                }
                imie = value;
            }
        }

        public string Nazwisko
        {
            get => nazwisko;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 2)
                {
                    throw new ArgumentException("Nazwisko musi mieć co najmniej 2 znaki.");
                }
                nazwisko = value;
            }
        }

        public int Wiek
        {
            get => wiek;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Wiek musi być liczbą dodatnią.");
                }
                wiek = value;
            }
        }

        // konstruktor przyjmujący wszystkie trzy wartosci
        public Osoba(string imie, string nazwisko, int wiek)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Wiek = wiek;
        }

        public void WyswietlInformacje()
        {
            Console.WriteLine($"Osoba: {Imie} {Nazwisko}, wiek: {Wiek} lat");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // taki szybki test
            var o = new Osoba("Jan", "Kowalski", 25);
            o.WyswietlInformacje();
        }
    }
}
