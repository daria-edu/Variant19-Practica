using System;
using System.Collections.Generic;

namespace InvestmentPortfolioApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InvestmentLinkedList list = new InvestmentLinkedList();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("========== МЕНЮ ==========");
                Console.WriteLine("1. Додати інвестицію");
                Console.WriteLine("2. Видалити останню інвестицію");
                Console.WriteLine("3. Показати список");
                Console.WriteLine("4. Пошук облігацій");
                Console.WriteLine("5. Отримати останні N елементів");
                Console.WriteLine("6. Зберегти у файл");
                Console.WriteLine("7. Завантажити з файлу");
                Console.WriteLine("8. Змінити елемент за індексом");
                Console.WriteLine("9. Обхід списку з кінця");
                Console.WriteLine("0. Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddInvestment(list);
                            list.Display();
                            break;

                        case "2":
                            list.RemoveLast();
                            Console.WriteLine("Останню інвестицію видалено.");
                            list.Display();
                            break;

                        case "3":
                            list.Display();
                            break;

                        case "4":
                            SearchInvestments(list);
                            break;

                        case "5":
                            GetLastNElements(list);
                            break;

                        case "6":
                            Console.Write("Введіть ім'я файлу: ");
                            string saveFile = Console.ReadLine();

                            list.SaveToFile(saveFile);

                            Console.WriteLine("Дані успішно збережено.");
                            break;

                        case "7":
                            Console.Write("Введіть ім'я файлу: ");
                            string loadFile = Console.ReadLine();

                            list.LoadFromFile(loadFile);

                            Console.WriteLine("Дані успішно завантажено.");
                            list.Display();
                            break;

                        case "8":
                            ChangeElement(list);
                            list.Display();
                            break;

                        case "9":
                            TraverseFromEnd(list);
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Невірний вибір.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
            }
        }

        static void AddInvestment(InvestmentLinkedList list)
        {
            Console.WriteLine();
            Console.WriteLine("Оберіть тип інвестиції:");
            Console.WriteLine("0 - Акції");
            Console.WriteLine("1 - Облігації");
            Console.WriteLine("2 - Нерухомість");
            Console.WriteLine("3 - Криптовалюта");
            Console.Write("Ваш вибір: ");

            int typeValue = int.Parse(Console.ReadLine());

            InvestmentType type = (InvestmentType)typeValue;

            Console.Write("Введіть прибутковість (%): ");
            double returnRate = double.Parse(Console.ReadLine());

            Console.Write("Високий ризик (true/false): ");
            bool highRisk = bool.Parse(Console.ReadLine());

            InvestmentPortfolio portfolio =
                new InvestmentPortfolio(
                    type,
                    returnRate,
                    highRisk);

            list.AddMiddle(portfolio);

            Console.WriteLine("Інвестицію успішно додано.");
        }

        static void SearchInvestments(InvestmentLinkedList list)
        {
            List<InvestmentPortfolio> result = list.Search();

            if (result.Count == 0)
            {
                Console.WriteLine("Нічого не знайдено.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Результати пошуку:");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"{"Тип",-20}{"Прибутковість",-18}{"Ризик"}");
            Console.WriteLine("--------------------------------------------------");

            foreach (InvestmentPortfolio item in result)
            {
                Console.WriteLine(
                    $"{item.InvestmentType,-20}" +
                    $"{item.ReturnRate,-18}" +
                    $"{item.HighRisk}");
            }

            Console.WriteLine("--------------------------------------------------");
        }

        static void GetLastNElements(InvestmentLinkedList list)
        {
            Console.Write("Введіть N: ");

            int n = int.Parse(Console.ReadLine());

            InvestmentLinkedList newList =
                list.GetLastNElements(n);

            Console.WriteLine();
            Console.WriteLine("Новий список:");

            newList.Display();
        }

        static void ChangeElement(InvestmentLinkedList list)
        {
            Console.Write("Введіть індекс: ");
            int index = int.Parse(Console.ReadLine());

            Console.WriteLine("Оберіть тип інвестиції:");
            Console.WriteLine("0 - Акції");
            Console.WriteLine("1 - Облігації");
            Console.WriteLine("2 - Нерухомість");
            Console.WriteLine("3 - Криптовалюта");
            Console.Write("Ваш вибір: ");

            int typeValue = int.Parse(Console.ReadLine());

            InvestmentType type = (InvestmentType)typeValue;

            Console.Write("Введіть прибутковість (%): ");
            double returnRate = double.Parse(Console.ReadLine());

            Console.Write("Високий ризик (true/false): ");
            bool highRisk = bool.Parse(Console.ReadLine());

            list[index] = new InvestmentPortfolio(
                type,
                returnRate,
                highRisk);

            Console.WriteLine("Елемент успішно змінено.");
        }

        static void TraverseFromEnd(InvestmentLinkedList list)
        {
            InvestmentPortfolio item = list.GetLast();

            Console.WriteLine();
            Console.WriteLine("Обхід списку з кінця:");

            while (item != null)
            {
                Console.WriteLine(
                    $"{item.InvestmentType} | " +
                    $"{item.ReturnRate} | " +
                    $"{item.HighRisk}");

                item = list.GetPrevious();
            }
        }
    }
}