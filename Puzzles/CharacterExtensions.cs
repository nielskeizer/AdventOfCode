namespace Puzzles.TwentyTwentyTwo
{
    public static class CharacterExtensions
    {
        
            public static int Priority(this char x)
            {
                if (char.IsLower(x))
                {
                    return (int)x - (int)'a' + 1;
                }
                if (char.IsUpper(x))
                {
                    return (int)x - (int)'A' + 27;
                }

                throw new ArgumentException($"{x} is not a valid letter of the alphabet.");
            }
    }
}