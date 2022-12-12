using FluentAssertions;
using Puzzles.TwentyTwentyTwo;
using Xunit;

namespace Puzzles.Tests.TwentyTwentyTwo
{
    public class Day12Tests
    {
        
        [Fact]
        public void FirstPuzzle()
        {
            var input = InputReader.Read("2022\\Day12.txt");

            var puzzle = new Day12();

            var solution = puzzle.SolveFirst(input);

            solution.Should().Be("481");
        }

        [Fact]
        public void SecondPuzzle()
        {
            var input = InputReader.Read("2022\\Day12.txt");

            var puzzle = new Day12();

            var solution = puzzle.SolveSecond(input);

            solution.Should().Be("480");
        }
    }
}