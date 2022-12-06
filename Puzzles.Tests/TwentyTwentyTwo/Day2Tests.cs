using FluentAssertions;
using Puzzles.TwentyTwentyTwo;
using Xunit;

namespace Puzzles.Tests.TwentyTwentyTwo
{
    public class Day2Tests
    {
        
        [Fact]
        public void FirstPuzzle()
        {
            var input = InputReader.Read("2022\\Day2.txt");

            var puzzle = new Day2();

            var solution = puzzle.SolveFirst(input);

            solution.Should().Be("17189");
        }

        [Fact]
        public void SecondPuzzle()
        {
            var input = InputReader.Read("2022\\Day2.txt");;

            var puzzle = new Day2();

            var solution = puzzle.SolveSecond(input);

            solution.Should().Be("13490");
        }
    }
}