using System.Collections;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i <= 300; i++)
            {
                bool fizz = i % 3 == 0;
                bool buzz = i % 5 == 0;
                bool bang = i % 7 == 0;
                bool bong = i % 11 == 0;
                bool fezz = i % 13 == 0;
                bool reverse = i % 17 == 0;

                List<string> words = new List<string>();
                
                if (fizz)
                {
                    words.Add("Fizz");
                }
                if (buzz)
                {
                    words.Add("Buzz");
                }
                if (bang)
                {
                    words.Add("Bang");
                }
                if (bong)
                {
                    // Bong should appear on its own unless Fezz (below)
                    words.Clear();
                    words.Add("Bong");
                }
                if (fezz)
                {
                    // Fezz comes immediately before the first word beginning with a B,
                    // or at the end if there are none
                    int index = words.FindIndex(x => x[0] == 'B');
                    if (index != -1)
                    {
                        words.Insert(index, "Fezz");
                    }
                    else
                    {
                        words.Add("Fezz");
                    }
                }
                if (reverse)
                {
                    words.Reverse();
                }

                // Print the number itself if no words have been printed
                if (words.Count == 0)
                {
                    words.Add(i.ToString());
                }
                Console.WriteLine(String.Join("", words.ToArray()));
            }
        }
    }
}
