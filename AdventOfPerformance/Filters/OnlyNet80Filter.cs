using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Filters;

namespace AdventOfPerformance;

public class OnlyNet80Filter : FilterConfigBaseAttribute
{
    public OnlyNet80Filter()
        : base(new SimpleFilter(f => f.GetRuntime().Name.Contains("8.0")))
    {
    }
}
