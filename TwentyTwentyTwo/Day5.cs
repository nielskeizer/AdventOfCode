namespace AdventOfCode.TwentyTwentyTwo
{
    public class Day5 : IPuzzle
    {
        public string SolveFirst(string input)
        {
            var inputByLine = input.Split("\n");

            var separationLine = inputByLine.Select((x, index) => (x, index)).Skip(1).First(y => string.IsNullOrEmpty(y.x)).index;
            var stackLine = inputByLine[separationLine - 1];
            var numberOfStacks = int.Parse(stackLine.Split(" ").Where(x => !string.IsNullOrEmpty(x)).Last());
            var stackRows = stackLine.Select((character, index) => (character, index)).Where(x => char.IsNumber(x.character)).ToDictionary(x => x.index, x => int.Parse(x.character.ToString()));
            var lineNumber = separationLine - 2;
            while (lineNumber >= 0)
            {
                Console.WriteLine(inputByLine[lineNumber]);
                lineNumber--;
            }
            throw new NotImplementedException();
        }

        public string SolveSecond(string input)
        {
            throw new NotImplementedException();
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

            bool InvalidStackNumber(int stackNumber) => stackNumber <= 0 || stackNumber > numberOfStacks;
        }

        class Step
        {
            public Step(int numberOfCrates, int sourceStackId, int targetStackId)
            {
                this.NumberOfCrates = numberOfCrates;
                this.SourceStackId = sourceStackId;
                this.TargetStackId = targetStackId;

            }
            public int NumberOfCrates { get; set; }
            public int SourceStackId { get; set; }
            public int TargetStackId { get; set; }
        }
    }
}