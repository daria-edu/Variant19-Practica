using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace InvestmentPortfolioApp
{
    public class InvestmentLinkedList
    {
        private InvestmentNode? head;
        private InvestmentNode? tail;
        private InvestmentNode? current;

        private int count;

        public int Length
        {
            get { return count; }
        }

        public InvestmentPortfolio this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException("Невірний індекс.");

                InvestmentNode temp = head!;

                for (int i = 0; i < index; i++)
                    temp = temp.Next!;

                return temp.Data;
            }

            set
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException("Невірний індекс.");

                InvestmentNode temp = head!;

                for (int i = 0; i < index; i++)
                    temp = temp.Next!;

                temp.Data = value;
            }
        }

        public void AddMiddle(InvestmentPortfolio data)
        {
            InvestmentNode newNode = new InvestmentNode(data);

            if (head == null)
            {
                head = tail = newNode;
                count++;
                return;
            }

            int middle = count / 2;

            InvestmentNode currentNode = head;

            for (int i = 0; i < middle; i++)
                currentNode = currentNode.Next!;

            newNode.Prev = currentNode.Prev;
            newNode.Next = currentNode;

            if (currentNode.Prev != null)
                currentNode.Prev.Next = newNode;
            else
                head = newNode;

            currentNode.Prev = newNode;

            count++;
        }

        public void RemoveLast()
        {
            if (head == null)
                throw new InvalidOperationException("Список порожній.");

            if (head == tail)
            {
                head = null;
                tail = null;
                count = 0;
                return;
            }

            tail = tail!.Prev;
            tail!.Next = null;

            count--;
        }

        public InvestmentPortfolio GetLast()
        {
            if (tail == null)
                throw new InvalidOperationException("Список порожній.");

            current = tail;
            return current.Data;
        }

        public InvestmentPortfolio? GetPrevious()
        {
            if (current == null || current.Prev == null)
                return null;

            current = current.Prev;

            return current.Data;
        }

        public InvestmentLinkedList GetLastNElements(int n)
        {
            if (n <= 0)
                throw new ArgumentException("N повинно бути більше нуля.");

            if (n > count)
                throw new ArgumentException("N більше довжини списку.");

            InvestmentLinkedList result = new InvestmentLinkedList();

            InvestmentNode? temp = tail;

            List<InvestmentPortfolio> items = new();

            for (int i = 0; i < n; i++)
            {
                items.Add(temp!.Data);
                temp = temp.Prev;
            }

            items.Reverse();

            foreach (InvestmentPortfolio item in items)
                result.AddMiddle(item);

            return result;
        }

        public List<InvestmentPortfolio> Search()
        {
            List<InvestmentPortfolio> result = new();

            InvestmentNode? temp = head;

            while (temp != null)
            {
                if (temp.Data.InvestmentType == InvestmentType.Bonds &&
                    temp.Data.ReturnRate > 3.5 &&
                    !temp.Data.HighRisk)
                {
                    result.Add(temp.Data);
                }

                temp = temp.Next;
            }

            return result;
        }

        public void Display()
        {
            if (head == null)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine($"{"№",-5}{"Тип",-20}{"Прибутковість",-18}{"Ризик"}");
            Console.WriteLine("------------------------------------------------------");

            InvestmentNode? temp = head;
            int index = 0;

            while (temp != null)
            {
                Console.WriteLine(
                    $"{index,-5}" +
                    $"{temp.Data.InvestmentType,-20}" +
                    $"{temp.Data.ReturnRate,-18}" +
                    $"{temp.Data.HighRisk}");

                temp = temp.Next;
                index++;
            }

            Console.WriteLine("------------------------------------------------------");
        }

        public void SaveToFile(string fileName)
        {
            List<InvestmentPortfolio> items = new();

            InvestmentNode? temp = head;

            while (temp != null)
            {
                items.Add(temp.Data);
                temp = temp.Next;
            }

            string json = JsonSerializer.Serialize(
                items,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(fileName, json);
        }

        public void LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Файл не знайдено.");

            string json = File.ReadAllText(fileName);

            List<InvestmentPortfolio>? items =
                JsonSerializer.Deserialize<List<InvestmentPortfolio>>(json);

            if (items == null)
                return;

            head = null;
            tail = null;
            count = 0;

            foreach (InvestmentPortfolio item in items)
            {
                AddToEnd(item);
            }
        }

        private void AddToEnd(InvestmentPortfolio data)
        {
            InvestmentNode newNode = new InvestmentNode(data);

            if (head == null)
            {
                head = tail = newNode;
            }
            else
            {
                tail!.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }

            count++;
        }
    }
}