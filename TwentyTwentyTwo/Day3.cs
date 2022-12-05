namespace AdventOfCode.TwentyTwentyTwo
{
    public class Day3 : IPuzzle
    {
        public string SolveFirst(string input)
        {
            return input.Split("\n").Sum(x => new Rucksack(x).Priority).ToString();
        }

        public string SolveSecond(string input)
        {
            var rucksackInputs = input.Split("\n").ToList();
            var numberOfGroups = rucksackInputs.Count() / 3;

            var sum = 0;
            foreach ( var groupNumber in Enumerable.Range(0, numberOfGroups))
            {
                var ruckSacks = rucksackInputs.Skip(groupNumber*3).Take(3).Select(x => new Rucksack(x));
                var priority = new Group(ruckSacks).Priority;
                sum += priority;
            }

            return sum.ToString();
        }

        class Rucksack
        {

            public List<string> Compartments => compartments; 
            public string Contents => contents; 
            private readonly List<string> compartments = new();
            private readonly string contents;

            public Rucksack(string items)
            {
                contents = items;
                var compartmentLength = items.Length/2;
                compartments.Add(items[..compartmentLength]);
                compartments.Add(items[compartmentLength..]);
            }

            char Error() 
            {
                return compartments[0].Intersect(compartments[1]).Single();
            }

            public int Priority => Error().Priority();
        }

        class Group
        {
            private readonly IEnumerable<Rucksack> rucksacks;

            public Group(IEnumerable<Rucksack> rucksacks)
            {
                this.rucksacks = rucksacks;
            }

            char Badge()
            {
                var intersection = rucksacks
                    .Skip(1)
                    .Aggregate(
                        new HashSet<char>(rucksacks.First().Contents),
                        (h, e) => { h.IntersectWith(e.Contents); return h;}
                    );
                return intersection.Single();
            }

            public int Priority => Badge().Priority();
        }
    }
}