using FluentAssertions;
using Puzzles.TwentyTwentyTwo;
using Xunit;

namespace Puzzles.Tests.TwentyTwentyTwo
{
    public class Day10Tests
    {
        
        [Fact]
        public void FirstPuzzle()
        {
            var input = InputReader.Read("2022\\Day10.txt");

            var puzzle = new Day10();

            var solution = puzzle.SolveFirst(input);

            solution.Should().Be("12540");
        }

        [Fact]
        public void SecondPuzzle()
        {
            var input = InputReader.Read("2022\\Day10.txt");

            var puzzle = new Day10();

            var solution = puzzle.SolveSecond(input);

            solution.Should().Be(@"####.####..##..####.####.#....#..#.####.
#....#....#..#....#.#....#....#..#.#....
###..###..#......#..###..#....####.###..
#....#....#.....#...#....#....#..#.#....
#....#....#..#.#....#....#....#..#.#....
#....####..##..####.####.####.#..#.####.");
        }
    }
}