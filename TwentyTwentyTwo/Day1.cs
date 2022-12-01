namespace AdventOfCode.TwentyTwentyTwo
{
    public class Day1 : IPuzzle
    {
        public string SolveFirst(string input)
        {
            var foods = input.Split("\n");

            List<Elf> elves = MakeElves(foods);

            return elves.Max(x => x.Calories).ToString();
        }

        public string SolveSecond(string input)
        {            
            var foods = input.Split("\n");

            List<Elf> elves = MakeElves(foods);

            return elves.OrderBy(x => x.Calories).TakeLast(3).Sum(x => x.Calories).ToString();
        }

        private static List<Elf> MakeElves(string[] foods)
        {
            var elf = new Elf();
            var elves = new List<Elf>();

            foreach (var food in foods)
            {
                if (string.IsNullOrEmpty(food))
                {
                    elves.Add(elf);
                    elf = new();
                }
                else if (int.TryParse(food, out var calories))
                {
                    elf.Calories += calories;
                }
                else
                {
                    throw new ArgumentException($"Invalid food {food}.");
                }
            }

            return elves;
        }
    }

    class Elf
    {
        public int Calories { get; set; }
    }
}