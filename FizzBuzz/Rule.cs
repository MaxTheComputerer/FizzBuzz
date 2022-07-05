namespace FizzBuzz;

// Base class for Rules
// A rule is activated when the current number is a multiple of its Trigger value
abstract class Rule
{
    public int Trigger { get; }

    protected Rule(int trigger)
    {
        Trigger = trigger;
    }

    // Does <number> trigger this rule?
    public bool Test(int number)
    {
        return number % Trigger == 0;
    }

    public abstract void Apply(List<string> wordList);

    // A list of the standard rules of the game
    public static List<Rule> GetStandardRules()
    {
        Rule fizz = new PrintRule(3, "Fizz");
        Rule buzz = new PrintRule(5, "Buzz");
        Rule bang = new PrintRule(7, "Bang");
        Rule bong = new RemoveThenPrintRule(11, "Bong");
        Rule fezz = new InsertBeforeRule(13, "Fezz", 'B');
        Rule reverse = new ReverseRule(17);

        return new List<Rule>
        {
            fizz,
            buzz,
            bang,
            bong,
            fezz,
            reverse
        };
    }
}

// A rule which displays a word (e.g. "Fizz") on a trigger
class PrintRule : Rule
{
    private string Word { get; }
    
    public PrintRule(int trigger, string word) : base(trigger)
    {
        Word = word;
    }

    public override void Apply(List<string> wordList)
    {
        wordList.Add(Word);
    }
}

// A rule which displays a word on its own
// Removes all previous words, then prints
class RemoveThenPrintRule : PrintRule
{
    public RemoveThenPrintRule(int trigger, string word) : base(trigger, word)
    {}

    public override void Apply(List<string> wordList)
    {
        // Word should appear on its own
        wordList.Clear();
        base.Apply(wordList);
    }
}

// Inserts a word before the first word in the list which begins with a given letter
class InsertBeforeRule : Rule
{
    private string Word { get; }
    private char LetterToFind { get; }
    
    public InsertBeforeRule(int trigger, string word, char letterToFind) : base(trigger)
    {
        Word = word;
        LetterToFind = letterToFind;
    }

    public override void Apply(List<string> wordList)
    {
        // Word is inserted immediately before the first word beginning with LetterToFind,
        // or at the end if there are none
        var index = wordList.FindIndex(x => x[0] == LetterToFind);
        if (index != -1)
        {
            wordList.Insert(index, Word);
        }
        else
        {
            wordList.Add(Word);
        }
    }
}

// Rule which reverses the order of words to be printed
class ReverseRule : Rule
{
    public ReverseRule(int trigger) : base(trigger) {}

    public override void Apply(List<string> wordList)
    {
        wordList.Reverse();
    }
}
