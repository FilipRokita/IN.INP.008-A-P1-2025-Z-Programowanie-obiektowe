using System;
using System.Collections.Generic;

namespace Lab04School
{
    // baza: osoba
    public abstract class Osoba
    {
        private string firstName;
        private string lastName;
        private string pesel;

        public string FirstName => firstName;
        public string LastName  => lastName;
        public string Pesel     => pesel;

        public void SetFirstName(string name)
        {
            firstName = name;
        }

        public void SetLastName(string name)
        {
            lastName = name;
        }

        public void SetPesel(string peselValue)
        {
            // bez wielkiej walidacji, zakladamy ze pesel jest poprawny
            pesel = peselValue;
        }

        public int GetAge()
        {
            return GetAgeOn(DateTime.Today);
        }

        protected int GetAgeOn(DateTime date)
        {
            if (string.IsNullOrWhiteSpace(pesel) || pesel.Length < 6)
            {
                return 0;
            }

            int year  = int.Parse(pesel.Substring(0, 2));
            int month = int.Parse(pesel.Substring(2, 2));
            int day   = int.Parse(pesel.Substring(4, 2));

            int century;
            if (month >= 81 && month <= 92)
            {
                century = 1800;
                month -= 80;
            }
            else if (month >= 1 && month <= 12)
            {
                century = 1900;
            }
            else if (month >= 21 && month <= 32)
            {
                century = 2000;
                month -= 20;
            }
            else if (month >= 41 && month <= 52)
            {
                century = 2100;
                month -= 40;
            }
            else if (month >= 61 && month <= 72)
            {
                century = 2200;
                month -= 60;
            }
            else
            {
                // jak cos jest mocno nie tak
                return 0;
            }

            var birthDate = new DateTime(century + year, month, day);

            int age = date.Year - birthDate.Year;
            if (date < birthDate.AddYears(age))
            {
                age--;
            }

            return age;
        }

        public string GetGender()
        {
            // pozycja 10 w peselu 0-based index 9
            if (string.IsNullOrWhiteSpace(pesel) || pesel.Length < 10)
            {
                return "nieznana";
            }

            int digit = pesel[9] - '0';
            return digit % 2 == 0 ? "kobieta" : "mężczyzna";
        }

        // abstrakcyjne – dzieci muszą zaimplementowac
        public abstract string GetEducationInfo();
        public abstract string GetFullName();
        public abstract bool   CanGoAloneToHome();
    }

    // uczen
    public class Uczen : Osoba
    {
        public string Szkola { get; private set; }
        public bool MozeSamWracacDoDomu { get; private set; }

        public void SetSchool(string szkola)
        {
            Szkola = szkola;
        }

        public void ChangeSchool(string nowaSzkola)
        {
            Szkola = nowaSzkola;
        }

        public void SetCanGoHomeAlone(bool canGo)
        {
            MozeSamWracacDoDomu = canGo;
        }

        public override string GetEducationInfo()
        {
            return $"uczeń szkoły: {Szkola}";
        }

        public override string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public override bool CanGoAloneToHome()
        {
            int age = GetAge();

            // info z pdf: nie moze poniżej 12 lat, chyba że ma pozwolenie
            if (age < 12)
            {
                return MozeSamWracacDoDomu;
            }

            return true;
        }

        public override string ToString()
        {
            return $"{GetFullName()}, wiek {GetAge()}, szkola: {Szkola}, moze sam: {CanGoAloneToHome()}";
        }
    }

    // nauczyciel
    public class Nauczyciel : Uczen
    {
        public string TytulNaukowy { get; set; }

        public List<Uczen> PodwladniUczniowie { get; } = new List<Uczen>();

        public void WhichStudentCanGoHomeAlone(DateTime dateToCheck)
        {
            Console.WriteLine($"uczniowie, ktorzy moga isc sami do domu ({dateToCheck:yyyy-MM-dd}):");

            foreach (var uczen in PodwladniUczniowie)
            {
                // liczymy wiek na konkretny dzien
                int age = uczen is Osoba o ? o.GetAgeOn(dateToCheck) : 0;

                bool canGo;
                if (age < 12)
                {
                    canGo = uczen.MozeSamWracacDoDomu;
                }
                else
                {
                    canGo = true;
                }

                if (canGo)
                {
                    Console.WriteLine(uczen.GetFullName());
                }
            }
        }

        public override string ToString()
        {
            return $"{TytulNaukowy} {GetFullName()} – uczniow w klasie: {PodwladniUczniowie.Count}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // nauczyciel
            var n = new Nauczyciel
            {
                TytulNaukowy = "mgr"
            };
            n.SetFirstName("Anna");
            n.SetLastName("Kowalska");
            n.SetPesel("02210112345"); // przykladowy pesel

            // uczen 1 ma pozwolenie, wiek np. 11
            var u1 = new Uczen();
            u1.SetFirstName("Jan");
            u1.SetLastName("Nowak");
            u1.SetPesel("13210112346"); // pesel do testow
            u1.SetSchool("SP 1");
            u1.SetCanGoHomeAlone(true);

            // uczen 2 – brak pozwolenia
            var u2 = new Uczen();
            u2.SetFirstName("Ola");
            u2.SetLastName("Lis");
            u2.SetPesel("13210112340");
            u2.SetSchool("SP 1");
            u2.SetCanGoHomeAlone(false);

            n.PodwladniUczniowie.Add(u1);
            n.PodwladniUczniowie.Add(u2);

            Console.WriteLine(n);
            Console.WriteLine(u1);
            Console.WriteLine(u2);

            Console.WriteLine();
            n.WhichStudentCanGoHomeAlone(DateTime.Today);
        }
    }
}
