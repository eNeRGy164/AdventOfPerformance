#if NET5_0_OR_GREATER
using BenchmarkDotNet.Attributes;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AdventOfPerformance;

public class PuzzlePart1Benchmarks
{
    private static readonly string[] input = File.ReadAllLines("input.2000.01.txt");

    [Benchmark(Baseline = true)]
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

    [Benchmark]
    public int LinqQueryIndex()
    {
        var query = from i in Enumerable.Range(0, input.Length)
                    let x = Convert.ToInt32(input[i])
                    from j in Enumerable.Range(i + 1, input.Length - i - 1)
                    let y = Convert.ToInt32(input[j])
                    where x + y == 2020
                    select x * y;

        return query.First();
    }

    [Benchmark]
    public int Foreach()
    {
        var numbers = input.Select(int.Parse).ToArray();

        var result = 0;
        var i = 0;

        foreach (var x in numbers)
        {
            foreach (var y in numbers.Skip(i))
            {
                if (x + y == 2020)
                    result = x * y;
            }

            i++;
        }

        return result;
    }

    [Benchmark]
    public int ForeachInnerRange()
    {
        var numbers = input.Select(int.Parse).ToArray();

        var result = 0;
        var i = 0;

        foreach (var x in numbers)
        {
            foreach (var y in numbers[i..])
            {
                if (x + y == 2020)
                    result = x * y;
            }

            i++;
        }

        return result;
    }

    [Benchmark]
    public int For()
    {
        var numbers = input.Select(int.Parse).ToArray();

        var result = 0;

        for (var i = 0; i < numbers.Length; i++)
        {
            for (var j = i + 1; j < numbers.Length; j++)
            {
                if (numbers[i] + numbers[j] == 2020)
                {
                    result = numbers[i] * numbers[j];
                }
            }
        }

        return result;
    }

    [Benchmark]
    public int ForGoto()
    {
        var numbers = input.Select(int.Parse).ToArray();

        var result = 0;

        for (var i = 0; i < numbers.Length; i++)
        {
            for (var j = i + 1; j < numbers.Length; j++)
            {
                if (numbers[i] + numbers[j] == 2020)
                {
                    result = numbers[i] * numbers[j];
                    goto quit;
                }
            }
        }

        // Imagine more code here

    quit:
        return result;
    }

    [Benchmark]
    public int IntermediateVariables()
    {
        var numbers = input.Select(int.Parse).ToArray();

        var result = 0;

        for (var i = 0; i < numbers.Length; i++)
        {
            var a = numbers[i];

            for (var j = i + 1; j < numbers.Length; j++)
            {
                var b = numbers[j];

                if (a + b == 2020)
                {
                    result = numbers[i] * numbers[j];
                    goto quit;
                }
            }
        }

    quit:
        return result;
    }

    [Benchmark]
    public int InitializeArray()
    {
        var numbers = new int[input.Length];
        for (var i = 0; i < input.Length; i++)
        {
            numbers[i] = int.Parse(input[i].AsSpan());
        }

        for (var i = 0; i < numbers.Length; i++)
        {
            var a = numbers[i];

            for (var j = i + 1; j < numbers.Length; j++)
            {
                var b = numbers[j];

                if (a + b == 2020)
                    return a * b;
            }
        }

        return 0;
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

                if (a + b == 2020)
                    return a * b;
            }
        }

        return 0;

        static int ParseInt(string input)
        {
            var value = 0;

            for (var i = 0; i < input.Length; i++)
            {
                value = (value * 10) + (input[i] - '0');
            }

            return value;
        }
    }

    [Benchmark]
    public int LowerMemory()
    {
        var numbers = new ushort[input.Length];
        for (var i = 0; i < input.Length; i++)
        {
            numbers[i] = ParseShort(input[i]);
        }

        var result = 0;

        for (var i = 0; i < numbers.Length; i++)
        {
            var a = numbers[i];

            for (var j = i + 1; j < numbers.Length; j++)
            {
                var b = numbers[j];

                if (a + b == 2020)
                    return a * b;
            }
        }

        return result;

        static ushort ParseShort(string input)
        {
            var value = 0;

            for (var i = 0; i < input.Length; i++)
            {
                value = (value * 10) + (input[i] - '0');
            }

            return (ushort)value;
        }
    }

    [Benchmark]
    public unsafe int Pointers()
    {
        int* numbers = stackalloc int[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            numbers[i] = ParseInt(input[i]);
        }

        for (int i = 0; i < input.Length; i++)
        {
            var a = numbers[i];

            for (int j = i + 1; j < input.Length; j++)
            {
                var b = numbers[j];

                if (a + b == 2020)
                    return a * b;
            }
        }

        return 0;
    }

    [Benchmark]
    public int Refs()
    {
        var numbers = new int[input.Length];
        for (var i = 0; i < input.Length; i++)
        {
            numbers[i] = ParseInt(input[i]);
        }

        ref var numberRef = ref MemoryMarshal.GetArrayDataReference(numbers);

        for (var i = 0; i < numbers.Length; i++)
        {
            var a = Unsafe.Add(ref numberRef, i);

            for (var j = i + 1; j < numbers.Length; j++)
            {
                var b = Unsafe.Add(ref numberRef, j);

                if (a + b == 2020)
                    return a * b;
            }
        }

        return 0;
    }

    [Benchmark]
    public int Hashing()
    {
        var numbers = new HashSet<int>(input.Length);
        for (var i = 0; i < input.Length; i++)
        {
            numbers.Add(ParseInt(input[i]));
        }

        foreach (var number in numbers)
        {
            var complement = 2020 - number;

            if (numbers.Contains(complement))
                return number * complement;
        }

        return 0;
    }

    [Benchmark]
    public int Bits()
    {
        var bitArray = new BitArray(2020 + 1);

        for (var i = 0; i < input.Length; i++)
        {
            var number = ParseInt(input[i]);
            var complement = 2020 - number;

            if (bitArray[complement])
                return number * complement;

            bitArray[number] = true;
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

        var left = 0;
        var right = numbers.Length - 1;

        while (left < right)
        {
            var a = numbers[left];
            var b = numbers[right];

            var sum = a + b;
            if (sum == 2020)
                return a * b;
            else if (sum < 2020)
                left++;
            else
                right--;
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