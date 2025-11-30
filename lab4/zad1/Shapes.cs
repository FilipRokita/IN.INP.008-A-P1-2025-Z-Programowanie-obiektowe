using System;
using System.Collections.Generic;

namespace Lab04Shapes
{
    public class Shape
    {
        public int X      { get; set; }
        public int Y      { get; set; }
        public int Height { get; set; }
        public int Width  { get; set; }

        public Shape(int x, int y, int height, int width)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
        }

        // virtual dzieci beda nadpisywac
        public virtual void Draw()
        {
            Console.WriteLine($"rysuje ogólna figure na ({X}, {Y})");
        }
    }

    public class Rectangle : Shape
    {
        public Rectangle(int x, int y, int height, int width)
            : base(x, y, height, width)
        {
        }

        public override void Draw()
        {
            Console.WriteLine($"rysuje prostokat na ({X}, {Y}), h={Height}, w={Width}");
        }
    }

    public class Triangle : Shape
    {
        public Triangle(int x, int y, int height, int width)
            : base(x, y, height, width)
        {
        }

        public override void Draw()
        {
            Console.WriteLine($"rysuje trojkat na ({X}, {Y}), h={Height}, w={Width}");
        }
    }

    public class Circle : Shape
    {
        public Circle(int x, int y, int radius)
            : base(x, y, radius * 2, radius * 2)
        {
        }

        public override void Draw()
        {
            Console.WriteLine($"rysuje kolo na ({X}, {Y}), promien={Height / 2}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var shapes = new List<Shape>
            {
                new Rectangle(0, 0, 10, 20),
                new Triangle(5, 5, 8, 12),
                new Circle(10, 10, 6)
            };

            foreach (var s in shapes)
            {
                s.Draw();
            }
        }
    }
}
