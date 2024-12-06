using FluentAssertions;
using Puzzles.TwentyTwentyFour;
using Xunit;

namespace Puzzles.Tests.TwentyTwentyFour
{
    public class Day2Tests
    {
        [Fact]
        public void FirstPuzzle()
        {
            var input = InputReader.Read("2024\\Day2.txt");

            var puzzle = new Day2();

            var solution = puzzle.SolveFirst(input);

            solution.Should().Be("639");
        }

        [Fact]
        public void SecondPuzzle()
        {
            var input = InputReader.Read("2024\\Day2.txt");

            var puzzle = new Day2();

            var solution = puzzle.SolveSecond(input);

            solution.Should().Be("22014209");
        }
    }
}