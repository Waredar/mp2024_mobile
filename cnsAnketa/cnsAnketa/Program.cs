string question(string str)
{
    Console.WriteLine(str);
    Console.Write("> ");
    string? buff = Console.ReadLine();
    while (string.IsNullOrEmpty(buff.Replace(" ", "")))
    {
        Console.WriteLine("Вы не ответили на вопрос. " + str);
        Console.Write("> ");
        buff = Console.ReadLine();
    }
    return buff;
}
string getMonthFromNumber(int number)
{
    switch (number)
    {
        case 1: return "Январь";
        case 2: return "Февраль";
        case 3: return "Март";
        case 4: return "Апрель";
        case 5: return "Май";
        case 6: return "Июнь";
        case 7: return "Июль";
        case 8: return "Август";
        case 9: return "Сентябрь";
        case 10: return "Октябрь";
        case 11: return "Ноябрь";
        case 12: return "Декабрь";
        default: return "Невозможно";
    }
}
void DrawMenu(string[] items, int row, int col, int index)
{
    Console.SetCursorPosition(col, row);
    for (int i = 0; i < items.Length; i++)
    {
        if (i == index)
        {
            Console.BackgroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        Console.WriteLine(items[i]);
        Console.ResetColor();
    }
    Console.WriteLine();
}

string firstName = question("Введите имя:");

string lastName = question("Введите фамилию:");

int[] date;
while (true)
{
    string dateStr = question("Введите дату рождения в формате dd-mm-yyyy:");
    string[] dateArray = dateStr.Split('-');
    date = new int[dateArray.Length];
    for (int i = 0; i < dateArray.Length; i++)
        date[i] = int.Parse(dateArray[i]);
    if (date.Length != 3)
        Console.Write("Неверная дата. ");
    else if (date[0] < 1 | date[0] > 31 | date[1] < 1 | date[1] > 12 | date[2] < 1900 | date[2] > 2024)
        Console.Write("Неверная дата. ");
    else
        break;
}

string city = question("Введите город проживания:");

string group = question("Введите номер группы:");

string semestrStr = question("Введите номер семестра:");
int semestr = int.Parse(semestrStr);

string answer1 = question("Чему равна разность квадратов (a^2 - b^2)");
if (answer1 != "(a-b)(a+b)" & answer1 != "(a+b)(a-b)")
{
    Console.WriteLine("НЕУД ПО МАТЕМАТИКЕ");
    answer1 += " (Неуд)";
}
else answer1 += " (Хорош)";

string answer2 = question("Каких камней нет ни в одном море?");
if (answer2.ToLower() != "сухих")
{
    Console.WriteLine("НЕУД ПО ЛОГИКЕ");
    answer2 += " (Неуд)";
}
else answer2 += " (Хорош)";

string answer3 = question("Горело 5 электрических лампочек, три лампочки выключили. Сколько лампочек осталось?");
int intAnswer3 = int.Parse(answer3);
if (intAnswer3 != 5)
{
    Console.WriteLine("НЕУД ПО ДЕТСКИМ ЗАДАЧАМ");
    answer3 += " (Неуд)";
}
else answer3 += " (Хорош)";

Console.Clear();
Console.WriteLine("Анкета:\n");
Console.WriteLine($"Фамилия: {lastName}");
Console.WriteLine($"Имя: {firstName}");
Console.WriteLine($"Дата рождения: {date[0]} {getMonthFromNumber(date[1])} {date[2]} г.");
Console.WriteLine($"Город: {city}");
Console.WriteLine($"Номер группы: {group}");
Console.WriteLine($"Номер семестра: {semestr}");
Console.WriteLine($"Ответ на вопрос 1: {answer1}");
Console.WriteLine($"Ответ на вопрос 2: {answer2}");
Console.WriteLine($"Ответ на вопрос 3: {answer3}");
Console.WriteLine();
Console.WriteLine("Сохраняем?");
Console.WriteLine();
string[] menuItems = ["Да", "Нет"];
int row = Console.CursorTop;
int col = Console.CursorLeft;
int index = 0;
while (true)
{
    DrawMenu(menuItems, row, col, index);
    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.DownArrow:
            if (index < menuItems.Length)
                index++;
            break;
        case ConsoleKey.UpArrow:
            if (index > 0)
                index--;
            break;
        case ConsoleKey.Enter:
            if (index == 0)
            {
                StreamWriter streamWriter = new StreamWriter($"..\\..\\..\\..\\{lastName} {firstName}.txt", false);
                streamWriter.WriteLine("Анкета:\n");
                streamWriter.WriteLine($"Фамилия: {lastName}");
                streamWriter.WriteLine($"Имя: {firstName}");
                streamWriter.WriteLine($"Дата рождения: {date[0]} {getMonthFromNumber(date[1])} {date[2]} г.");
                streamWriter.WriteLine($"Город: {city}");
                streamWriter.WriteLine($"Номер группы: {group}");
                streamWriter.WriteLine($"Номер семестра: {semestr}");
                streamWriter.WriteLine($"Ответ на вопрос 1: {answer1}");
                streamWriter.WriteLine($"Ответ на вопрос 2: {answer2}");
                streamWriter.WriteLine($"Ответ на вопрос 3: {answer3}");
                streamWriter.Close();
            }
            Console.WriteLine("Выход из приложения");
            return;
    }
}
