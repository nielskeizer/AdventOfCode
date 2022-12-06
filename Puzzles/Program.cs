using Puzzles;
using Puzzles.TwentyTwentyTwo;

var input = InputReader.Read("2022\\Day6.txt");

var puzzle = new Day6();

var solution1 = puzzle.SolveFirst(input.Skip(0).ToArray());
var solution2 = puzzle.SolveSecond(input);

Console.WriteLine(solution1);
Console.WriteLine(solution2);