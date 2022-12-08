using FluentAssertions;
using Puzzles.TwentyTwentyTwo;
using Xunit;

namespace Puzzles.Tests.TwentyTwentyTwo
{
    public class Day8Tests
    {
        
        [Fact]
        public void FirstPuzzle()
        {
            var input = InputReader.Read("2022\\Day8.txt");

            var puzzle = new Day8();

            var solution = puzzle.SolveFirst(input);

            solution.Should().Be("1776");
        }

        [Fact]
        public void SecondPuzzle()
        {
            var input = InputReader.Read("2022\\Day8.txt");

            var puzzle = new Day8();

            var solution = puzzle.SolveSecond(input);

            solution.Should().Be("234416");
        }
    }
}