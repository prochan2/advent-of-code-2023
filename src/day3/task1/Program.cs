var lines = File.ReadAllLines(Path.Combine("..", "..", "..", "input",
//"sinput.txt"));
"input.txt"));

var height = lines.Length;
var width = lines[0].Length;

// <id, number>
Dictionary<long, long> partNumbers = new();

long[,] map = new long[height, width];

const long firstPartNumberId = 1000;
long currentPartNumberId = firstPartNumberId;
const long dotId = 0;
const long symbolId = 1;

const long unknownPartNumber = -1;
long currentPartNumber = unknownPartNumber;

for (int l = 0; l < height; l++)
{
    for (int r = 0; r < width; r++)
    {
        var c = lines[l][r];

        if (char.IsDigit(c))
        {
            if (currentPartNumber < 0)
            {
                currentPartNumber = 0;
            }

            currentPartNumber *= 10;
            currentPartNumber += c - '0';

            map[l, r] = currentPartNumberId;
        }
        else
        {
            if (currentPartNumber != unknownPartNumber)
            {
                partNumbers[currentPartNumberId] = currentPartNumber;
                currentPartNumberId++;
                currentPartNumber = unknownPartNumber;
            }

            map[l, r] = c == '.' ? dotId : symbolId;
        }
    }
}

HashSet<long> usedPartNumberIds = new();

void AddIfPartNumberId(long x)
{
    if (x >= firstPartNumberId)
    {
        usedPartNumberIds.Add(x);
    }
}

for (int l = 0; l < height; l++)
{
    for (int r = 0; r < width; r++)
    {
        if (map[l, r] == symbolId)
        {
            if (l > 0 && r > 0) AddIfPartNumberId(map[l - 1, r - 1]);
            if (l > 0) AddIfPartNumberId(map[l - 1, r]);
            if (l > 0 && r < width - 1) AddIfPartNumberId(map[l - 1, r + 1]);
            if (r > 0) AddIfPartNumberId(map[l, r - 1]);
            if (r < width - 1) AddIfPartNumberId(map[l, r + 1]);
            if (l < height - 1 &&  r > 0) AddIfPartNumberId(map[l + 1, r - 1]);
            if (l < height - 1) AddIfPartNumberId(map[l + 1, r]);
            if (l < height - 1 && r < width - 1) AddIfPartNumberId(map[l + 1, r + 1]);
        }
    }
}

var sum = usedPartNumberIds.Select(id => partNumbers[id]).Sum();

Console.WriteLine(sum);