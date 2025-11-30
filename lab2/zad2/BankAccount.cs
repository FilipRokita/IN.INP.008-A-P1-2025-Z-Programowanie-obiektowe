using System;

namespace Lab02Zad2
{
    internal class BankAccount
    {
        // trzymamy saldo prywatnie, zeby nie dało sie tego zmieniać z zewnatrz
        private decimal saldo;

        public string Wlasciciel { get; set; }

        public decimal Saldo
        {
            get => saldo; // tylko odczyt na zewnatrz
            private set => saldo = value;
        }

        public BankAccount(string wlasciciel, decimal saldoPoczatkowe)
        {
            if (string.IsNullOrWhiteSpace(wlasciciel))
            {
                throw new ArgumentException("Właściciel nie może być pusty.");
            }

            if (saldoPoczatkowe < 0)
            {
                throw new ArgumentException("Saldo początkowe nie może być ujemne.");
            }

            Wlasciciel = wlasciciel;
            Saldo = saldoPoczatkowe;
        }

        public void Wplata(decimal kwota)
        {
            // prosta walidacja, zeby nie wplacac bzdur
            if (kwota <= 0)
            {
                throw new ArgumentException("Kwota wpłaty musi być dodatnia.");
            }

            Saldo += kwota;
        }

        public void Wyplata(decimal kwota)
        {
            if (kwota <= 0)
            {
                throw new ArgumentException("Kwota wypłaty musi być dodatnia.");
            }

            if (kwota > Saldo)
            {
                throw new InvalidOperationException("Brak wystarczających środków na koncie.");
            }

            Saldo -= kwota;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // przykład z treści zadania
            BankAccount konto = new BankAccount("Jan Kowalski", 1000);
            konto.Wplata(500);
            konto.Wyplata(200);
            Console.WriteLine($"Saldo: {konto.Saldo}");
        }
    }
}
