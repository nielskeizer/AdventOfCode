using FluentAssertions;
using Puzzles.TwentyTwentyTwo;
using Xunit;

namespace Puzzles.Tests.TwentyTwentyTwo
{
    public class Day1Tests
    {
        [Fact]
        public void FirstPuzzle()
        {
            var input = InputReader.Read("2022\\Day1.txt");

            var puzzle = new Day1();

            var solution = puzzle.SolveFirst(input);

            solution.Should().Be("66487");
        }

        [Fact]
        public void SecondPuzzle()
        {
            var input = InputReader.Read("2022\\Day1.txt");

            var puzzle = new Day1();

            var solution = puzzle.SolveSecond(input);

            solution.Should().Be("197301");
        }
    }
}