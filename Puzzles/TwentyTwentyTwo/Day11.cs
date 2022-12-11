using System.Text;

namespace Puzzles.TwentyTwentyTwo
{
    public class Day11 : IPuzzle
    {
        public string SolveFirst(string[] input)
        {
            var monkeys = CreateMonkeys(input, true);
            var game = new Game(monkeys);
            game.Play(20);
            return game.MonkeyBusiness().ToString();
        }

        public string SolveSecond(string[] input)
        {
            var monkeys = CreateMonkeys(input, false);
            var game = new Game(monkeys);
            game.Play(10000);
            Console.WriteLine(game.InspectLog());
            return game.MonkeyBusiness().ToString();
        }

        List<Monkey> CreateMonkeys(string[] input, bool boredomDecreasesWorryLevel)
        {
            var linesPerMonkey = 7;
            var numberOfMonkeys = (input.Count() + 1) / linesPerMonkey;
            var monkeys = new List<Monkey>();
            for (int monkeyNumber = 0; monkeyNumber < numberOfMonkeys; monkeyNumber++)
            {
                var monkeyStartLine = monkeyNumber * linesPerMonkey;
                var monkeyId = int.Parse(input[monkeyStartLine][7..^1]);
                var startingItems = input[monkeyStartLine + 1][18..];
                var operation = input[monkeyStartLine + 2][13..];
                var test = input[monkeyStartLine + 3][8..];
                var toMonkey = input[(monkeyStartLine + 4)..(monkeyStartLine + 6)];

                var monkey = boredomDecreasesWorryLevel
                ? new MonkeyBuilder(monkeyId)
                    .WithItems(startingItems)
                    .WithInspection(operation)
                    .WithTest(test)
                    .WithThrowToMonkey(toMonkey)
                    .Build()
                : new MonkeyBuilder(monkeyId)
                    .WithItems(startingItems)
                    .WithInspection(operation)
                    .WithTest(test)
                    .WithThrowToMonkey(toMonkey)
                    .WithoutBoredom()
                    .Build();

                monkeys.Add(monkey);
            }

            return monkeys.OrderBy(x => x.Id).ToList();
        }

        class Game
        {
            readonly Dictionary<int, Monkey> monkeys;
            readonly Dictionary<int, int> monkeyActivities;
            int round;
            int leastCommonMultiple;

            public Game(List<Monkey> monkeys)
            {
                this.monkeys = monkeys.ToDictionary(x => x.Id, x => x);
                monkeyActivities = monkeys.ToDictionary(x => x.Id, x => 0);
                leastCommonMultiple = LeastCommonMultiple(monkeys.Select(x => x.Divisibility));
            }

            public void Play(int numberOfRounds)
            {
                foreach (var round in Enumerable.Range(1, numberOfRounds))
                {
                    PlayRound();
                    this.round = round;
                }
            }

            void PlayRound()
            {
                foreach (var (monkeyId, monkey) in monkeys)
                {
                    TakeTurn(monkey);
                }
            }

            void TakeTurn(Monkey monkey)
            {
                while (monkey.Items.TryDequeue(out var item))
                {
                    monkeyActivities[monkey.Id] += 1;
                    item.WorryLevel = monkey.Inspect(item.WorryLevel) % leastCommonMultiple;
                    item.WorryLevel = monkey.GetBored(item.WorryLevel);
                    var test = monkey.Test(item.WorryLevel);
                    var throwToMonkey = monkey.ThrowToMonkey(test);
                    monkeys[throwToMonkey].Catch(item);
                }
            }

            public long MonkeyBusiness() => monkeyActivities.Values.OrderBy(x => x).TakeLast(2).Select(x => (long)x).Aggregate((long)1, (i, j) => i * j);

            public string InspectLog()
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine($"== After round {round} ==");
                foreach (var (monkeyId, activity) in monkeyActivities)
                {
                    stringBuilder.AppendLine($"Monkey {monkeyId} inspected items {activity} times.");
                }
                return stringBuilder.ToString();
            }
        }

