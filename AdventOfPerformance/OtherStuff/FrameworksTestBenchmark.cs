using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AdventOfPerformance;

[SimpleJob(RuntimeMoniker.Net481)]
[SimpleJob(RuntimeMoniker.Net50)]
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
public class FrameworksTestBenchmark
{
    private static readonly string[] input = File.ReadAllLines("input.2000.01.txt");

    [Benchmark]
    public int LinqQuery()
    {
        var query = from i in input
                    let x = Convert.ToInt32(i)
                    from j in input
                    let y = Convert.ToInt32(j)
                    where x + y == 2020
                    select x * y;

        return query.First();
    }
}
