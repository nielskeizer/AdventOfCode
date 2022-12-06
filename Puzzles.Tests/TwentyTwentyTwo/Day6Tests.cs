using FluentAssertions;
using Puzzles.TwentyTwentyTwo;
using Xunit;

namespace Puzzles.Tests.TwentyTwentyTwo
{
    public class Day6Tests
    {
        
        [Fact]
        public void FirstPuzzle()
        {
            var input = InputReader.Read("2022\\Day6.txt");

            var puzzle = new Day6();

            var solution = puzzle.SolveFirst(input);

            solution.Should().Be("1582");
        }

        [Fact]
        public void SecondPuzzle()
        {
            var input = InputReader.Read("2022\\Day6.txt");;

            var puzzle = new Day6();

            var solution = puzzle.SolveSecond(input);

            solution.Should().Be("3588");
        }
    }
}