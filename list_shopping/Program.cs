var shopping_list = LoadFromFile("shopping_list.txt");
while (true)
{
    Console.WriteLine("Список покупок");
    Console.WriteLine("1. Показать список");
    Console.WriteLine("2. Добавить товар");
    Console.WriteLine("3. Удалить товар");
    Console.WriteLine("4. Сохранить и выйти");
    Console.Write("Выберите функцию: ");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            displayShoppingList(shopping_list);
            break;
        case "2":
            AddItem(shopping_list);
            break;
        case "3":
            RemoveItem(shopping_list);
            break;
        case "4":
            SaveToFile(shopping_list, "shopping_list.txt");
            Console.WriteLine("Список сохранен");
            return;
        default:
            Console.WriteLine("Такого выбора нет");
            break;
}
}


void displayShoppingList(List<(string Name, int Quantity)> list)
{
    Console.WriteLine("Список покупок:");
    for (int i = 0; i < list.Count; i++)
    {
        var (name, quantity) = list[i];
        Console.WriteLine($"{i + 1}. {name} (x{quantity})");
    }
    Console.WriteLine("Нажмите любую кнопку для возврата в меню");
    Console.ReadKey();
}

void AddItem(List<(string Name, int Quantity)> list)
{
    Console.WriteLine("Какой товар добавить? ");
    string productName = Console.ReadLine();
    Console.WriteLine("Введите количество: ");
    string ProductCount = Console.ReadLine();
    if (int.TryParse(ProductCount, out int quantity))
    {
        list.Add((productName, quantity));
    }
    else
    {
        Console.WriteLine("неверное число");
    }
}

void RemoveItem(List<(string Name, int Quantity)> list)
{
    Console.WriteLine("Введите индекс товара для удаления ");
    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= list.Count)
    {
        list.RemoveAt(index - 1);
        Console.WriteLine("Товар Удален");
    }
    else
    {
        Console.WriteLine("Товар с таким индексом не найден");
    }
}

void SaveToFile(List<(string Name, int Quantity)> list, string FileName)
{
    File.WriteAllLines(FileName, list.ConvertAll(item=>$"{item.Name} - {item.Quantity}шт."));
}

List<(string Name, int Quantity)> LoadFromFile(string FileName)
{
    var list = new List<(string Name, int Quantity)>();
    if (File.Exists(FileName))
    {
        foreach (var line in File.ReadLines(FileName))
        {
            var parts = line.Split(",");
            if (parts.Length == 2 && int.TryParse(parts[1], out int quantity))
            {
                list.Add((parts[0], quantity));
            }
        }
    }
    return list;
}
