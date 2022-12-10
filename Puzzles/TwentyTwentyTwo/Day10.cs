using System.Text;

namespace Puzzles.TwentyTwentyTwo
{
    public class Day10 : IPuzzle
    {
        public string SolveFirst(string[] input)
        {
            var cpu = new Cpu();
            var instructions = CreateInstructions(input);
            var sum = 0;
            while (instructions.Count > 0)
            {
                if (!cpu.IsExecuting)
                {
                    var instruction = instructions.Dequeue();
                    cpu.Process(instruction);
                }
                if ((cpu.Cycle % 40 == 20) && cpu.Cycle <= 220)
                {
                    sum += cpu.Cycle * cpu.Register('X');
                }
                cpu.Tick();
            }

            return sum.ToString();
        }

        public string SolveSecond(string[] input)
        {
            var cpu = new Cpu();
            var crt = new Crt();
            var instructions = CreateInstructions(input);
            while (instructions.Count > 0)
            {
                if (!cpu.IsExecuting)
                {
                    var instruction = instructions.Dequeue();
                    cpu.Process(instruction);
                }
                crt.Draw(cpu.Register('X'));
                crt.Tick();
                cpu.Tick();
            }

            return crt.Show();
        }

        Queue<IInstruction> CreateInstructions(string[] input)
        {
            return new Queue<IInstruction>(input.Select(CreateInstruction));
        }

        private static IInstruction CreateInstruction(string instruction)
        {
            return instruction[..4] switch
            {
                "noop" => new Noop(),
                "addx" => new Addx(instruction[5..]),
                _ => throw new ArgumentException($"Unknown instruction {instruction}")
            };
        }
    }

    class Crt
    {
        const int Width = 40;
        const int Height = 6;
        bool[,] pixels = new bool[Width, Height];
        int cycle = 1;
        public void Draw(int spritePosition)
        {
            var screenPosition = (cycle - 1) % (Width * Height);
            var horizontalPosition = screenPosition % Width;
            var verticalPosition = screenPosition / Width;

            if (Math.Abs(spritePosition - horizontalPosition) < 2)
            {
                pixels[horizontalPosition, verticalPosition] = true;
            }
        }

        public void Tick() => cycle++;

        public string Show()
        {
            var stringBuilder = new StringBuilder();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    stringBuilder.Append(pixels[x,y] ? "#" : ".");
                }
                if (y < Height - 1) 
                {
                    stringBuilder.Append("\n");
                }
            }
            return stringBuilder.ToString();
        }
    }

    class Cpu
    {
        readonly Dictionary<char, int> registers = new();
        IInstruction? executingInstruction;
        public bool IsExecuting => executingInstruction is not null;
        public int Cycle { get; private set; } = 1;

        public Cpu()
        {
            registers.Add('X', 1);
        }

        public void Process(IInstruction instruction)
        {
            if (!(executingInstruction is null)) throw new Exception();
            executingInstruction = instruction;
        }

        public void Tick()
        {
            if (executingInstruction is null) throw new Exception();

            executingInstruction.Execute();
            if (executingInstruction.IsFinished)
            {
                registers['X'] += executingInstruction.V;
                executingInstruction = null;
            }
            Cycle++;
        }

        public int Register(char x) => registers[x];
    }



    interface IInstruction
    {
        int Cycles { get; }
        int V { get; }
        bool IsFinished { get; }
        void Execute();
    }

    class Noop : IInstruction
    {
        public int Cycles => 1;
        public int V => 0;
        int duration = 0;
        public void Execute()
        {
            duration++;
        }

        public bool IsFinished => duration >= Cycles;
    }

    class Addx : IInstruction
    {
        public int V { get; }
        public int Cycles => 2;
        int duration = 0;
        public Addx(string V)
        {
            this.V = int.Parse(V);
        }
        public void Execute()
        {
            duration++;
        }

        public bool IsFinished => duration >= Cycles;
    }
}