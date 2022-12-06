﻿using Puzzles;
using Puzzles.TwentyTwentyTwo;

var input = InputReader.Read("2022\\Day5.txt");

var puzzle = new Day5();

var solution1 = puzzle.SolveFirst(input);
var solution2 = puzzle.SolveSecond(input);

Console.WriteLine(solution1);
Console.WriteLine(solution2);