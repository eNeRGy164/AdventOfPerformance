#if NET5_0_OR_GREATER
using BenchmarkDotNet.Attributes;
using System.Runtime.CompilerServices;

namespace AdventOfPerformance;

public class PuzzlePart2Benchmarks
{
    private static readonly string[] input = File.ReadAllLines("input.2000.01.txt");

    [Benchmark(Baseline = true)]
    public int LinqQuery()
    {
        var query = from i in input
                    let x = Convert.ToInt32(i)
                    from j in input
                    let y = Convert.ToInt32(j)
                    from k in input
                    let z = Convert.ToInt32(k)
                    where x + y + z == 2020
                    select x * y * z;

        return query.First();
    }

    [Benchmark]
    public int CustomIntParser()
    {
        var numbers = new int[input.Length];
        for (var i = 0; i < input.Length; i++)
        {
            numbers[i] = ParseInt(input[i]);
        }

        for (var i = 0; i < numbers.Length; i++)
        {
            var a = numbers[i];

            for (var j = i + 1; j < numbers.Length; j++)
            {
                var b = numbers[j];

                for (var k = j + 1; k < numbers.Length; k++)
                {
                    var c = numbers[k];

                    if (a + b + c == 2020)
                        return a * b * c;
                }
            }
        }

        return 0;
    }

    [Benchmark]
    public int TwoPointer()
    {
        var numbers = new int[input.Length];
        for (var i = 0; i < input.Length; i++)
        {
            numbers[i] = ParseInt(input[i]);
        }

        Array.Sort(numbers);

        for (var i = 0; i < numbers.Length - 2; i++)
        {
            var a = numbers[i];

            var left = i + 1;
            var right = numbers.Length - 1;

            while (left < right)
            {
                var b = numbers[left];
                var c = numbers[right];

                var sum = a + b + c;
                if (sum == 2020)
                    return a * b * c;
                else if (sum < 2020)
                    left++;
                else
                    right--;
            }
        }

        return 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int ParseInt(string input)
    {
        var result = 0;

        for (var i = 0; i < input.Length; i++)
        {
            result = (result * 10) + (input[i] - '0');
        }

        return result;
    }
}
#endif