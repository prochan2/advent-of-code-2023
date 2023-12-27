var lines = File.ReadAllLines(Path.Combine("..", "..", "..", "input",
//"sinput.txt"));
"input.txt"));

var sum = 0l;

foreach (var card in lines)
{
    var numberSets = card.Substring(card.IndexOf(':') + 1).Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    var winningNumbers = numberSets[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse);
    var numbersIHave = numberSets[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse);
    var matches = winningNumbers.Intersect(numbersIHave);
    var value = (long) Math.Pow(2, matches.Count() - 1);
    sum += value;
}

Console.WriteLine(sum);