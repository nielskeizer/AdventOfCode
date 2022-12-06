using FluentAssertions;
using Puzzles.TwentyTwentyTwo;
using Xunit;

namespace Puzzles.Tests.TwentyTwentyTwo
{
    public class Day5Tests
    {
        
        [Fact]
        public void FirstPuzzle()
        {
            var input = InputReader.Read("2022\\Day5.txt");

            var puzzle = new Day5();

            var solution = puzzle.SolveFirst(input);

            solution.Should().Be("VCTFTJQCG");
        }

        [Fact]
        public void SecondPuzzle()
        {
            var input = InputReader.Read("2022\\Day5.txt");;

            var puzzle = new Day5();

            var solution = puzzle.SolveSecond(input);

            solution.Should().Be("GCFGLDNJZ");
        }
    }
}