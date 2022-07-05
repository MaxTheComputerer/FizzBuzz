﻿using System.Collections;

namespace FizzBuzz;

// Class which implements FizzBuzz game
// Enumerate an object to generate values
class FizzBuzzer : IEnumerable<string>
{
    // List of Rule objects to be used in the current game
    private List<Rule> rules;
    // The maximum number to play until
    private int maxNum;
    
    public FizzBuzzer()
    {
        rules = AskForRules();
        Console.Write("Enter maximum number: ");
        maxNum = int.Parse(Console.ReadLine() ?? string.Empty);
    }

    // Asks the user for which rules should be applied in this game
    // Represented by a list of "rule triggers", e.g. 3, 5, 15, etc.
    // Fetches the corresponding Rule objects
    private static List<Rule> AskForRules()
    {
        var standardRules = Rule.GetStandardRules();
        
        Console.Write("Enter a comma-separated list of rule triggers (or none for all): ");
        var requestedRuleString = Console.ReadLine();
        
        if (requestedRuleString == "")
        {
            return standardRules;
        }

        var requestedRules = requestedRuleString.Split(',').Select(int.Parse).ToArray();
        return standardRules.Where(rule => requestedRules.Contains(rule.Trigger)).ToList();
    }

    public IEnumerator<string> GetEnumerator()
    {
        for (var i = 1; i <= maxNum; i++)
        {
            var words = new List<string>();

            // Test each rule in the (ordered) list and apply it if necessary
            foreach (var rule in rules)
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
            
            // Return the correct string for this number
            yield return String.Join("", words.ToArray());
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    static void Main(string[] args)
    {
        var fizzBuzzer = new FizzBuzzer();

        foreach (var value in fizzBuzzer)
        {
            Console.WriteLine(value);
        }
    }
}
