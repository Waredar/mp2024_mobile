using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace cnsObservableCollection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cities = new ObservableCollection<string>();
            cities.Add("Москва");
            cities.CollectionChanged += Cities_CollectionChanged;

            Console.WriteLine("Команды:");
            Console.WriteLine("?                - Показать все города");
            Console.WriteLine("+ <Новый город>  - Добавит город");
            Console.WriteLine("- <Старый город> - Удалить город");
            Console.WriteLine("[Enter]          - выход");

            while (true)
            {
                var newLine = Console.ReadLine();
                if (newLine == string.Empty)
                {
                    break;
                }

                switch (newLine)
                {
                    case "?":
                        Console.Write($"Города ({cities.Count}) : ");
                        Console.WriteLine(String.Join(", ", cities));
                        break;
                    default:
                        var cmd = newLine.Split(' ');
                        switch (cmd[0])
                        {
                            case "+":
                                if (cmd.Length == 2)
                                    cities.Add(cmd[1]);
                                break;

                            case "-":
                                if (cmd.Length == 2)
                                    cities.Remove(cmd[1]);
                                break;

                        }
                        break;
                }

            }
        }

        private static void Cities_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    console.WriteLine($"Добавлен город [{sender.ToString()}]");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
        }
    }
}
