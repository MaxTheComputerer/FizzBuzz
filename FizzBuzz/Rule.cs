namespace FizzBuzz;

abstract class Rule
{
    public int Trigger { get; }

    protected Rule(int trigger)
    {
        Trigger = trigger;
    }

    public bool Test(int number)
    {
        return number % Trigger == 0;
    }

    public abstract void Apply(List<string> wordList);

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

class PrintRule : Rule
{
    public string Word { get; }
    
    public PrintRule(int trigger, string word) : base(trigger)
    {
        Word = word;
    }

    public override void Apply(List<string> wordList)
    {
        wordList.Add(Word);
    }
}

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

class InsertBeforeRule : Rule
{
    public string Word { get; }
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
        int index = wordList.FindIndex(x => x[0] == LetterToFind);
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

class ReverseRule : Rule
{
    public ReverseRule(int trigger) : base(trigger)
    {
    }

    public override void Apply(List<string> wordList)
    {
        wordList.Reverse();
    }
}
