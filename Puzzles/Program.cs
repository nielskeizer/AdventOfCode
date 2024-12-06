using Puzzles;
using Puzzles.TwentyTwentyFour;

var isTest = false;
var source = isTest ? "2024\\Test.txt" : "2024\\Day2.txt";
var input = InputReader.Read(source);

var puzzle = new Day2();

var solution1 = puzzle.SolveFirst(input);
var solution2 = puzzle.SolveSecond(input);

Console.WriteLine(solution1);
Console.WriteLine(solution2);