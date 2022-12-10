using Puzzles;
using Puzzles.TwentyTwentyTwo;

var input = InputReader.Read("2022\\Day10.txt");

var puzzle = new Day10();

var solution1 = puzzle.SolveFirst(input);
var solution2 = puzzle.SolveSecond(input);

Console.WriteLine(solution1);
Console.WriteLine(solution2);