using System;
using System.Collections.Generic;

namespace Lab04People
{
    // interfejs osoba
    public interface IOsoba
    {
        string Imie     { get; set; }
        string Nazwisko { get; set; }

        string ZwrocPelnaNazwe();
    }

    // implementacja osoby
    public class Osoba : IOsoba
    {
        public string Imie     { get; set; }
        public string Nazwisko { get; set; }

        public Osoba(string imie, string nazwisko)
        {
            Imie = imie;
            Nazwisko = nazwisko;
        }

        public string ZwrocPelnaNazwe()
        {
            return $"{Imie} {Nazwisko}";
        }

        public override string ToString()
        {
            return ZwrocPelnaNazwe();
        }
    }

    // metody rozszerzajace dla List<IOsoba>
    public static class OsobaListExtensions
    {
        // zad 3b
        public static void WypiszOsoby(this List<IOsoba> osoby)
        {
            foreach (var o in osoby)
            {
                Console.WriteLine(o.ZwrocPelnaNazwe());
            }
        }

        // zad 3c
        public static void PosortujOsobyPoNazwisku(this List<IOsoba> osoby)
        {
            osoby.Sort((a, b) =>
                string.Compare(a.Nazwisko, b.Nazwisko, StringComparison.OrdinalIgnoreCase));
        }

        // zad 3e "przeciążona" wersja, korzysta z metody studenta
        public static void WypiszOsoby(this List<Student> studenci)
        {
            foreach (var s in studenci)
            {
                Console.WriteLine(s.WypiszPelnaNazweIUczelnie());
            }
        }
    }

    // interfejs student
    public interface IStudent : IOsoba
    {
        string Uczelnia { get; set; }
        string Kierunek { get; set; }
        int    Rok      { get; set; }
        int    Semestr  { get; set; }
    }

    public class Student : IStudent
    {
        public string Imie      { get; set; }
        public string Nazwisko  { get; set; }
        public string Uczelnia  { get; set; }
        public string Kierunek  { get; set; }
        public int    Rok       { get; set; }
        public int    Semestr   { get; set; }

        public Student(string imie, string nazwisko,
                       string uczelnia, string kierunek,
                       int rok, int semestr)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Uczelnia = uczelnia;
            Kierunek = kierunek;
            Rok = rok;
            Semestr = semestr;
        }

        public string ZwrocPelnaNazwe()
        {
            return $"{Imie} {Nazwisko}";
        }

        public string WypiszPelnaNazweIUczelnie()
        {
            // taki format jak w opisie (lekko dopasowany)
            return $"{Imie} {Nazwisko} – {Kierunek} {Rok} {Uczelnia}";
        }

        public override string ToString()
        {
            return WypiszPelnaNazweIUczelnie();
        }
    }

    public class StudentWSIiZ : Student
    {
        public StudentWSIiZ(string imie, string nazwisko,
                            string kierunek, int rok, int semestr)
            : base(imie, nazwisko, "WSIiZ", kierunek, rok, semestr)
        {
            // uczelnia ustawiona na sztywno
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // zad 3a kilka osob
            var osoby = new List<IOsoba>
            {
                new Osoba("Jan", "Kowalski"),
                new Osoba("Anna", "Nowak"),
                new Osoba("Piotr", "Zielinski")
            };

            Console.WriteLine("== osoby przed sortowaniem ==");
            osoby.WypiszOsoby();

            osoby.PosortujOsobyPoNazwisku();

            Console.WriteLine("\n== osoby po sortowaniu ==");
            osoby.WypiszOsoby();

            // zad 3d + 3e studenci WSIiZ
            var studenci = new List<Student>
            {
                new StudentWSIiZ("Jan", "Kowalski", "Informatyka", 4, 7),
                new StudentWSIiZ("Ola", "Lis", "Informatyka", 2, 3),
                new StudentWSIiZ("Kuba", "Nowak", "Informatyka", 1, 1)
            };

            Console.WriteLine("\n== studenci WSIiZ ==");
            studenci.WypiszOsoby(); // tutaj leci wersja z pelna nazwa + uczelnia
        }
    }
}
