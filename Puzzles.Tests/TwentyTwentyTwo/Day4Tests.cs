using FluentAssertions;
using Puzzles.TwentyTwentyTwo;
using Xunit;

namespace Puzzles.Tests.TwentyTwentyTwo
{
    public class Day4Tests
    {
        
        [Fact]
        public void FirstPuzzle()
        {
            var input = InputReader.Read("2022\\Day4.txt");

            var puzzle = new Day4();

            var solution = puzzle.SolveFirst(input);

            solution.Should().Be("528");
        }

        [Fact]
        public void SecondPuzzle()
        {
            var input = InputReader.Read("2022\\Day4.txt");

            var puzzle = new Day4();

            var solution = puzzle.SolveSecond(input);

            solution.Should().Be("881");
        }
    }
}