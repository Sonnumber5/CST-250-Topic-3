namespace Example_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter an integer. I will do some math and eventually arrive at 1");
            int startingNumber = int.Parse(Console.ReadLine());
            int x = CountToOne(startingNumber);
            Console.ReadLine();
        }

        public static int CountToOne(int n)
        {
            Console.Out.WriteLine($"N is {n}");
            if (n == 1)
            {
                return 1;
            }
            else
            {
                if (n % 2 == 0)
                {
                    Console.Out.WriteLine("N is even. Divide by 2");
                    return CountToOne(n / 2);
                }
                else
                {
                    Console.Out.WriteLine("N is odd. Add 1");
                    return CountToOne(n + 1);
                }
            }
        }
    }
}
