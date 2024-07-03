#if NET5_0_OR_GREATER
using BenchmarkDotNet.Attributes;

namespace AdventOfPerformance;

public class WalkThroughMultiDimArraysBenchmarks
{
    readonly static int[,] intArray = new int[512, 512];
    readonly static Random random = new Random(42);

    /// <summary>
    /// Generate random data for the tests.
    /// </summary>
    public WalkThroughMultiDimArraysBenchmarks()
    {
        for (var y = 0; y < 512; y++)
        {
            for (var x = 0; x < 512; x++)
            {
                intArray[y, x] = random.Next(0, 1_000_000);
            }
        }
    }

    /// <summary>
    /// Handle array row, for row.
    /// </summary>
    [Benchmark(Description = "Walk through 2D Array, row by row")]
    public int WalkYX()
    {
        var value = 0;

        for (var y = 0; y < 512; y++)
        {
            for (var x = 0; x < 512; x++)
            {
                value += intArray[y, x];
            }
        }

        return value;
    }

    [Benchmark(Description = "Walk through 2D Array, column by column")]
    public int WalkXY()
    {
        var value = 0;

        for (var x = 0; x < 512; x++)
        {
            for (var y = 0; y < 512; y++)
            {
                value += intArray[y, x];
            }
        }

        return value;
    }

    /// <summary>
    /// Use the enumerator of the multidimensional array.
    /// </summary>
    [Benchmark(Description = "Use enumerator of the 2D Array")]
    public int WalkForeach()
    {
        var value = 0;

        foreach (var i in intArray)
        {
            value += i;
        }

        return value;
    }
}
#endif
