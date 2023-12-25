var lines = File.ReadAllLines(Path.Combine("..", "..", "..", "input",
//"sinput.txt"));
"input.txt"));

var games = new Dictionary<long, List<(long R, long G, long B)>>();

var idIndex = "Game ".Length;

foreach (var line in lines)
{
    var colonIndex = line.IndexOf(':');
    var id = long.Parse(line.Substring(idIndex, colonIndex - idIndex));
    var packStrings = line.Substring(colonIndex + 1).Split(new[] { ';' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

    var packs = new List<(long R, long G, long B)>();

    foreach (var packString in packStrings)
    {
        var cubeStrings = packString.Split(new[] { ',' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        long r = 0, g = 0, b = 0;

        foreach (var cubeString in cubeStrings)
        {
            var cubes = cubeString.Split(new[] { ' ' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var count = long.Parse(cubes[0]);
            
            switch (cubes[1])
            {
                case "red": r += count; break;
                case "green": g += count; break;
                case "blue": b += count; break;
                default: throw new InvalidOperationException();
            }

            //Console.WriteLine($"{cubes[0]} {cubes[1]}");
        }

        packs.Add((r, g, b));
    }

    games.Add(id, packs);
}

long maxR = 12, maxG = 13, maxB = 14;

var possibleGames = new List<long>();

foreach (var game in games)
{
    Console.Write(game.Key);
    Console.Write(": ");

    var possible = true;

    foreach (var pack in game.Value)
    {
        Console.Write($"{pack.R}, {pack.G}, {pack.B}; ");

        if (pack.R > maxR || pack.G > maxG || pack.B > maxB)
        {
            possible = false;

            Console.Write("IMPOSSIBLE");
            
            break;
        }
    }

    if (possible)
    {
        possibleGames.Add(game.Key);
    }

    Console.WriteLine();
}

Console.WriteLine(possibleGames.Sum());