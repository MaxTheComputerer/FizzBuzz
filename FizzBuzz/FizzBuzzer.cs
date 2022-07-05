namespace FizzBuzz
{
    class FizzBuzzer
    {
        private static List<Rule> AskForRules()
        {
            List<Rule> standardRules = Rule.GetStandardRules();
            
            Console.Write("Enter a comma-separated list of rule triggers (or none for all): ");
            string requestedRuleString = Console.ReadLine();
            
            if (requestedRuleString == "")
            {
                return standardRules;
            }

            int[] requestedRules = requestedRuleString.Split(',').Select(int.Parse).ToArray();
            List<Rule> ruleList = new List<Rule>();
            foreach (Rule rule in standardRules)
            {
                if (requestedRules.Contains(rule.Trigger))
                {
                    ruleList.Add(rule);
                }
            }
            return ruleList;
        }

        public void Play()
        {
            List<Rule> rules = AskForRules();
            Console.Write("Enter maximum number: ");
            int maxNum = int.Parse(Console.ReadLine());
            
            for (int i = 1; i <= maxNum; i++)
            {
                List<string> words = new List<string>();

                foreach (Rule rule in rules)
                {
                    if (rule.Test(i))
                    {
                        rule.Apply(words);
                    }
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

    class Program
    {
        static void Main(string[] args)
        {
            var fizzBuzzer = new FizzBuzzer();
            fizzBuzzer.Play();
        }
    }
}
