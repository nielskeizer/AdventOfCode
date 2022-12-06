using System.Text;

namespace Puzzles.TwentyTwentyTwo
{
    public class Day5 : IPuzzle
    {
        public string SolveFirst(string[] input)
        {
            var inputByLine = input;

            var separationLine = inputByLine.Select((x, index) => (x, index)).First(y => string.IsNullOrEmpty(y.x)).index;

            var supplies = CreateSupplies(inputByLine[..separationLine]);
            var steps = CreateSteps(inputByLine[separationLine..]);

            Console.WriteLine(supplies.ToString());

            foreach (var step in steps)
            {
                foreach (var i in Enumerable.Range(0, step.NumberOfCrates))
                {
                    supplies.MoveCrate(step.SourceStackId, step.TargetStackId);
                }

                //Console.WriteLine(supplies.ToString());
            }

            return supplies.TopCrates();
        }
        
        public string SolveSecond(string[] input)
        {
            var inputByLine = input;

            var separationLine = inputByLine.Select((x, index) => (x, index)).First(y => string.IsNullOrEmpty(y.x)).index;

            var supplies = CreateSupplies(inputByLine[..separationLine]);
            var steps = CreateSteps(inputByLine[separationLine..]);

            Console.WriteLine(supplies.ToString());

            foreach (var step in steps)
            {
                supplies.MoveMultipleCrates(step);
                //Console.WriteLine(supplies.ToString());
            }

            return supplies.TopCrates();
        }

        static List<Step> CreateSteps(string[] input)
        {
            var steps = new List<Step>();
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var stepInput = line.Split(" ").Where(x => int.TryParse(x, out var i)).Select(x => int.Parse(x)).ToList();
                steps.Add(new Step {NumberOfCrates = stepInput[0], SourceStackId = stepInput[1], TargetStackId = stepInput[2]});
            }

            return steps;
        }

        static Supplies CreateSupplies(string[] input)
        {
            var stackLine = input[input.Length - 1];
            var numberOfStacks = int.Parse(stackLine.Split(" ").Where(x => !string.IsNullOrEmpty(x)).Last());
            var stackColumns = stackLine.Select((character, index) => (character, index)).Where(x => char.IsNumber(x.character)).ToDictionary(x => int.Parse(x.character.ToString()), x => x.index);
            var lineNumber = input.Length - 2;

            var supplies = new Supplies(numberOfStacks);

            while (lineNumber >= 0)
            {
                foreach (var (stack, column) in stackColumns)
                {
                    var crateCandidate = input[lineNumber][column];
                    if (crateCandidate != ' ')
                    {
                        supplies.AddCrate(stack, crateCandidate);
                    }
                }
                lineNumber--;
            }

            return supplies;
        }

        class Supplies
        {
            readonly int numberOfStacks;
            Dictionary<int, Stack<char>> stacks = new();

            public Supplies(int numberOfStacks)
            {

                foreach (var i in Enumerable.Range(1, numberOfStacks))
                {
                    stacks.Add(i, new Stack<char>());
                }

                this.numberOfStacks = numberOfStacks;
            }

            public void AddCrate(int stackNumber, char crate)
            {
                if (InvalidStackNumber(stackNumber)) throw new ArgumentOutOfRangeException();

                stacks[stackNumber].Push(crate);
            }

            public char RemoveCrate(int stackNumber)
            {
                if (InvalidStackNumber(stackNumber)) throw new ArgumentOutOfRangeException();

                var crate = stacks[stackNumber].Pop();
                return crate;
            }

            public void MoveCrate(int stackNumberFrom, int stackNumberTo)
            {
                AddCrate(stackNumberTo, RemoveCrate(stackNumberFrom));
            }

            public void MoveMultipleCrates(Step step)
            {
                var cratesToMove = Enumerable.Range(0, step.NumberOfCrates).Select(x => RemoveCrate(step.SourceStackId));

                foreach (var crate in cratesToMove.Reverse())
                {
                    AddCrate(step.TargetStackId, crate);
                }
            }

            bool InvalidStackNumber(int stackNumber) => stackNumber <= 0 || stackNumber > numberOfStacks;

            public override string ToString()
            {
                var stringBuilder = new StringBuilder("");

                foreach (var (stackNumber, stack) in stacks)
                {
                    stringBuilder.AppendLine($"Stack {stackNumber}: {string.Join(", ", stack)} ");
                }

                return stringBuilder.ToString();
            }

            public string TopCrates()
            {
                var stringBuilder = new StringBuilder("");

                foreach (var stackNumber in Enumerable.Range(1, numberOfStacks))
                {
                    stringBuilder.Append(stacks[stackNumber].Peek());
                }

                return stringBuilder.ToString();
                 
            }
        }

        class Step
        {
            public int NumberOfCrates { get; set; }
            public int SourceStackId { get; set; }
            public int TargetStackId { get; set; }
        }
    }
}