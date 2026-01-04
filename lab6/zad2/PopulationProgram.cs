using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;

public class PopulationRecord
{
    public Country country { get; set; }
    public string value { get; set; }
    public string date { get; set; }
}

public class Country
{
    public string id { get; set; }
    public string value { get; set; }
}

class Program
{
    static void Main()
    {
        string path = "db.json";

        if (!File.Exists(path))
        {
            Console.WriteLine("Brak pliku db.json w folderze uruchomieniowym.");
            return;
        }

        var records = LoadDb(path);

        while (true)
        {
            Console.WriteLine("\n=== POPULACJA ===");
            Console.WriteLine("1) Indie: różnica 1970 -> 2000");
            Console.WriteLine("2) USA: różnica 1965 -> 2010");
            Console.WriteLine("3) Chiny: różnica 1980 -> 2018");
            Console.WriteLine("4) Pokaż populację dla kraju i roku");
            Console.WriteLine("5) Różnica populacji dla kraju i zakresu lat");
            Console.WriteLine("6) % wzrost względem roku poprzedniego do wskazanego roku (dla kraju)");
            Console.WriteLine("0) Wyjście");
            Console.Write("Wybierz: ");

            var choice = Console.ReadLine();
            if (choice == "0") break;

            try
            {
                switch (choice)
                {
                    case "1":
                        Console.WriteLine(Diff(records, "IN", 1970, 2000));
                        break;

                    case "2":
                        Console.WriteLine(Diff(records, "US", 1965, 2010));
                        break;

                    case "3":
                        Console.WriteLine(Diff(records, "CN", 1980, 2018));
                        break;

                    case "4":
                        ShowPopulation(records);
                        break;

                    case "5":
                        ShowDiffCustom(records);
                        break;

                    case "6":
                        ShowPercentGrowth(records);
                        break;

                    default:
                        Console.WriteLine("Zła opcja.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
        }
    }

    static List<PopulationRecord> LoadDb(string path)
    {
        var json = File.ReadAllText(path);

        var data = JsonSerializer.Deserialize<List<PopulationRecord>>(json);
        return data ?? new List<PopulationRecord>();
    }

    static long? GetPopulation(List<PopulationRecord> records, string countryId, int year)
    {
        var rec = records.FirstOrDefault(r =>
            r.country != null &&
            string.Equals(r.country.id, countryId, StringComparison.OrdinalIgnoreCase) &&
            r.date == year.ToString());

        if (rec == null) return null;
        if (string.IsNullOrWhiteSpace(rec.value)) return null;

        if (long.TryParse(rec.value, NumberStyles.Integer, CultureInfo.InvariantCulture, out long v))
            return v;

        return null;
    }

    static string Diff(List<PopulationRecord> records, string countryId, int y1, int y2)
    {
        var p1 = GetPopulation(records, countryId, y1);
        var p2 = GetPopulation(records, countryId, y2);

        if (p1 == null || p2 == null)
            return "Brak danych dla wskazanych lat.";

        long diff = p2.Value - p1.Value;
        return $"Różnica {countryId} {y1}->{y2}: {diff}";
    }

    static void ShowPopulation(List<PopulationRecord> records)
    {
        Console.Write("Kraj (IN/CN/US): ");
        var c = Console.ReadLine()?.Trim().ToUpper();

        Console.Write("Rok: ");
        if (!int.TryParse(Console.ReadLine(), out int year))
        {
            Console.WriteLine("Zły rok.");
            return;
        }

        var pop = GetPopulation(records, c, year);
        if (pop == null) Console.WriteLine("Brak danych.");
        else Console.WriteLine($"Populacja {c} w {year}: {pop}");
    }

    static void ShowDiffCustom(List<PopulationRecord> records)
    {
        Console.Write("Kraj (IN/CN/US): ");
        var c = Console.ReadLine()?.Trim().ToUpper();

        Console.Write("Rok start: ");
        if (!int.TryParse(Console.ReadLine(), out int y1))
        {
            Console.WriteLine("Zły rok.");
            return;
        }

        Console.Write("Rok koniec: ");
        if (!int.TryParse(Console.ReadLine(), out int y2))
        {
            Console.WriteLine("Zły rok.");
            return;
        }

        Console.WriteLine(Diff(records, c, y1, y2));
    }

    static void ShowPercentGrowth(List<PopulationRecord> records)
    {
        Console.Write("Kraj (IN/CN/US): ");
        var c = Console.ReadLine()?.Trim().ToUpper();

        Console.Write("Do którego roku liczyć (np. 2010): ");
        if (!int.TryParse(Console.ReadLine(), out int toYear))
        {
            Console.WriteLine("Zły rok.");
            return;
        }

        var years = records
            .Where(r => r.country != null && string.Equals(r.country.id, c, StringComparison.OrdinalIgnoreCase))
            .Select(r => int.TryParse(r.date, out int y) ? y : (int?)null)
            .Where(y => y != null)
            .Select(y => y.Value)
            .Distinct()
            .OrderBy(y => y)
            .ToList();

        if (years.Count == 0)
        {
            Console.WriteLine("Brak danych dla kraju.");
            return;
        }

        foreach (var y in years)
        {
            if (y > toYear) break;

            var curr = GetPopulation(records, c, y);
            var prev = GetPopulation(records, c, y - 1);

            if (curr == null || prev == null) continue;
            if (prev.Value == 0) continue;

            double pct = (curr.Value - prev.Value) * 100.0 / prev.Value;
            Console.WriteLine($"{c} {y-1}->{y}: {pct:F2}%");
        }
    }
}
