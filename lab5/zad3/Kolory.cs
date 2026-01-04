using System;
using System.Collections.Generic;

enum Kolor
{
    Czerwony = 1,
    Niebieski = 2,
    Zielony = 3,
    Zolty = 4,
    Fioletowy = 5
}

class Program
{
    static void Main()
    {
        List<Kolor> kolory = new List<Kolor>
        {
            Kolor.Czerwony,
            Kolor.Niebieski,
            Kolor.Zielony,
            Kolor.Zolty,
            Kolor.Fioletowy
        };

        Random rnd = new Random();
        Kolor wylosowany = kolory[rnd.Next(kolory.Count)];

        Console.WriteLine("=== ZGADNIJ KOLOR ===");
        Console.WriteLine("Dostępne kolory: Czerwony, Niebieski, Zielony, Żółty, Fioletowy");

        while (true)
        {
            try
            {
                Console.Write("Podaj kolor: ");
                string input = Console.ReadLine();

                Kolor strzal = ParsujKolor(input, kolory);

                if (strzal == wylosowany)
                {
                    Console.WriteLine("Brawo! Odhadłeś.");
                    break;
                }
                else
                {
                    Console.WriteLine("Nie, spróbuj ponownie.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
        }
    }

    static Kolor ParsujKolor(string input, List<Kolor> dozwolone)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Nie podano koloru.");

        string s = input.Trim().ToLower();

        // prosta normalizacja polskich znaków dla żółty
        s = s.Replace("ó", "o").Replace("ł", "l").Replace("ż", "z").Replace("ź", "z");

        Kolor k;
        switch (s)
        {
            case "czerwony":
                k = Kolor.Czerwony; break;
            case "niebieski":
                k = Kolor.Niebieski; break;
            case "zielony":
                k = Kolor.Zielony; break;
            case "zolty":
                k = Kolor.Zolty; break;
            case "fioletowy":
                k = Kolor.Fioletowy; break;
            default:
                throw new ArgumentException("Taki kolor nie istnieje na liście.");
        }

        if (!dozwolone.Contains(k))
            throw new ArgumentException("Kolor spoza dostępnej listy.");

        return k;
    }
}
