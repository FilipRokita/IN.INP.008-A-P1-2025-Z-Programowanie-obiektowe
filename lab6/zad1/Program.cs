using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        string txtPath = "contacts.txt";
        string jsonPath = "contacts.json";

        var txtRepo = new TxtContactRepository(txtPath);
        var jsonRepo = new JsonContactRepository(jsonPath);

        var contacts = new List<Contact>();

        while (true)
        {
            Console.WriteLine("\n=== KONTAKTY ===");
            Console.WriteLine("1) Wczytaj z TXT");
            Console.WriteLine("2) Zapisz do TXT");
            Console.WriteLine("3) Wczytaj z JSON");
            Console.WriteLine("4) Zapisz do JSON");
            Console.WriteLine("5) Dodaj kontakt");
            Console.WriteLine("6) Usuń kontakt");
            Console.WriteLine("7) Pokaż kontakty");
            Console.WriteLine("0) Wyjście");
            Console.Write("Wybierz: ");

            var choice = Console.ReadLine();

            try
            {
                if (choice == "0") break;

                switch (choice)
                {
                    case "1":
                        contacts = txtRepo.Load();
                        Console.WriteLine($"Wczytano {contacts.Count} z TXT.");
                        break;

                    case "2":
                        txtRepo.Save(contacts);
                        Console.WriteLine("Zapisano do TXT.");
                        break;

                    case "3":
                        contacts = jsonRepo.Load();
                        Console.WriteLine($"Wczytano {contacts.Count} z JSON.");
                        break;

                    case "4":
                        jsonRepo.Save(contacts);
                        Console.WriteLine("Zapisano do JSON.");
                        break;

                    case "5":
                        AddContact(contacts);
                        break;

                    case "6":
                        RemoveContact(contacts);
                        break;

                    case "7":
                        ShowContacts(contacts);
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

    static void AddContact(List<Contact> contacts)
    {
        Console.Write("Id: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Zły Id.");
            return;
        }

        if (contacts.Any(c => c.Id == id))
        {
            Console.WriteLine("Kontakt o takim Id już istnieje.");
            return;
        }

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("Name/Email nie może być puste.");
            return;
        }

        contacts.Add(new Contact { Id = id, Name = name.Trim(), Email = email.Trim() });
        Console.WriteLine("Dodano kontakt.");
    }

    static void RemoveContact(List<Contact> contacts)
    {
        Console.Write("Podaj Id do usunięcia: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Zły Id.");
            return;
        }

        var toRemove = contacts.FirstOrDefault(c => c.Id == id);
        if (toRemove == null)
        {
            Console.WriteLine("Nie znaleziono kontaktu.");
            return;
        }

        contacts.Remove(toRemove);
        Console.WriteLine("Usunięto kontakt.");
    }

    static void ShowContacts(List<Contact> contacts)
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine("Brak kontaktów.");
            return;
        }

        foreach (var c in contacts.OrderBy(c => c.Id))
            Console.WriteLine(c);
    }
}
