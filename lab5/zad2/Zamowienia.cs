using System;
using System.Collections.Generic;

enum StatusZamowienia
{
    Oczekujace = 1,
    Przyjete = 2,
    Zrealizowane = 3,
    Anulowane = 4
}

class Program
{
    static Dictionary<int, List<string>> zamowienia = new Dictionary<int, List<string>>();
    static Dictionary<int, StatusZamowienia> statusy = new Dictionary<int, StatusZamowienia>();

    static void Main()
    {
        DodajPrzykladoweDane();

        while (true)
        {
            Console.WriteLine("\n=== ZAMÓWIENIA ===");
            Console.WriteLine("1) Dodaj zamówienie");
            Console.WriteLine("2) Zmień status zamówienia");
            Console.WriteLine("3) Wyświetl wszystkie");
            Console.WriteLine("0) Wyjście");
            Console.Write("Wybierz: ");

            string wybor = Console.ReadLine();
            if (wybor == "0") break;

            switch (wybor)
            {
                case "1":
                    DodajZamowienie();
                    break;
                case "2":
                    ZmienStatus();
                    break;
                case "3":
                    WyswietlWszystkie();
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja.");
                    break;
            }
        }
    }

    static void DodajPrzykladoweDane()
    {
        zamowienia[1001] = new List<string> { "Chleb", "Masło" };
        statusy[1001] = StatusZamowienia.Oczekujace;

        zamowienia[1002] = new List<string> { "Mleko", "Ser", "Makaron" };
        statusy[1002] = StatusZamowienia.Przyjete;
    }

    static void DodajZamowienie()
    {
        Console.Write("Podaj numer zamówienia: ");
        if (!int.TryParse(Console.ReadLine(), out int nr))
        {
            Console.WriteLine("Zły numer.");
            return;
        }

        if (zamowienia.ContainsKey(nr))
        {
            Console.WriteLine("Zamówienie o takim numerze już istnieje.");
            return;
        }

        List<string> produkty = new List<string>();
        Console.WriteLine("Dodawanie produktów (pusta linia kończy):");
        while (true)
        {
            Console.Write("Produkt: ");
            string p = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(p)) break;
            produkty.Add(p.Trim());
        }

        if (produkty.Count == 0)
        {
            Console.WriteLine("Nie dodano produktów.");
            return;
        }

        zamowienia[nr] = produkty;
        statusy[nr] = StatusZamowienia.Oczekujace;

        Console.WriteLine("Dodano zamówienie.");
    }

    static void ZmienStatus()
    {
        try
        {
            Console.Write("Podaj numer zamówienia: ");
            int nr = int.Parse(Console.ReadLine());

            if (!zamowienia.ContainsKey(nr) || !statusy.ContainsKey(nr))
                throw new KeyNotFoundException("Nie ma zamówienia o podanym numerze.");

            Console.WriteLine("Dostępne statusy:");
            Console.WriteLine("1) Oczekujące");
            Console.WriteLine("2) Przyjęte");
            Console.WriteLine("3) Zrealizowane");
            Console.WriteLine("4) Anulowane");
            Console.Write("Nowy status: ");

            int sInt = int.Parse(Console.ReadLine());
            if (sInt < 1 || sInt > 4)
            {
                Console.WriteLine("Nieprawidłowy status.");
                return;
            }

            StatusZamowienia nowy = (StatusZamowienia)sInt;
            ZmienStatusZamowienia(nr, nowy);

            Console.WriteLine("Status zmieniony.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Błąd: niepoprawny format liczby.");
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Inny błąd: {ex.Message}");
        }
    }

    static void ZmienStatusZamowienia(int nr, StatusZamowienia nowyStatus)
    {
        StatusZamowienia aktualny = statusy[nr];
        if (aktualny == nowyStatus)
            throw new ArgumentException("Nie można ustawić takiego samego statusu jak obecny.");

        statusy[nr] = nowyStatus;
    }

    static void WyswietlWszystkie()
    {
        if (zamowienia.Count == 0)
        {
            Console.WriteLine("Brak zamówień.");
            return;
        }

        foreach (var kv in zamowienia)
        {
            int nr = kv.Key;
            List<string> produkty = kv.Value;
            StatusZamowienia status = statusy.ContainsKey(nr) ? statusy[nr] : StatusZamowienia.Oczekujace;

            Console.WriteLine($"\nZamówienie #{nr} | Status: {status}");
            for (int i = 0; i < produkty.Count; i++)
                Console.WriteLine($"- {produkty[i]}");
        }
    }
}
