var lines = File.ReadAllLines(Path.Combine("..", "..", "..", "input",
    //"sinput.txt"));
    "input.txt"));

//lines.ToList().ForEach(Console.WriteLine);
//Console.WriteLine();

var digitLines = lines.Select(l => l
    .Where(char.IsDigit)
    .Select(c => (long)(c - '0')).ToArray());

//digitLines.ToList().ForEach(Console.WriteLine);
//Console.WriteLine();

var calibrationValues = digitLines.Select(d => d[0] * 10 + d[^1]);

//calibrationValues.ToList().ForEach(Console.WriteLine);
//Console.WriteLine();

var sum = calibrationValues.Sum();

Console.WriteLine(sum);