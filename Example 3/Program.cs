namespace Example_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number1 = 400;
            int number2 = 85;
            int answer = GCD(number1, number2);
            Console.WriteLine($"The GCD of {number1} and {number2} is {answer}");
            Console.ReadLine();
        }

        static int GCD(int n1, int n2)
        {
            if (n2 == 0)
            {
                return n1;
            }
            else
            {
                {
                    Console.Out.WriteLine($"Not yet. Remainder is {n1 % n2}");
                    return GCD(n2, n1 % n2);
                }
            }
        }
    }
}
