using System;
using System.Collections.Generic;

namespace Lab03Zad1
{
    // osoba bazowa
    public class Person
    {
        private string firstName;
        private string lastName;
        private int wiek;

        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }

        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }

        public int Wiek
        {
            get => wiek;
            set => wiek = value;
        }

        public Person(string firstName, string lastName, int wiek)
        {
            FirstName = firstName;
            LastName = lastName;
            Wiek = wiek;
        }

        // virtual zeby zad 1d dzialalo (polimorfizm)
        public virtual void View()
        {
            Console.WriteLine($"{FirstName} {LastName}, wiek {Wiek}");
        }
    }

    // ksiazka bazowa
    public class Book
    {
        // zad 1h: pola protected
        protected string title;
        protected Person author;
        protected int year;

        public Book(string title, Person author, int year)
        {
            this.title = title;
            this.author = author;
            this.year = year;
        }

        // zad 1j: virtual dzieci nadpisuja
        public virtual void View()
        {
            Console.WriteLine($"{title}, {author.FirstName} {author.LastName}, {year}");
        }

        public string Title => title;
    }

    // zad 1i: ksiazka przygodowa
    public class AdventureBook : Book
    {
        private string world;

        public AdventureBook(string title, Person author, int year, string world)
            : base(title, author, year)
        {
            this.world = world;
        }

        public override void View()
        {
            Console.WriteLine($"{title} (przygodowa) – świat: {world}, autor: {author.FirstName} {author.LastName}, rok: {year}");
        }
    }

    // zad 1i: ksiazka dokumentalna
    public class DocumentaryBook : Book
    {
        private string topic;

        public DocumentaryBook(string title, Person author, int year, string topic)
            : base(title, author, year)
        {
            this.topic = topic;
        }

        public override void View()
        {
            Console.WriteLine($"{title} (dokumentalna) – temat: {topic}, autor: {author.FirstName} {author.LastName}, rok: {year}");
        }
    }

    // czytelnik – dziedziczy po Person
    public class Reader : Person
    {
        // zad 1b: lista przeczytanych ksiazek
        // moze byc private – do zad 1f nie musi byc protected
        private readonly List<Book> readBooks = new List<Book>();

        public Reader(string firstName, string lastName, int wiek)
            : base(firstName, lastName, wiek)
        {
        }

        public void AddBook(Book book)
        {
            readBooks.Add(book);
        }

        // zad 1b + 1j: teraz wywolujemy View() na ksiazce, zeby dzialal polimorfizm
        public void ViewBook()
        {
            if (readBooks.Count == 0)
            {
                Console.WriteLine("brak książek");
                return;
            }

            foreach (var book in readBooks)
            {
                book.View();
            }
        }

        // zad 1c + 1d: View czytelnika
        public override void View()
        {
            base.View();
            Console.WriteLine("przeczytane książki:");
            ViewBook();
        }

        // helper dla Reviewer, bez zmiany na protected
        public List<Book> GetBooks() => readBooks;
    }

    // recenzent czytelnik z ocenami
    public class Reviewer : Reader
    {
        private readonly Random rnd = new Random();

        public Reviewer(string firstName, string lastName, int wiek)
            : base(firstName, lastName, wiek)
        {
        }

        public override void View()
        {
            // najpierw standardowy opis czytelnika + lista ksiazek
            base.View();

            Console.WriteLine("oceny recenzenta:");
            foreach (var book in GetBooks())
            {
                int ocena = rnd.Next(1, 11); // 1–10
                Console.WriteLine($"{book.Title} — ocena: {ocena}");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // autorzy (zad 1a)
            var autor1 = new Person("Adam", "Nowak", 45);
            var autor2 = new Person("Ewa", "Kowalska", 38);

            // ksiazki (zad 1a, 1i)
            var b1 = new Book("Zwykła książka", autor1, 2001);
            var b2 = new AdventureBook("Przygody w lesie", autor2, 2010, "Leśny świat");
            var b3 = new DocumentaryBook("Historia kosmosu", autor1, 2020, "astronomia");

            // czytelnicy (zad 1b-1c)
            var r1 = new Reader("Jan", "Nowak", 20);
            r1.AddBook(b1);
            r1.AddBook(b2);

            var r2 = new Reader("Ala", "Kowalska", 22);
            r2.AddBook(b3);

            // recenzenci (zad 1f)
            var rev1 = new Reviewer("Piotr", "Lis", 30);
            rev1.AddBook(b1);
            rev1.AddBook(b3);

            var rev2 = new Reviewer("Karol", "Zieliński", 28);
            rev2.AddBook(b2);

            // zad 1g: lista Person z Reader i Reviewer
            List<Person> osoby = new List<Person>
            {
                r1,
                r2,
                rev1,
                rev2
            };

            foreach (var o in osoby)
            {
                Console.WriteLine("\n=== obiekt ===");
                o.View(); // polimorficznie: dla Reader/Reviewer wywoluje ich View
            }
        }
    }
}
