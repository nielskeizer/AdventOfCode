using FluentAssertions;
using Puzzles.TwentyTwentyTwo;
using Xunit;

namespace Puzzles.Tests.TwentyTwentyTwo
{
    public class Day3Tests
    {
        
        [Fact]
        public void FirstPuzzle()
        {
            var input = InputReader.Read("2022\\Day3.txt");

            var puzzle = new Day3();

            var solution = puzzle.SolveFirst(input);

            solution.Should().Be("7848");
        }

        [Fact]
        public void SecondPuzzle()
        {
            var input = InputReader.Read("2022\\Day3.txt");

            var puzzle = new Day3();

            var solution = puzzle.SolveSecond(input);

            solution.Should().Be("2616");
        }
    }
}