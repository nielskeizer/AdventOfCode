
using System.ComponentModel.Design;

namespace Puzzles.TwentyTwentyFour
{
    public class Day2 : IPuzzle
    {
        public string SolveFirst(string[] input)
        {
            var reports = CreateReports(input);

            return reports.Where(x => x.IsSafe()).Count().ToString();
        }

        public string SolveSecond(string[] input)
        {
            var reports = CreateReports(input);
            return reports.Where(x => x.IsSafeAfterDampening()).Count().ToString();
        }

        List<Report> CreateReports(string[] input)
        {
            return input
                .Select(line => 
                    new Report(line
                        .Split(' ')
                        .Select(x => int.TryParse(x, out var level) ? level : int.MaxValue)
                        .ToList()))
                .ToList();
        }

        class Report(List<int> levels)
        {
            public bool IsSafe() => IsMonotone() && IsGradual();

            public bool IsSafeAfterDampening() => IsSafe() || SubReports().Any(report => report.IsSafe());
            readonly IEnumerable<int> deltaLevels = levels.Zip(levels.Skip(1), (current, next) => next - current);
            IEnumerable<Report> SubReports()
            {
                return Enumerable.Range(0,levels.Count).Select(i => new Report(levels.Take(i).Concat(levels.Skip(i+1)).ToList()));
            }

            bool IsGradual() => deltaLevels.All(x => Math.Abs(x)>=1 && Math.Abs(x)<=3);

            bool IsMonotone() => deltaLevels.All(x => x>0) || deltaLevels.All(x => x<0);
        }
    }
}