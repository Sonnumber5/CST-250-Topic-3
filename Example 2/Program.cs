namespace Example_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int StartingNumber = 6;
            Console.Out.WriteLine(factorial(StartingNumber));
            Console.ReadLine();
        }

        static int factorial(int x)
        {
            Console.Out.WriteLine($"X is {x}");
            if (x == 1)
            {
                return 1;
            }
            else
            {
                return x * factorial(x - 1);
            }
        }
    }
}
