var lines = File.ReadAllLines(Path.Combine("..", "..", "..", "input",
//"sinput.txt"));
"input.txt"));

var height = lines.Length;
var width = lines[0].Length;

// <id, number>
Dictionary<long, long> gears = new();

long[,] map = new long[height, width];

const long firstGearId = 1000;
long currentGearId = firstGearId;
const long nonStarId = 0;
const long starId = 1;

const long unknownGear = -1;
long currentGear = unknownGear;

for (int l = 0; l < height; l++)
{
    for (int r = 0; r < width; r++)
    {
        var c = lines[l][r];

        if (char.IsDigit(c))
        {
            if (currentGear < 0)
            {
                currentGear = 0;
            }

            currentGear *= 10;
            currentGear += c - '0';

            map[l, r] = currentGearId;
        }
        else
        {
            if (currentGear != unknownGear)
            {
                gears[currentGearId] = currentGear;
                currentGearId++;
                currentGear = unknownGear;
            }

            map[l, r] = c == '*' ? starId : nonStarId;
        }
    }
}

List<long> gearRatios = new();

long gear1 = unknownGear;
long gear2 = unknownGear;

bool TrySetGear(long gearId)
{
    if (gearId < firstGearId)
    {
        return true;
    }

    var gear = gears[gearId];

    if (gear == gear1 || gear == gear2)
    {
        return true;
    }

    if (gear1 == unknownGear)
    {
        gear1 = gear;
        return true;
    }

    if (gear2 == unknownGear)
    {
        gear2 = gear;
        return true;
    }

    return false;
}

for (int l = 0; l < height; l++)
{
    for (int r = 0; r < width; r++)
    {
        if (map[l, r] == starId)
        {
            bool success = true;

            if (l > 0 && r > 0) success &= TrySetGear(map[l - 1, r - 1]);
            if (l > 0) success &= TrySetGear(map[l - 1, r]);
            if (l > 0 && r < width - 1) success &= TrySetGear(map[l - 1, r + 1]);
            if (r > 0) success &= TrySetGear(map[l, r - 1]);
            if (r < width - 1) success &= TrySetGear(map[l, r + 1]);
            if (l < height - 1 &&  r > 0) success &= TrySetGear(map[l + 1, r - 1]);
            if (l < height - 1) success &= TrySetGear(map[l + 1, r]);
            if (l < height - 1 && r < width - 1) success &= TrySetGear(map[l + 1, r + 1]);

            if (success && gear1 != unknownGear && gear2 != unknownGear)
            {
                gearRatios.Add(gear1 * gear2);
            }
        }

        gear1 = gear2 = unknownGear;
    }
}

var sum = gearRatios.Sum();

Console.WriteLine(sum);