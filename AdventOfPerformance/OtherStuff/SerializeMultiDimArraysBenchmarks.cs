#if NET5_0_OR_GREATER
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Filters;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace AdventOfPerformance;

public class SerializeMultiDimArraysBenchmarks
{
    readonly static char[,] charArray = new char[512, 512];
    readonly static Random random = new Random(42);

    /// <summary>
    /// Generate random data for the tests.
    /// </summary>
    public SerializeMultiDimArraysBenchmarks()
    {
        for (var y = 0; y < 512; y++)
        {
            for (var x = 0; x < 512; x++)
            {
                charArray[y, x] = (char)random.Next('a', 'z');
            }
        }
    }

    [Benchmark]
    public string WriteCharYX()
    {
        var sb = new StringBuilder();

        for (var y = 0; y < 512; y++)
        {
            for (var x = 0; x < 512; x++)
            {
                sb.Append(charArray[y, x]);
            }
        }

        return sb.ToString();
    }

    [Benchmark]
    public string WriteCharForeach()
    {
        var sb = new StringBuilder();

        foreach (var i in charArray)
        {
            sb.Append(i);
        }

        return sb.ToString();
    }

    [Benchmark]
    public string WriteCharStringCreate()
    {
        return string.Create(512 * 512, charArray, (span, charArray) =>
        {
            for (var i = 0; i < 512 * 512; i++)
            {
                span[i] = charArray[i / 512, i % 512];
            }
        });
    }

    [Benchmark]
    [NotNet50Filter]
    public string WriteCharSpan()
    {
#if NET6_0_OR_GREATER
        var span = MemoryMarshal.CreateSpan(ref Unsafe.As<byte, char>(ref MemoryMarshal.GetArrayDataReference(charArray)), charArray.Length);

        return new string(span);
#else
        return string.Empty;
#endif
    }
}
#endif
