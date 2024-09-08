internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Введите имя:");
        string? name = Console.ReadLine();

        Console.WriteLine("Введите город:");
        string? city = Console.ReadLine();


        Console.WriteLine("Имя = " + name + ", Город = " + city);
        Console.WriteLine("Имя =  {0}, Город = {1}", name, city);
        Console.WriteLine($"Имя =  {name}, Город = {city}");


    }
}