using FluentAssertions;
using Puzzles.TwentyTwentyTwo;
using Xunit;

namespace Puzzles.Tests.TwentyTwentyTwo
{
    public class Day7Tests
    {
        
        [Fact]
        public void FirstPuzzle()
        {
            var input = InputReader.Read("2022\\Day7.txt");

            var puzzle = new Day7();

            var solution = puzzle.SolveFirst(input);

            solution.Should().Be("1491614");
        }

        [Fact]
        public void SecondPuzzle()
        {
            var input = InputReader.Read("2022\\Day7.txt");

            var puzzle = new Day7();

            var solution = puzzle.SolveSecond(input);

            solution.Should().Be("6400111");
        }
    }
}