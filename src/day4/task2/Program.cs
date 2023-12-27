var lines = File.ReadAllLines(Path.Combine("..", "..", "..", "input",
//"sinput.txt"));
"input.txt"));

var counts = new long[lines.Length];

for (int i = 0; i < lines.Length; i++)
{
    counts[i]++;
    var card = lines[i];
    var numberSets = card.Substring(card.IndexOf(':') + 1).Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    var winningNumbers = numberSets[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse);
    var numbersIHave = numberSets[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse);
    var matches = winningNumbers.Intersect(numbersIHave);
    var value = matches.Count();

    //Console.WriteLine(value);
    //Console.WriteLine();

    //foreach (var count in counts) Console.WriteLine(count);
    //Console.WriteLine();
    //Console.WriteLine();


    for (int j = i + 1; j < lines.Length && j - i <= value; j++) counts[j] += counts[i];
}



Console.WriteLine(counts.Sum());