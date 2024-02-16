using System.Text.RegularExpressions;

namespace WordCounter;

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("Enter path of input file");
        var inputFilePath = Console.ReadLine();

        Console.WriteLine("Enter path of output file");
        var outputFilePath = Console.ReadLine();

        try
        {
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("Input file does't exist");
                return;
            }

            Dictionary<string, int> wordCount = CountWords(inputFilePath);
            WriteCountOfWordsToFile(wordCount, outputFilePath);
            Console.WriteLine("Words count successfully, see results in file");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    static Dictionary<string, int> CountWords(string filePath)
    {
        var wordCount = new Dictionary<string, int>();

        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] words = Regex.Split(line, @"\W+");
                foreach (string word in words)
                {
                    if (!string.IsNullOrWhiteSpace(word))
                    {
                        string lowercaseWord = word.ToLower();
                        if (wordCount.ContainsKey(lowercaseWord))
                        {
                            wordCount[lowercaseWord]++;
                        }
                        else
                        {
                            wordCount[lowercaseWord] = 1;
                        }
                    }
                }
            }
        }

        return wordCount;
    }

    static void WriteCountOfWordsToFile(Dictionary<string, int> wordCount, string outPath)
    {
        using (var writer = new StreamWriter(outPath))
        {
            foreach (var entry in wordCount.OrderByDescending(pair => pair.Value))
            {
                writer.WriteLine($"{entry.Key}\t\t{entry.Value}");
            }
        }
    }
}