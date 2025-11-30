using System;

namespace Lab03Zad2
{
    public class Samochod
    {
        // pola prywatne, na spokojnie opakowane w wlasciwosci
        private string marka;
        private string model;
        private string nadwozie;
        private string kolor;
        private int rokProdukcji;
        private int przebieg; // nie moze byc ujemny

        public string Marka
        {
            get => marka;
            set => marka = value;
        }

        public string Model
        {
            get => model;
            set => model = value;
        }

        public string Nadwozie
        {
            get => nadwozie;
            set => nadwozie = value;
        }

        public string Kolor
        {
            get => kolor;
            set => kolor = value;
        }

        public int RokProdukcji
        {
            get => rokProdukcji;
            set => rokProdukcji = value;
        }

        public int Przebieg
        {
            get => przebieg;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Przebieg nie może być ujemny.");
                }
                przebieg = value;
            }
        }

        // konstruktor wczytujący dane od użytkownika
        public Samochod()
        {
            // taki prosty interaktywny setup samochodu
            Console.Write("podaj markę: ");
            Marka = Console.ReadLine();

            Console.Write("podaj model: ");
            Model = Console.ReadLine();

            Console.Write("podaj nadwozie: ");
            Nadwozie = Console.ReadLine();

            Console.Write("podaj kolor: ");
            Kolor = Console.ReadLine();

            Console.Write("podaj rok produkcji: ");
            RokProdukcji = int.Parse(Console.ReadLine() ?? "0");

            // lekka walidacja przebiegu
            while (true)
            {
                Console.Write("podaj przebieg (km, nieujemny): ");
                if (int.TryParse(Console.ReadLine(), out int p) && p >= 0)
                {
                    Przebieg = p;
                    break;
                }
                Console.WriteLine("nieprawidłowy przebieg, spróbuj jeszcze raz");
            }
        }

        // konstruktor z parametrami (przeciążenie)
        public Samochod(string marka, string model, string nadwozie, string kolor, int rokProdukcji, int przebieg)
        {
            Marka = marka;
            Model = model;
            Nadwozie = nadwozie;
            Kolor = kolor;
            RokProdukcji = rokProdukcji;
            Przebieg = przebieg; // tu wpadnie przez setter z walidacją
        }

        // metoda wyswietlajaca informacje
        public virtual void Wyswietl()
        {
            Console.WriteLine("samochód:");
            Console.WriteLine($"  marka:        {Marka}");
            Console.WriteLine($"  model:        {Model}");
            Console.WriteLine($"  nadwozie:     {Nadwozie}");
            Console.WriteLine($"  kolor:        {Kolor}");
            Console.WriteLine($"  rok produkcji:{RokProdukcji}");
            Console.WriteLine($"  przebieg:     {Przebieg} km");
        }
    }

    public class SamochodOsobowy : Samochod
    {
        // zad 2: dodatkowe pola
        private double waga;          // 2.0 – 4.5 t
        private double pojemnosc;     // 0.8 – 3.0
        private int iloscOsob;

        public double Waga
        {
            get => waga;
            set
            {
                if (value < 2.0 || value > 4.5)
                {
                    throw new ArgumentException("Waga powinna być z przedziału 2.0–4.5 t.");
                }
                waga = value;
            }
        }

        public double Pojemnosc
        {
            get => pojemnosc;
            set
            {
                if (value < 0.8 || value > 3.0)
                {
                    throw new ArgumentException("Pojemność silnika powinna być z przedziału 0.8–3.0.");
                }
                pojemnosc = value;
            }
        }

        public int IloscOsob
        {
            get => iloscOsob;
            set => iloscOsob = value;
        }

        // konstruktor wczytujący dane od użytkownika (plus pola bazowe)
        public SamochodOsobowy() : base()
        {
            // teraz dokładamy część osobową
            while (true)
            {
                Console.Write("podaj wagę (w tonach, 2.0–4.5): ");
                if (double.TryParse(Console.ReadLine(), out double w) && w >= 2.0 && w <= 4.5)
                {
                    Waga = w;
                    break;
                }
                Console.WriteLine("nieprawidłowa waga, spróbuj jeszcze raz");
            }

            while (true)
            {
                Console.Write("podaj pojemność silnika (0.8–3.0): ");
                if (double.TryParse(Console.ReadLine(), out double poj) && poj >= 0.8 && poj <= 3.0)
                {
                    Pojemnosc = poj;
                    break;
                }
                Console.WriteLine("nieprawidłowa pojemność, spróbuj jeszcze raz");
            }

            Console.Write("podaj liczbę osób: ");
            IloscOsob = int.Parse(Console.ReadLine() ?? "0");
        }

        // przesłonięcie metody z klasy bazowej
        public override void Wyswietl()
        {
            base.Wyswietl();
            Console.WriteLine("parametry osobowe:");
            Console.WriteLine($"  waga:         {Waga} t");
            Console.WriteLine($"  pojemność:    {Pojemnosc} l");
            Console.WriteLine($"  ilość osób:   {IloscOsob}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // 1) samochod osobowy – konstruktor wczytujący dane od użytkownika
            Console.WriteLine("=== tworzenie samochodu osobowego ===");
            SamochodOsobowy osobowy = new SamochodOsobowy();

            // 2) pierwszy samochod – konstruktor bezparametrowy (wczytywanie)
            Console.WriteLine("\n=== tworzenie samochodu (konstruktor bezparametrowy) ===");
            Samochod s1 = new Samochod();

            // 3) drugi samochod – konstruktor z parametrami
            Console.WriteLine("\n=== tworzenie samochodu (konstruktor z parametrami) ===");
            Samochod s2 = new Samochod(
                "BMW",
                "E90",
                "sedan",
                "czarny",
                2007,
                250000
            );

            // wyswietlenie informacji
            Console.WriteLine("\n=== informacje o samochodach ===");
            osobowy.Wyswietl();
            Console.WriteLine();
            s1.Wyswietl();
            Console.WriteLine();
            s2.Wyswietl();
        }
    }
}
