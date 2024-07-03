using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Filters;
using BenchmarkDotNet.Jobs;

namespace AdventOfPerformance;

public class NotNet50Filter : FilterConfigBaseAttribute
{
    public NotNet50Filter()
        : base(new SimpleFilter(benchmarkCase => benchmarkCase.GetRuntime().RuntimeMoniker != RuntimeMoniker.Net50))
    {
    }
}
