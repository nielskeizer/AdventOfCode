using System.Globalization;

namespace Puzzles.TwentyTwentyFour
{
    public class Day1 : IPuzzle
    {
        public string SolveFirst(string[] input)
        {
            var (leftList, rightList) = LocationIds(input);

            leftList.Sort();
            rightList.Sort();

            return leftList
                .Select((number, index) => Math.Abs(rightList.ElementAt(index) - number))
                .Sum()
                .ToString();
        }

        public string SolveSecond(string[] input)
        {
            var (leftList, rightList) = LocationIds(input);

            var occurrencesPerInput = new Dictionary<int, int>();
            foreach (var number in rightList)
            {
                if (occurrencesPerInput.ContainsKey(number))
                {
                    occurrencesPerInput[number] +=1;
                }
                else
                {
                    occurrencesPerInput[number] = 1;
                }
            }

            return leftList
                .Select(number => number * (occurrencesPerInput.TryGetValue(number, out var count) ? count : 0))
                .Sum()
                .ToString();
        }

        private (List<int>, List<int>) LocationIds(string[] input)
        {
            var leftList = new List<int>();
            var rightList = new List<int>();

            foreach(var line in input)
            {
                var numbers = line.Split(' ').Select(x => int.TryParse(x, out var y) ? y : 0).ToList();
                leftList.Add(numbers.First());
                rightList.Add(numbers.Last());
            }

            return (leftList, rightList);
        }
    }
}