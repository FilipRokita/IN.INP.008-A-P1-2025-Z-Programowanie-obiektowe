using System;

namespace Lab01Zad1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ObliczTrojmian();
        }

        static void ObliczTrojmian()
        {
            Console.WriteLine("trójmian kwadratowy: a*x^2 + b*x + c");

            Console.Write("podaj a: ");
            double a = Convert.ToDouble(Console.ReadLine());

            Console.Write("podaj b: ");
            double b = Convert.ToDouble(Console.ReadLine());

            Console.Write("podaj c: ");
            double c = Convert.ToDouble(Console.ReadLine());

            if (a == 0)
            {
                Console.WriteLine("to nie jest równanie kwadratowe (a = 0)");
                return;
            }

            double delta = (b * b) - (4 * a * c);
            Console.WriteLine($"delta = {delta}");

            if (delta > 0)
            {
                double sqrtDelta = Math.Sqrt(delta);
                double x1 = (-b - sqrtDelta) / (2 * a);
                double x2 = (-b + sqrtDelta) / (2 * a);
                Console.WriteLine($"dwa pierwiastki rzeczywiste: x1 = {x1}, x2 = {x2}");
            }
            else if (delta == 0)
            {
                double x = -b / (2 * a);
                Console.WriteLine($"jeden pierwiastek podwójny: x = {x}");
            }
            else
            {
                Console.WriteLine("brak pierwiastków rzeczywistych");
            }
        }
    }
}
