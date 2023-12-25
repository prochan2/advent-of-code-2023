var lines = File.ReadAllLines(Path.Combine("..", "..", "..", "input",
    //"sinput.txt"));
    "input.txt"));

var digitMap = new[]
{
    ("one", "1"),
    ("two", "2"),
    ("three", "3"),
    ("four", "4"),
    ("five", "5"),
    ("six", "6"),
    ("seven", "7"),
    ("eight", "8"),
    ("nine", "9")
};

var digitLines = lines
    .Select(ReplaceWords)
    .Select(l => l
    .Where(char.IsDigit)
    .Select(c => (long)(c - '0')).ToArray())
    .ToArray();

var calibrationValues = digitLines.Select(d => d[0] * 10 + d[^1]).ToArray();

//for (int i = 0; i < lines.Length; i++)
//{
//    Console.WriteLine($"{lines[i]} {string.Join("", digitLines[i])} {calibrationValues[i]}");
//}

//Console.WriteLine();

var sum = calibrationValues.Sum();

Console.WriteLine(sum);

string ReplaceWords(string s)
{
    int startIndex = 0;

    while (startIndex < s.Length)
    {
        string substring = s[startIndex..];

        foreach (var digit in digitMap)
        {
            if (substring.StartsWith(digit.Item1))
            {
                var tailIndex = startIndex + digit.Item2.Length;
                var tail = tailIndex < s.Length ? s.AsSpan(tailIndex) : "";
                s = string.Concat(s.AsSpan(0, startIndex), digit.Item2, tail);
            }
        }

        startIndex++;
    }

    return s;
}