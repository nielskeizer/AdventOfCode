namespace AdventOfCode.TwentyTwentyTwo
{
    public class Day2 : IPuzzle
    {
        public string SolveFirst(string input)
        {
            var strategyGuide = new StrategyGuide(input);
            var game = new FirstGame();
            var totalScore = 0;

            foreach (var strategy in strategyGuide.Strategies)
            {
                totalScore += game.RoundScore(strategy);
            }

            return totalScore.ToString();
        }

        public string SolveSecond(string input)
        {
            var strategyGuide = new CorrectStrategyGuide(input);
            var game = new SecondGame();
            var totalScore = 0;

            foreach (var strategy in strategyGuide.Strategies)
            {
                totalScore += game.RoundScore(strategy);
            }

            return totalScore.ToString();
        }

        class StrategyGuide
        {
            Dictionary<string, Shape> opponent = new(){{"A", Shape.Rock}, {"B", Shape.Paper}, {"C", Shape.Scissors}};
            Dictionary<string, Shape> player = new(){{"X", Shape.Rock}, {"Y", Shape.Paper}, {"Z", Shape.Scissors}};
            List<(Shape, Shape)> strategies = new();

            public List<(Shape, Shape)> Strategies => strategies;
            public StrategyGuide(string input)
            {
                var games = input.Split("\n").Select(x => x.Split(" "));
                foreach (var game in games)
                {
                    strategies.Add((opponent[game[0]], player[game[1]]));
                }
            }
        }

        class CorrectStrategyGuide
        {
            Dictionary<string, Shape> opponent = new(){{"A", Shape.Rock}, {"B", Shape.Paper}, {"C", Shape.Scissors}};
            Dictionary<string, Result> player = new(){{"X", Result.Lose}, {"Y", Result.Draw}, {"Z", Result.Win}};
            List<(Shape, Result)> strategies = new();

            public List<(Shape, Result)> Strategies => strategies;
            public CorrectStrategyGuide(string input)
            {
                var games = input.Split("\n").Select(x => x.Split(" "));
                foreach (var game in games)
                {
                    strategies.Add((opponent[game[0]], player[game[1]]));
                }
            }
        }

        class FirstGame
        {
            Dictionary<(Shape,Shape), int> outcomeScores = new()
            {
                {(Shape.Rock, Shape.Rock), 3},
                {(Shape.Rock, Shape.Paper), 6},
                {(Shape.Rock, Shape.Scissors), 0},
                {(Shape.Paper, Shape.Rock), 0},
                {(Shape.Paper, Shape.Paper), 3},
                {(Shape.Paper, Shape.Scissors), 6},
                {(Shape.Scissors, Shape.Rock), 6},
                {(Shape.Scissors, Shape.Paper), 0},
                {(Shape.Scissors, Shape.Scissors), 3}                
            };
            public int RoundScore((Shape, Shape) strategy)
            {
                return outcomeScores[strategy] + (int)strategy.Item2;
            }
        }

        class SecondGame
        {
            Dictionary<(Shape,Result), Shape> choices = new()
            {
                {(Shape.Rock, Result.Lose), Shape.Scissors},
                {(Shape.Rock, Result.Draw), Shape.Rock},
                {(Shape.Rock, Result.Win), Shape.Paper},
                {(Shape.Paper, Result.Lose), Shape.Rock},
                {(Shape.Paper, Result.Draw), Shape.Paper},
                {(Shape.Paper, Result.Win), Shape.Scissors},
                {(Shape.Scissors, Result.Lose), Shape.Paper},
                {(Shape.Scissors, Result.Draw), Shape.Scissors},
                {(Shape.Scissors, Result.Win), Shape.Rock}                
            };
            public int RoundScore((Shape, Result) strategy)
            {
                return (int)choices[strategy] + (int)strategy.Item2;
            }
        }

        enum Shape
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }

        enum Result
        {
            Lose = 0,
            Win = 6,
            Draw = 3
        }
    }
}