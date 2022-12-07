using System.Collections;

namespace Puzzles.TwentyTwentyTwo
{
    public class Day7 : IPuzzle
    {
        public string SolveFirst(string[] input)
        {
            var parser = new Parser();
            var fileSystem = parser.CreateFileSystem(input);

            return fileSystem.Nodes.Where(x => x is Directory).Where(x => x.Size <= 100000).Sum(x => x.Size).ToString();
        }
        
        public string SolveSecond(string[] input)
        {
            var parser = new Parser();
            var fileSystem = parser.CreateFileSystem(input);
            
            var totalSpace = 70000000;
            var requiredSpace = 30000000;

            var availableSpace = totalSpace - fileSystem.TopDirectory.Size;
            var spaceToFreeUp = requiredSpace - availableSpace;

            var directory = fileSystem.Nodes.Where(x => x is Directory).Where(x => x.Size >= spaceToFreeUp).OrderBy(x => x.Size).First();

            return directory.Size.ToString();
        }

        class Parser
        {
            int lineNumber;

            FileSystem fileSystem = new();
            public FileSystem CreateFileSystem(string[] input)
            {
                var activeDirectory = new Directory("");

                while (lineNumber < input.Count())
                {
                    var line = input[lineNumber];
                    if (line.First() != '$') continue;
                    var command = line[2..4];

                    activeDirectory = command switch
                    {
                        "cd" => CreateDirectoryCommand(line, activeDirectory),
                        "ls" => ListCommand(input, activeDirectory),
                        _ => throw new Exception($"Unknown command {command} on line {lineNumber}.")
                    };
                }

                while (activeDirectory.Parent is not null)
                {
                    activeDirectory = activeDirectory.Parent;
                }
                fileSystem.TopDirectory = activeDirectory;
                return fileSystem;
            }

            private Directory ListCommand(string[] input, Directory activeDirectory)
            {
                lineNumber++;

                while (lineNumber < input.Count())
                {
                    var result = input[lineNumber].Split(" ");
                    if (result[0] == "dir")
                    {
                        var dir = new Directory(result[1]);
                        dir.SetParent(activeDirectory);
                        activeDirectory.AddChild(dir);
                        fileSystem.Nodes.Add(dir);
                    }
                    else if (int.TryParse(result[0], out var size))
                    {
                        var file = new File(result[1], size);
                        file.SetParent(activeDirectory);
                        activeDirectory.AddChild(file);
                        fileSystem.Nodes.Add(file);
                    }
                    else if (result[0] == "$")
                    {
                        break;
                    }
                    else
                    {
                        throw new Exception($"Unknown list command result {input[lineNumber]}");
                    }
                    lineNumber++;
                }

                return activeDirectory;
            }

            private Directory CreateDirectoryCommand(string line, Directory activeDirectory)
            {
                lineNumber++;

                var argument = line[5..];
                if (argument == "/")
                {
                    return new Directory(argument);
                }
                if (argument == "..")
                {
                    return activeDirectory.Parent ?? throw new Exception($"The node {activeDirectory.Name} does not have a parent.");
                }

                return activeDirectory.Children.SingleOrDefault(x => x.Name == argument) as Directory ?? throw new Exception($"The node {activeDirectory.Name} does not have a child directory with name {argument}.");
            }
        }

        interface INode
        {
            string Name { get; }
            int Size { get; }
            Directory? Parent { get; }
            List<INode> Children { get; }
            void SetParent(Directory node);
        }

        class Directory : INode
        {
            public string Name { get; }
            public int Size => Children?.Sum(x => x.Size) ?? 0;
            public Directory? Parent { get; private set; }
            public List<INode> Children { get; } = new();

            public Directory(string name)
            {
                Name = name;
            }

            public void AddChild(INode node)
            {
                Children.Add(node);
            }

            public void SetParent(Directory node)
            {
                Parent = node;
            }
        }

        class File : INode
        {
            public string Name { get; }
            public int Size { get; }

            public Directory? Parent { get; private set; }
            public List<INode> Children => throw new NotImplementedException();

            public File(string name, int size)
            {
                Name = name;
                Size = size;
            }

            public void SetParent(Directory node)
            {
                Parent = node;
            }
        }

        class FileSystem
        {
            public Directory TopDirectory { get; set;}
            public List<INode> Nodes { get; set; } = new();
        }
    }
}