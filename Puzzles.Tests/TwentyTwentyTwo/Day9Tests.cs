using FluentAssertions;
using Puzzles.TwentyTwentyTwo;
using Xunit;

namespace Puzzles.Tests.TwentyTwentyTwo
{
    public class Day9Tests
    {
        
        [Fact]
        public void FirstPuzzle()
        {
            var input = InputReader.Read("2022\\Day9.txt");

            var puzzle = new Day9();

            var solution = puzzle.SolveFirst(input);

            solution.Should().Be("6339");
        }

        [Fact]
        public void SecondPuzzle()
        {
            var input = InputReader.Read("2022\\Day9.txt");

            var puzzle = new Day9();

            var solution = puzzle.SolveSecond(input);

            solution.Should().Be("2541");
        }
    }
}