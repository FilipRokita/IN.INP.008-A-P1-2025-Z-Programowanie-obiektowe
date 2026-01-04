using System;
using System.Collections.Generic;

enum Operacja
{
    Dodawanie = 1,
    Odejmowanie = 2,
    Mnozenie = 3,
    Dzielenie = 4
}

class Program
{
    static void Main()
    {
        List<double> historia = new List<double>();

        while (true)
        {
            Console.WriteLine("\n=== KALKULATOR ===");
            Console.WriteLine("1) Dodawanie");
            Console.WriteLine("2) Odejmowanie");
            Console.WriteLine("3) Mnożenie");
            Console.WriteLine("4) Dzielenie");
            Console.WriteLine("5) Pokaż historię");
            Console.WriteLine("0) Wyjście");
            Console.Write("Wybierz: ");

            string wybor = Console.ReadLine();

            if (wybor == "0") break;

            if (wybor == "5")
            {
                if (historia.Count == 0) Console.WriteLine("Historia jest pusta.");
                else
                {
                    Console.WriteLine("Historia wyników:");
                    for (int i = 0; i < historia.Count; i++)
                        Console.WriteLine($"{i + 1}. {historia[i]}");
                }
                continue;
            }

            try
            {
                if (!int.TryParse(wybor, out int opInt) || opInt < 1 || opInt > 4)
                {
                    Console.WriteLine("Nieprawidłowy wybór operacji.");
                    continue;
                }

                Operacja op = (Operacja)opInt;

                Console.Write("Podaj pierwszą liczbę: ");
                double a = double.Parse(Console.ReadLine());

                Console.Write("Podaj drugą liczbę: ");
                double b = double.Parse(Console.ReadLine());

                double wynik = WykonajOperacje(a, b, op);
                historia.Add(wynik);

                Console.WriteLine($"Wynik: {wynik}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Błąd: wpisano niepoprawny format liczby.");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Błąd: nie można dzielić przez zero.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Inny błąd: {ex.Message}");
            }
        }
    }

    static double WykonajOperacje(double a, double b, Operacja op)
    {
        switch (op)
        {
            case Operacja.Dodawanie:
                return a + b;
            case Operacja.Odejmowanie:
                return a - b;
            case Operacja.Mnozenie:
                return a * b;
            case Operacja.Dzielenie:
                if (b == 0) throw new DivideByZeroException();
                return a / b;
            default:
                throw new ArgumentOutOfRangeException(nameof(op));
        }
    }
}
