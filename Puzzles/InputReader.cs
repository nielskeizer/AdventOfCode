using System.Reflection;

namespace Puzzles
{
    public static class InputReader
    {
        public static string[] Read(string relativeLocation)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            var goThreeDirectoriesUp = "..\\..\\..\\..\\Puzzles.Input\\";
            var path = Path.GetFullPath(directory + goThreeDirectoriesUp + relativeLocation);
            return File.ReadAllLines(path);
        }
    }
}