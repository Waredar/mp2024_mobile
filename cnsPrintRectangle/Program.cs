namespace cnsPrintRectangle
{
    internal class Program
    {
        /// <summary>
        /// Нарисовать прямоугольник
        /// </summary>

        static string[] GetRectangle(
            int width,
            int height,
            char symbol = '*',
            bool isFill = true,
            char symbolClear = ' ')
        {
            List<string> result = new();

            for (int r = 0; r < height; r++)
            {
                if (isFill || r == 0 || r == height - 1)
                    result.Add(new string(symbol, width));
                else
                    result.Add(symbol + new string(symbolClear, width - 2) + symbol);
            }

            return result.ToArray();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Ширина фигуры?");
            int w = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Высота фигуры?");
            int h = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Символ рисования?");
            char c = Convert.ToChar(Console.ReadLine());
            Console.WriteLine("Заполнить фигуру?(y/n)");
            bool f = Console.ReadLine()?.ToLower() == "y";

            var shape = GetRectangle(w, h, c, f);
            Console.WriteLine(String.Join(Environment.NewLine, shape));
        }
    }
}
