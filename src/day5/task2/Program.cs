var lines = File.ReadAllLines(Path.Combine("..", "..", "..", "input",
//"sinput.txt"));
"input.txt"));

var seedRanges = lines[0]
	.Substring(lines[0].IndexOf(':') + 1)
	.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
	.Select(long.Parse)
	.ToArray();

List<long> seeds = new();

for (int i = 0; i < seedRanges.Length; i += 2)
{
	for (long seed = seedRanges[i]; seed < seedRanges[i] + seedRanges[i + 1]; seed++)
	{
		seeds.Add(seed);
	}
}

List<Map> maps = new();

List<MapInterval> currentIntervals = new();

for (int i = 3; i < lines.Length; i++)
{
	if (lines[i] == "")
	{
		maps.Add(new(currentIntervals));
		currentIntervals = new();
		i++;
		continue;
	}

	var data = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToArray();
	currentIntervals.Add(new(data[1], data[0], data[2]));
}

maps.Add(new(currentIntervals));

var locationNumbers = new long[seeds.Count];

for (int i = 0; i < seeds.Count; i++)
{
	long current = seeds[i];

	foreach (var map in maps)
	{
		current = map[current];
	}

	locationNumbers[i] = current;
}

Console.WriteLine(locationNumbers.Min());

record MapInterval(long SourceStart, long DestinationStart, long Range);

class Map(IEnumerable<MapInterval> intervals)
{
    private MapInterval[] intervals = intervals.ToArray();

	public long this[long i]
	{
		get 
		{
			foreach (var interval in intervals)
			{
				if (i >= interval.SourceStart && i < interval.SourceStart + interval.Range)
				{
					return interval.DestinationStart + (i - interval.SourceStart);
				}
			}

			return i;
		}
	}
}

