using FluentAssertions;
using Puzzles.TwentyTwentyFour;
using Xunit;

namespace Puzzles.Tests.TwentyTwentyFour
{
    public class Day1Tests
    {
        [Fact]
        public void FirstPuzzle()
        {
            var input = InputReader.Read("2024\\Day1.txt");

            var puzzle = new Day1();

            var solution = puzzle.SolveFirst(input);

            solution.Should().Be("1938424");
        }

        [Fact]
        public void SecondPuzzle()
        {
            var input = InputReader.Read("2024\\Day1.txt");

            var puzzle = new Day1();

            var solution = puzzle.SolveSecond(input);

            solution.Should().Be("22014209");
        }
    }
}