namespace Puzzles.TwentyTwentyTwo
{
    public class Day6 : IPuzzle
    {
        public string SolveFirst(string[] input)
        {
            var signal = input.First();
            var device = new Device(signal);

            return device.ProcessedCharactersForMarker.ToString();
        }

        public string SolveSecond(string[] input)
        {
            var signal = input.First();
            var device = new Device(signal);

            return device.ProcessedCharactersForMessage.ToString();
        }

        class Device
        {
            readonly string signal;
            const int MarkerLength = 4;
            public string Marker { get; }
            public int ProcessedCharactersForMarker { get; private set; }
            const int MessageLength = 14;
            public string Message { get; }
            public int ProcessedCharactersForMessage { get; private set; }
            public Device(string signal)
            {
                this.signal = signal;
                Marker = FindMarker();
                Message = FindMessage();
            }

            string FindMarker()
            {
                for ( int i = MarkerLength; i < signal.Length; i++)
                {
                    var markerStart = i - MarkerLength;
                    var markerCandidate = signal[markerStart..i];

                    if (markerCandidate.ToHashSet().Count == MarkerLength)
                    {
                        ProcessedCharactersForMarker = i;
                        return markerCandidate;
                    }
                }

                throw new ArgumentException("No marker found.");
            }

            string FindMessage()
            {
                for ( int i = MessageLength; i < signal.Length; i++)
                {
                    var messageStart = i - MessageLength;
                    var messageCandidate = signal[messageStart..i];

                    if (messageCandidate.ToHashSet().Count == MessageLength)
                    {
                        ProcessedCharactersForMessage = i;
                        return messageCandidate;
                    }
                }

                throw new ArgumentException("No message found.");
            }
        }
    }
}