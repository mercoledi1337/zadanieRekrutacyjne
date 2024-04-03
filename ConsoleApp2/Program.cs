using System;

namespace MyApp // Note: actual namespace depends on the project name.
{



    public delegate int Function(int x);

    public static class SomeClass
    {
        public static int Sum1(int value) => value + 1;

        public static int Sum2(int value) => value + 2;

        public static int Sum3(int value) => value + 3;
    }


    public class Program
    {


        public delegate int Transformer(int x);

        public static class SomeClass
        {
            public static int Square(int x) => x * x;

            public static int Cube(int x) => x * x * x;
        }
        public delegate void Greeting();
        static void Main()
        {
            Func<int, double> expression1 = x => x / 2;
            Func<double, double> expression2 = x => x / 2;
            Func<double, double> expression3 = (double x) => x / 2;
            Func<double, int> expression4 = (double x) => (int)(x / 2);

            Console.WriteLine($"{expression1(9)}, {expression2(9.0)}, {expression3(9)}, {expression4(9)}");
        }

    }
}