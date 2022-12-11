using FluentAssertions;
using Puzzles.TwentyTwentyTwo;
using Xunit;

namespace Puzzles.Tests.TwentyTwentyTwo
{
    public class Day11Tests
    {
        
        [Fact]
        public void FirstPuzzle()
        {
            var input = InputReader.Read("2022\\Day11.txt");

            var puzzle = new Day11();

            var solution = puzzle.SolveFirst(input);

            solution.Should().Be("56120");
        }

        [Fact]
        public void SecondPuzzle()
        {
            var input = InputReader.Read("2022\\Day11.txt");

            var puzzle = new Day11();

            var solution = puzzle.SolveSecond(input);

            solution.Should().Be("24389045529");
        }
    }
}