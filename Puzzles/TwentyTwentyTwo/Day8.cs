namespace Puzzles.TwentyTwentyTwo
{
    public class Day8 : IPuzzle
    {
        public string SolveFirst(string[] input)
        {
            var forest = new Forest(input);
            return forest.VisibleTrees.ToString();
        }

        public string SolveSecond(string[] input)
        {
            var forest = new Forest(input);
            return forest.MaxScenicScore.ToString();
        }

        class Forest
        {
            Tree[][] trees;
            const int MaxHeight = 9;
            readonly int numRows;
            readonly int numColumns;
            public int VisibleTrees => trees.Sum(x => x.Count(y => y.IsVisible));
            public int MaxScenicScore => trees.Max(x => x.Max(y => y.ScenicScore));
            public Forest(string[] input)
            {
                trees = input.Select(x => x.Select(y => new Tree(y)).ToArray()).ToArray();
                numRows = trees.Length;
                numColumns = trees[0].Length;
                DetermineVisibility();
                DetermineViews();
            }

            void DetermineVisibility()
            {
                
                for (int i=0; i<numRows; i++)
                {
                    var leftHeight = -1;
                    for (int j=0; j<numColumns; j++)
                    {
                        var tree = trees[i][j];
                        if (leftHeight < tree.Height )
                        {
                            leftHeight = tree.Height;
                            tree.IsVisibleHorizontal = true;
                            if (leftHeight == MaxHeight) break;
                        }
                    }

                    var rightHeight = -1;
                    for (int j=numColumns-1; j>=0; j--)
                    {
                        var tree = trees[i][j];
                        if (tree.IsVisibleHorizontal) break;
                        if (rightHeight < tree.Height )
                        {
                            rightHeight = tree.Height;
                            tree.IsVisibleHorizontal = true;
                        }
                    }
                }

                for (int j=0; j<numColumns; j++)
                {
                    var topHeight = -1;
                    for (int i=0; i<numRows; i++)
                    {
                        var tree = trees[i][j];
                        if (topHeight < tree.Height )
                        {
                            topHeight = tree.Height;
                            tree.IsVisibleVertical = true;
                            if (topHeight == MaxHeight) break;
                        }
                    }

                    var bottomHeight = -1;
                    for (int i=numRows-1; i>=0; i--)
                    {
                        var tree = trees[i][j];
                        if (tree.IsVisibleVertical) break;
                        if (bottomHeight < tree.Height )
                        {
                            bottomHeight = tree.Height;
                            tree.IsVisibleVertical = true;
                        }
                    }
                }
            }

            void DetermineViews()
            {
                for (int i=0; i<numRows; i++)
                {
                    var leftPositions = Enumerable.Range(0,10).ToDictionary(x => x, x=> 0);
                    for (int j=0; j<numColumns; j++)
                    {
                        var tree = trees[i][j];
                        tree.ViewLeft = j - leftPositions[tree.Height];
                        for (int s=0; s<=tree.Height; s++)
                        {
                            leftPositions[s] = j; 
                        }
                    }

                    var rightPositions = Enumerable.Range(0,10).ToDictionary(x => x, x=> 0);
                    for (int j=0; j<numColumns; j++)
                    {
                        var tree = trees[i][numColumns - j - 1];
                        tree.ViewRight = j - rightPositions[tree.Height];
                        for (int s=0; s<=tree.Height; s++)
                        {
                            rightPositions[s] = j; 
                        }
                    }
                }

                
                for (int j=0; j<numColumns; j++)
                {
                    var upPositions = Enumerable.Range(0,10).ToDictionary(x => x, x=> 0);
                    for (int i=0; i<numRows; i++)
                    {
                        var tree = trees[i][j];
                        tree.ViewUp = i - upPositions[tree.Height];
                        for (int s=0; s<=tree.Height; s++)
                        {
                            upPositions[s] = i; 
                        }
                    }

                    var downPositions = Enumerable.Range(0,10).ToDictionary(x => x, x=> 0);
                    for (int i=0; i<numRows; i++)
                    {
                        var tree = trees[numRows - i - 1][j];
                        tree.ViewDown = i - downPositions[tree.Height];
                        for (int s=0; s<=tree.Height; s++)
                        {
                            downPositions[s] = i; 
                        }
                    }
                }
            }
        }

        class Tree
        {
            public int Height { get; }
            public bool IsVisibleHorizontal { get; set; }
            public bool IsVisibleVertical { get; set; }
            public bool IsVisible => IsVisibleHorizontal || IsVisibleVertical;
            public int ScenicScore => ViewUp * ViewDown * ViewLeft * ViewRight;
            public int ViewUp { get; set; }
            public int ViewDown { get; set; }
            public int ViewLeft { get; set; }
            public int ViewRight { get; set; }

            public Tree(char height)
            {
                this.Height = int.Parse(height.ToString());
            }
        }
    }
}