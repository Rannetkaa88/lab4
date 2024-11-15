using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System;

public class CombinedPrograms
{
    // Задание 1
    public static void ReverseList()
    {
        List<string> list = new List<string> { "1", "2", "3", "4", "5" };

        Console.WriteLine("Исходный список:");
        foreach (var item in list)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        list.Reverse();

        Console.WriteLine("Перевёрнутый список:");
        foreach (var item in list)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    // Задание 2
    public static void InsertAround<T>(LinkedList<T> list, T elementE, T elementF)
    {
        if (list == null) return;
        LinkedListNode<T> node = list.Find(elementE);
        if (node == null) return;
        list.AddBefore(node, elementF);
        list.AddAfter(node, elementF);
    }

    public static void LinkedListInsertDemo()
    {
        LinkedList<string> list = new LinkedList<string>(new[] { "1", "2", "3", "4", "5" });
        Console.WriteLine("Исходный список:");
        foreach (var item in list)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
        string elementE = "3";
        string elementF = "10";
        InsertAround(list, elementE, elementF);
        Console.WriteLine($"Список после вставки {elementF} вокруг {elementE}:");
        foreach (var item in list)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    // Задание 3
    public static HashSet<string> GetDiscosVisitedByAll(List<HashSet<string>> studentDiscos)
    {
        if (studentDiscos.Count == 0) return new HashSet<string>();
        HashSet<string> discosVisitedByAll = new HashSet<string>(studentDiscos[0]);
        foreach (var discos in studentDiscos)
        {
            discosVisitedByAll.IntersectWith(discos);
        }
        return discosVisitedByAll;
    }

    public static HashSet<string> GetDiscosVisitedBySome(List<HashSet<string>> studentDiscos)
    {
        HashSet<string> discosVisitedBySome = new HashSet<string>();
        foreach (var discos in studentDiscos)
        {
            discosVisitedBySome.UnionWith(discos);
        }
        return discosVisitedBySome;
    }

    public static HashSet<string> GetDiscosVisitedByNone(HashSet<string> allDiscos, HashSet<string> discosVisitedBySome)
    {
        HashSet<string> discosVisitedByNone = new HashSet<string>(allDiscos);
        discosVisitedByNone.ExceptWith(discosVisitedBySome);
        return discosVisitedByNone;
    }

    public static void DiscoVisitAnalyzerDemo()
    {
        HashSet<string> allDiscos = new HashSet<string> { "Disco1", "Disco2", "Disco3", "Disco4", "Disco5", "Disco6" };
        List<HashSet<string>> studentDiscos = new List<HashSet<string>>
        {
            new HashSet<string> { "Disco1", "Disco2", "Disco3" },
            new HashSet<string> { "Disco2", "Disco3", "Disco4" },
            new HashSet<string> { "Disco1", "Disco3", "Disco5" }
        };
        HashSet<string> discosVisitedByAll = GetDiscosVisitedByAll(studentDiscos);
        Console.WriteLine("Дискотеки, которые посещали все студенты:");
        Console.WriteLine(string.Join(", ", discosVisitedByAll));
        HashSet<string> discosVisitedBySome = GetDiscosVisitedBySome(studentDiscos);
        Console.WriteLine("\nДискотеки, которые посещали некоторые студенты:");
        Console.WriteLine(string.Join(", ", discosVisitedBySome));
        HashSet<string> discosVisitedByNone = GetDiscosVisitedByNone(allDiscos, discosVisitedBySome);
        Console.WriteLine("\nДискотеки, которые никто не посещал:");
        Console.WriteLine(string.Join(", ", discosVisitedByNone));
    }

    // Задание 4
    public static HashSet<char> GetUniqueCharsFromEvenWords(string text)
    {
        HashSet<char> uniqueChars = new HashSet<char>();
        var words = text.Split(new char[] { ' ', '\t', '\n', '\r', '.', ',', '!', '?', ':', ';', '-', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 1; i < words.Length; i += 2)
        {
            foreach (char c in words[i])
            {
                if (char.IsLetter(c))
                {
                    uniqueChars.Add(char.ToLower(c));
                }
            }
        }
        return uniqueChars;
    }

    public static void PrintCharsInAlphabeticalOrder(HashSet<char> chars)
    {
        var sortedChars = chars.OrderBy(c => c).ToList();
        Console.WriteLine("Уникальные символы из чётных слов в алфавитном порядке:");
        foreach (var c in sortedChars)
        {
            Console.Write(c + " ");
        }
        Console.WriteLine();
    }

    public static void TextProcessorDemo()
    {
        string filePath = "input.txt";
        string text = "";
        try
        {
            text = File.ReadAllText(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            return;
        }
        HashSet<char> uniqueChars = GetUniqueCharsFromEvenWords(text);
        PrintCharsInAlphabeticalOrder(uniqueChars);
    }

    // Задание 5
    [Serializable]
    public class Applicant
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }
        public int Score3 { get; set; }
    }

    private static readonly Random random = new Random();

    public static void GenerateApplicantData(string filePath, int numApplicants)
    {
        List<Applicant> applicants = new List<Applicant>();
        string[] lastNames = { "Иванов", "Петров", "Сидоров", "Кузнецов", "Смирнов", "Попов", "Васильев" };
        string[] firstNames = { "Иван", "Петр", "Сергей", "Алексей", "Александр", "Дмитрий" };
        for (int i = 0; i < numApplicants; i++)
        {
            Applicant applicant = new Applicant();
            applicant.LastName = lastNames[random.Next(lastNames.Length)];
            applicant.FirstName = firstNames[random.Next(firstNames.Length)];
            applicant.Score1 = random.Next(0, 101);
            applicant.Score2 = random.Next(0, 101);
            applicant.Score3 = random.Next(0, 101);
            applicants.Add(applicant);
        }
        XmlSerializer serializer = new XmlSerializer(typeof(List<Applicant>));
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(fileStream, applicants);
        }
    }

    public static List<Applicant> ReadApplicantData(string filePath)
    {
        List<Applicant> applicants = null;
        XmlSerializer serializer = new XmlSerializer(typeof(List<Applicant>));
        try
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                applicants = (List<Applicant>)serializer.Deserialize(fileStream);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
        }
        return applicants;
    }

