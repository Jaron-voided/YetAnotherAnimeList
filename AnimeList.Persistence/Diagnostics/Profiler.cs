using System.Diagnostics;

namespace AnimeList.Persistence.Diagnostics;

public class Profiler
{
     private static readonly Stack<ProfileNode> _activeStack = new();
    private static readonly Dictionary<string, ProfileNode> _rootNodes = new();
    private static int _defaultBufferSize = 128;

    public static void SetDefaultBufferSize(int size)
    {
        if (size <= 0)
            throw new ArgumentException("Buffer size must be positive", nameof(size));
        // is size a power of 2?
        if ((size & (size - 1)) != 0) 
            throw new ArgumentException("Buffer size must be a power of 2", nameof(size));
        _defaultBufferSize = size;
    }

    public static void Begin(string name)
    {
        var node = GetOrCreateNode(name);
        node.Start();
        _activeStack.Push(node);
    }

    public static void End()
    {
        if (_activeStack.Count == 0)
            throw new InvalidOperationException("No active profiling session to end");

        var node = _activeStack.Pop();
        node.Stop();
    }

    public static void Reset()
    {
        _activeStack.Clear();
        _rootNodes.Clear();
    }

    public static void ToConsole()
    {
        if (_rootNodes.Count == 0)
        {
            Console.WriteLine("No profiling data available.");
            return;
        }

        int maxNameWidth = CalculateMaxNameWidth(_rootNodes.Values, 0);
        maxNameWidth = Math.Max(maxNameWidth, 20);

        Console.WriteLine();
        Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════");
        Console.WriteLine("                                          PROFILER RESULTS");
        Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════");
        Console.WriteLine();

        foreach (var rootNode in _rootNodes.Values)
        {
            PrintNode(rootNode, 0, maxNameWidth);
        }

        Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════");
        PrintSummary();
        Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════");
        Console.WriteLine();
    }

    private static int CalculateMaxNameWidth(IEnumerable<ProfileNode> nodes, int depth)
    {
        int maxWidth = 0;
        foreach (var node in nodes)
        {
            int indent = depth * 2;
            int prefix = depth > 0 ? 3 : 0;
            int nameWidth = indent + prefix + node.Name.Length;
            maxWidth = Math.Max(maxWidth, nameWidth);

            if (node.Children.Count > 0)
            {
                int childMaxWidth = CalculateMaxNameWidth(node.Children.Values, depth + 1);
                maxWidth = Math.Max(maxWidth, childMaxWidth);
            }
        }
        return maxWidth;
    }

    private static void PrintNode(ProfileNode node, int depth, int maxNameWidth)
    {
        string indent = new string(' ', depth * 2);
        string prefix = depth > 0 ? "├─ " : "";
        string nameWithPrefix = $"{indent}{prefix}{node.Name}";
        
        if (node.Count > 0)
        {
            double avg = node.GetAverage();
            double min = node.GetMin();
            double max = node.GetMax();
            int count = node.Count;
            long totalCalls = node.TotalCalls;
            double totalTime = node.TotalTimeMilliseconds;

            string timeUnit = "ms";
            double avgDisplay = avg;
            double minDisplay = min;
            double maxDisplay = max;

            if (avg < 1.0)
            {
                timeUnit = "μs";
                avgDisplay *= 1000;
                minDisplay *= 1000;
                maxDisplay *= 1000;
            }

            string totalTimeUnit = "ms";
            double totalTimeDisplay = totalTime;
            if (totalTime >= 1000)
            {
                totalTimeUnit = "s";
                totalTimeDisplay = totalTime / 1000.0;
            }

            Console.WriteLine($"{nameWithPrefix.PadRight(maxNameWidth + 2)}" +
                            $"Calls: {totalCalls,8}  " +
                            $"Samples: {count,6}  " +
                            $"Total: {totalTimeDisplay,10:F2} {totalTimeUnit}  " +
                            $"Avg: {avgDisplay,10:F2} {timeUnit}  " +
                            $"Min: {minDisplay,10:F2} {timeUnit}  " +
                            $"Max: {maxDisplay,10:F2} {timeUnit}");
        }
        else
        {
            Console.WriteLine($"{nameWithPrefix.PadRight(maxNameWidth + 2)}(no data)");
        }

        foreach (var child in node.Children.Values)
        {
            PrintNode(child, depth + 1, maxNameWidth);
        }
    }

