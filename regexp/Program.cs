using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace regexp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter input file name");
            string file = Console.ReadLine();
            try
            {
                using (StreamReader str = new StreamReader(@file))
                {
                    Dictionary<char, int> dictionary = new Dictionary<char, int>
                        {{'I', 1}, {'V', 5}, {'X', 10}, {'L', 50}, {'C', 100}, {'D', 500}, {'M', 1000}};
                    
                    string info;
                    string pattern = "^M{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$";
                    Regex regex = new Regex(@pattern);
                    Match match;
                    List<int> toSort = new List<int>();
                    while ((info = str.ReadLine()) != null)
                    {
                        match = regex.Match(info);
                        if (match.Success)
                        {
                            int total = 0;
                            for (int i = 1, k = 0; k < info.Length; k++)
                            {
                                if (dictionary[info[i]] <= dictionary[info[k]]) total += dictionary[info[k]];
                                else total -= dictionary[info[k]];
                                if (i < info.Length - 1) ++i;
                            }
                            toSort.Add(total);
                        }
                    }
                    toSort.Sort();
                    foreach (var i in toSort)
                    {
                        Console.Write(i + " ");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error\n" + e.Message);
            }
        }
    }
}