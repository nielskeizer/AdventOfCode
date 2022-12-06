namespace Puzzles.TwentyTwentyTwo
{
    public class Day4 : IPuzzle
    {
        public string SolveFirst(string[] input)
        {
            return input.Select(p => new AssignmentPairs(p)).Count(x => x.OneContainsTheOther()).ToString();
        }

        public string SolveSecond(string[] input)
        {
            return input.Select(p => new AssignmentPairs(p)).Count(x => x.Overlap()).ToString();
        }

        class AssignmentPairs
        {
            readonly Assignment firstAssignment;
            readonly Assignment secondAssignment;
            
            public AssignmentPairs(string input)
            {
                var assignmentInputs = input.Split(",");
                firstAssignment = new Assignment(assignmentInputs[0]);
                secondAssignment = new Assignment(assignmentInputs[1]);
            }

            public bool OneContainsTheOther() => firstAssignment.Contains(secondAssignment) || secondAssignment.Contains(firstAssignment);
            public bool Overlap() => firstAssignment.Overlaps(secondAssignment) || secondAssignment.Overlaps(firstAssignment);
        }

        class Assignment
        {
            public int FirstSection { get; }
            public int LastSection { get; }

            public Assignment(string input)
            {
                var sections = input.Split("-");
                FirstSection = int.Parse(sections[0]);
                LastSection = int.Parse(sections[1]);
            }

            public bool Contains(Assignment assignment) => FirstSection <= assignment.FirstSection && LastSection >= assignment.LastSection;
            public bool Overlaps(Assignment assignment) => (FirstSection <= assignment.FirstSection && assignment.FirstSection <= LastSection)
                                                       || (FirstSection <= assignment.LastSection && assignment.LastSection <= LastSection);
        }
    }
}