    private static void PrintSummary()
    {
        var nodesByName = new Dictionary<string, List<(ProfileNode node, int depth)>>();
        
        foreach (var rootNode in _rootNodes.Values)
        {
            CollectNodesByName(rootNode, 0, nodesByName);
        }

        var summaryData = new List<(string name, double totalTime, long totalCalls, int maxDepth)>();
        double grandTotalTime = 0;

        foreach (var kvp in nodesByName)
        {
            int maxDepth = 0;
            double totalTime = 0;
            long totalCalls = 0;

            foreach (var (node, depth) in kvp.Value)
            {
                if (depth > maxDepth)
                {
                    maxDepth = depth;
                    totalTime = node.TotalTimeMilliseconds;
                    totalCalls = node.TotalCalls;
                }
                else if (depth == maxDepth)
                {
                    totalTime += node.TotalTimeMilliseconds;
                    totalCalls += node.TotalCalls;
                }
            }

            summaryData.Add((kvp.Key, totalTime, totalCalls, maxDepth));
            grandTotalTime += totalTime;
        }

        summaryData.Sort((a, b) => b.totalTime.CompareTo(a.totalTime));

        Console.WriteLine();
        Console.WriteLine("                                    TIME DISTRIBUTION SUMMARY");
        Console.WriteLine();

        if (grandTotalTime > 0)
        {
            foreach (var (name, totalTime, totalCalls, maxDepth) in summaryData)
            {
                double percentage = (totalTime / grandTotalTime) * 100.0;
                
                string totalTimeUnit = "ms";
                double totalTimeDisplay = totalTime;
                if (totalTime >= 1000)
                {
                    totalTimeUnit = "s";
                    totalTimeDisplay = totalTime / 1000.0;
                }

                Console.WriteLine($"  {name,-40} {percentage,6:F2}%   " +
                                $"Total: {totalTimeDisplay,10:F2} {totalTimeUnit}   " +
                                $"Calls: {totalCalls,8}   " +
                                $"Depth: {maxDepth}");
            }
        }
        else
        {
            Console.WriteLine("  No timing data available.");
        }

        Console.WriteLine();
    }

    private static void CollectNodesByName(ProfileNode node, int depth, Dictionary<string, List<(ProfileNode, int)>> nodesByName)
    {
        if (!nodesByName.TryGetValue(node.Name, out var list))
        {
            list = new List<(ProfileNode, int)>();
            nodesByName[node.Name] = list;
        }
        list.Add((node, depth));

        foreach (var child in node.Children.Values)
        {
            CollectNodesByName(child, depth + 1, nodesByName);
        }
    }

    private static ProfileNode GetOrCreateNode(string name)
    {
        ProfileNode node;

        if (_activeStack.Count == 0)
        {
            // Root level node
            if (!_rootNodes.TryGetValue(name, out node!))
            {
                node = new ProfileNode(name, _defaultBufferSize);
                _rootNodes[name] = node;
            }
        }
        else
        {
            // Child node
            var parent = _activeStack.Peek();
            node = parent.GetOrCreateChild(name);
        }

        return node;
    }

    internal class ProfileNode
    {
        private readonly string _name;
        private readonly CircularBuffer _timings;
        private readonly Dictionary<string, ProfileNode> _children = new(16);
        private long _startTimestamp;
        private long _totalCalls;
        private double _totalTimeMilliseconds;

        public string Name => _name;
        public IReadOnlyDictionary<string, ProfileNode> Children => _children;

        public ProfileNode(string name, int bufferSize)
        {
            _name = name;
            _timings = new CircularBuffer(bufferSize);
        }

        public void Start()
        {
            _startTimestamp = Stopwatch.GetTimestamp();
            _totalCalls++;
        }

        public void Stop()
        {
            long endTimestamp = Stopwatch.GetTimestamp();
            double elapsedMilliseconds = (endTimestamp - _startTimestamp) * 1000.0 / Stopwatch.Frequency;
            _timings.Add(elapsedMilliseconds);
            _totalTimeMilliseconds += elapsedMilliseconds;
        }

        public ProfileNode GetOrCreateChild(string name)
        {
            if (!_children.TryGetValue(name, out var child))
            {
                child = new ProfileNode(name, _timings.Capacity);
                _children[name] = child;
            }
            return child;
        }

        public double GetAverage() => _timings.GetAverage();
        public double GetMin() => _timings.GetMin();
        public double GetMax() => _timings.GetMax();
        public int Count => _timings.Count;
        public long TotalCalls => _totalCalls;
        public double TotalTimeMilliseconds => _totalTimeMilliseconds;
    }

    internal class CircularBuffer
    {
        private readonly double[] _buffer;
        private int _head;
        private int _count;

        public int Capacity => _buffer.Length;
        public int Count => _count;

        public CircularBuffer(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be positive", nameof(capacity));
            _buffer = new double[capacity];
        }

        public void Add(double value)
        {
            _buffer[_head] = value;
            _head = (_head + 1) & (_buffer.Length - 1);
            if (_count < _buffer.Length)
                _count++;
        }

        public double GetAverage()
        {
            if (_count == 0)
                return 0;

            double sum = 0;
            for (int i = 0; i < _count; i++)
            {
                sum += _buffer[i];
            }
            return sum / _count;
        }

        public double GetMin()
        {
            if (_count == 0)
                return 0;

            double min = _buffer[0];
            for (int i = 1; i < _count; i++)
            {
                if (_buffer[i] < min)
                    min = _buffer[i];
            }
            return min;
        }

        public double GetMax()
        {
            if (_count == 0)
                return 0;

            double max = _buffer[0];
            for (int i = 1; i < _count; i++)
            {
                if (_buffer[i] > max)
                    max = _buffer[i];
            }
            return max;
        }
    }
}