        class Monkey
        {
            public int Id { get; }
            public Queue<Item> Items { get; set; } = new();
            public Func<long, long> Inspect { get; set; } = x => throw new MonkeyException($"This monkey does not know how to inspect.");
            public Func<long, long> GetBored { get; set; } = x => throw new MonkeyException($"This monkey does not do boredom.");
            public Func<long, bool> Test { get; set; } = x => throw new MonkeyException($"This monkey does not know how to test.");
            public Func<bool, int> ThrowToMonkey { get; set; } = x => throw new MonkeyException($"This monkey does not know how to throw to.");
            public int Divisibility { get; set; }
            public Monkey(int id)
            {
                Id = id;
            }

            public void Catch(Item item)
            {
                Items.Enqueue(item);
            }
        }

        class Item
        {
            public long WorryLevel { get; set; }
        }

        class MonkeyBuilder
        {
            readonly Monkey monkey;

            public MonkeyBuilder(int id)
            {
                this.monkey = new Monkey(id);
                monkey.GetBored = x => x / 3;
            }

            public MonkeyBuilder WithItems(string startingItems)
            {
                var worryLevels = startingItems.Split(", ").Select(int.Parse);
                foreach (var worryLevel in worryLevels)
                {
                    monkey.Items.Enqueue(new Item { WorryLevel = worryLevel });
                }
                return this;
            }

            public MonkeyBuilder WithTest(string test)
            {
                var divisibleBy = int.Parse(test[13..]);
                monkey.Divisibility = divisibleBy;
                monkey.Test = x => x % divisibleBy == 0;
                return this;
            }

            public MonkeyBuilder WithInspection(string inspectionInput)
            {
                var operation = inspectionInput[10];
                var numberInput = inspectionInput[12..];

                if (operation == '+')
                {
                    var number = int.Parse(numberInput);
                    monkey.Inspect = x => x + number;
                    return this;
                }
                if (operation == '*')
                {
                    if (numberInput == "old")
                    {
                        monkey.Inspect = x => x * x;
                        return this;
                    }
                    var number = int.Parse(numberInput);
                    monkey.Inspect = x => x * number;
                    return this;
                }

                throw new ArgumentException($"Unknown inspectionInput {inspectionInput}");
            }

            public MonkeyBuilder WithThrowToMonkey(string[] toMonkeyInputs)
            {
                var monkeyIfTrue = -1;
                var monkeyIfFalse = -1;

                foreach (var toMonkeyInput in toMonkeyInputs)
                {
                    if (toMonkeyInput.Contains("true"))
                    {
                        monkeyIfTrue = int.Parse(toMonkeyInput.Split(" ").Last());
                    }
                    if (toMonkeyInput.Contains("false"))
                    {
                        monkeyIfFalse = int.Parse(toMonkeyInput.Split(" ").Last());
                    }
                }

                if (monkeyIfTrue == -1 || monkeyIfFalse == -1) throw new ArgumentException("Can not find monkey targets.");
                if (monkeyIfTrue == monkey.Id || monkeyIfFalse == monkey.Id) throw new ArgumentException("Monkey can not throw to itself.");
                monkey.ThrowToMonkey = x => x ? monkeyIfTrue : monkeyIfFalse;
                return this;
            }

            public MonkeyBuilder WithoutBoredom()
            {
                monkey.GetBored = x => x;
                return this;
            }

            public Monkey Build()
            {
                return monkey;
            }
        }

        class MonkeyException : Exception
        {
            public MonkeyException(string? message) : base(message)
            {
            }
        }

        static int GreatestCommonFactor(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static int LeastCommonMultiple(int a, int b)
        {
            return (a / GreatestCommonFactor(a, b)) * b;
        }

        static int LeastCommonMultiple(IEnumerable<int> numbers)
        {
            return numbers.Skip(2).Aggregate(LeastCommonMultiple(numbers.First(), numbers.Skip(1).First()), (a, b) => LeastCommonMultiple(a,b));
        }
    }
}