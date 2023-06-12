using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HuffmanTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Podaj ścieżkę do pliku tekstowego:");
            string filePath = Console.ReadLine();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Podany plik nie istnieje.");
                return;
            }

            Console.WriteLine("Podaj ścieżkę do pliku, w którym chcesz zapisać skompresowany tekst:");
            string outputFilePath = Console.ReadLine();            

            string input = File.ReadAllText(filePath);
            HuffmanTree huffmanTree = new HuffmanTree();

            // Tworzenie drzewa Huffmana
            huffmanTree.Build(input);

            // Kompresja
            BitArray encoded = huffmanTree.Encode(input);
            string result = ToBitString(encoded);
            File.WriteAllText(outputFilePath, result);

            Console.WriteLine();

            // Dekompresja
            Console.WriteLine("Podaj ścieżkę do pliku, w którym chcesz zapisać zdekompresowany tekst:");
            outputFilePath = Console.ReadLine();

            string decoded = huffmanTree.Decode(encoded);
            File.WriteAllText(outputFilePath, decoded);

            Console.ReadLine();
        }
        public static string ToBitString(BitArray bits)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < bits.Count; i++)
            {
                char c = bits[i] ? '1' : '0';
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}