    public static void AdmissionProgramDemo()
    {
        string filePath = "applicants.xml";
        int numApplicants = 10;

        // Генерация данных и запись в XML-файл
        GenerateApplicantData(filePath, numApplicants);

        // Чтение данных из XML-файла
        List<Applicant> applicants = ReadApplicantData(filePath);

        if (applicants != null)
        {
            // Отбор абитуриентов, допущенных к экзаменам
            var admittedApplicants = applicants
                .Where(a => a.Score1 >= 30 && a.Score2 >= 30 && a.Score3 >= 30 && a.Score1 + a.Score2 + a.Score3 >= 140)
                .OrderBy(a => a.LastName);

            Console.WriteLine("Абитуриенты, допущенные к экзаменам (с проверкой длины имени и фамилии):");

            foreach (var applicant in admittedApplicants)
            {
                // Проверка длины фамилии и имени
                if (applicant.LastName.Length <= 20 && applicant.FirstName.Length <= 15)
                {
                    Console.WriteLine($"{applicant.LastName} {applicant.FirstName}");
                }
                else
                {
                    Console.WriteLine($"{applicant.LastName} {applicant.FirstName} – Пропущен: несоответствие длины имени или фамилии");
                }
            }
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Результат выполнения программы 1:");
        ReverseList();
        Console.WriteLine("\n----------------------------------------\n");

        Console.WriteLine("Результат выполнения программы 2:");
        LinkedListInsertDemo();
        Console.WriteLine("\n----------------------------------------\n");

        Console.WriteLine("Результат выполнения программы 3:");
        DiscoVisitAnalyzerDemo();
        Console.WriteLine("\n----------------------------------------\n");

        Console.WriteLine("Результат выполнения программы 4:");
        TextProcessorDemo();
        Console.WriteLine("\n----------------------------------------\n");

        Console.WriteLine("Результат выполнения программы 5:");
        AdmissionProgramDemo();
        Console.WriteLine("\n----------------------------------------\n");

        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}
