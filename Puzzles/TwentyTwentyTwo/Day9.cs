namespace Puzzles.TwentyTwentyTwo
{
    public class Day9 : IPuzzle
    {
        public string SolveFirst(string[] input)
        {
            return DistinctTailPositions(input, 2);
        }

        public string SolveSecond(string[] input)
        {
            return DistinctTailPositions(input, 10);
        }

        private static string DistinctTailPositions(string[] input, int numberOfKnots)
        {
            var tailPositions = new HashSet<(int, int)>();
            var rope = new Rope(numberOfKnots);

            foreach (var moveSeries in input)
            {
                var direction = moveSeries[0];
                var numberOfMoves = int.Parse(moveSeries[2..]);

                foreach (var move in Enumerable.Range(0, numberOfMoves))
                {
                    rope.Move(direction);
                    tailPositions.Add((rope.Tail.X, rope.Tail.Y));
                }
            }

            return tailPositions.Count().ToString();
        }

        class Rope
        {
            public Rope(int numberOfKnots)
            {
                this.tailLength = numberOfKnots-1;
                Head = new(0,0);
                Tails = Enumerable.Range(0, tailLength).ToDictionary(x => x, x => new Tail(0,0));
            }

            public Head Head { get; }
            public Tail Tail => Tails[tailLength-1];
            readonly Dictionary<int, Tail> Tails;
            readonly int tailLength;
            public void Move(char direction)
            {
                Head.Move(direction);
                Tails[0].Move(Head);
                for (int i=1; i<tailLength; i++ )
                {
                    Tails[i].Move(Tails[i-1]);
                }
            }
        }

        class Head : IKnot
        {
            public Head(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; private set; }
            public int Y { get; private set; }

            public void Move(char direction)
            {
                switch (direction)
                {
                    case 'U': Y++; break;
                    case 'D': Y--; break;
                    case 'L': X--; break;
                    case 'R': X++; break;
                    default: throw new ArgumentException($"{direction} is not a valid direction.");
                };
            }
        }

        class Tail : IKnot
        {
            public Tail(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; private set; }
            public int Y { get; private set; }

            public void Move(IKnot part)
            {
                var deltaX = X - part.X;
                var deltaY = Y - part.Y;

                if (Math.Abs(deltaX) < 2 && Math.Abs(deltaY) < 2) return;
                X = X - Math.Sign(deltaX);
                Y = Y - Math.Sign(deltaY);
            }    
        }

        interface IKnot
        {
            int X { get; }
            int Y { get; }
        }
    }
}