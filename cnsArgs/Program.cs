namespace cnsArgs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Параметры");
            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }
        }
    }
}
