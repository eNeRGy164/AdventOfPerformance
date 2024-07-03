#if NET5_0_OR_GREATER
using BenchmarkDotNet.Attributes;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AdventOfPerformance;

public partial class InputParseBenchmarks
{
    static readonly string[] input = File.ReadAllLines("input.2023.22.txt");

    [SuppressMessage("Performance", "SYSLIB1045:Convert to 'GeneratedRegexAttribute'.", Justification = "Demo")]
    static readonly Regex regex = new Regex("(\\d+),(\\d+),(\\d+)~(\\d+),(\\d+),(\\d+)");

    [Benchmark(Baseline = true)]
    public Beam[] Regex()
    {
        var beams = new Beam[input.Length];

        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];

            var groups = regex.Match(line).Groups;

            var begin = new Point3(ParseInt(groups[1].Value), ParseInt(groups[2].Value), ParseInt(groups[3].Value));
            var end = new Point3(ParseInt(groups[4].Value), ParseInt(groups[5].Value), ParseInt(groups[6].Value));

            beams[i] = new Beam(begin, end);
        };

        return beams;
    }

#if NET8_0_OR_GREATER
    [GeneratedRegex("^(\\d),(\\d),(\\d{1,3})~(\\d),(\\d),(\\d{1,3})$")]
    private static partial Regex GeneratedRegex();

    [Benchmark, OnlyNet80Filter]
    public Beam[] RegexGenerated()
    {
        var beams = new Beam[input.Length];
        var regex = GeneratedRegex();

        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];

            var groups = regex.Match(line).Groups;

            var begin = new Point3(ParseInt(groups[1].Value), ParseInt(groups[2].Value), ParseInt(groups[3].Value));
            var end = new Point3(ParseInt(groups[4].Value), ParseInt(groups[5].Value), ParseInt(groups[6].Value));

            beams[i] = new Beam(begin, end);
        };

        return beams;
    }
#endif

    [Benchmark]
    public Beam[] CharSplit()
    {
        var beams = new Beam[input.Length];

        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];

            var parts = line.Split('~');

            var beginParts = parts[0].Split(',').Select(i => ParseInt(i)).ToArray();
            var endParts = parts[1].Split(',').Select(i => ParseInt(i)).ToArray();

            var begin = new Point3(beginParts[0], beginParts[1], beginParts[2]);
            var end = new Point3(endParts[0], endParts[1], endParts[2]);

            beams[i] = new Beam(begin, end);
        };

        return beams;
    }

    [Benchmark]
    public Beam[] CharsSplit()
    {
        var beams = new Beam[input.Length];

        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];

            var numbers = line.Split(',', '~').Select(i => ParseInt(i)).ToArray();

            var begin = new Point3(numbers[0], numbers[1], numbers[2]);
            var end = new Point3(numbers[3], numbers[4], numbers[5]);

            beams[i] = new Beam(begin, end);
        };

        return beams;
    }

    [Benchmark]
    public Beam[] Spans()
    {
        var beams = new Beam[input.Length];

        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i].AsSpan();

            var separator = line.IndexOf('~');

            var firstPart = line[..separator];
            var secondPart = line[(separator + 1)..];

            var begin = new Point3(ParseNextInt(ref firstPart), ParseNextInt(ref firstPart), ParseNextInt(ref firstPart));
            var end = new Point3(ParseNextInt(ref secondPart), ParseNextInt(ref secondPart), ParseNextInt(ref secondPart));

            beams[i] = new Beam(begin, end);
        }

        return beams;
    }

    static int ParseNextInt(ref ReadOnlySpan<char> input)
    {
        var commaIndex = input.IndexOf(',');
        if (commaIndex == -1)
        {
            commaIndex = input.Length;
        }

        var value = ParseInt(input[..commaIndex]);

        if (commaIndex < input.Length)
        {
            input = input[(commaIndex + 1)..];
        }

        return value;
    }

    [Benchmark]
    public Beam[] SpansAnalyzeInput()
    {
        var beams = new Beam[input.Length];

        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i].AsSpan();

            var separator = line.IndexOf('~');

            var firstPart = line[..separator];
            var secondPart = line[(separator + 1)..];

            var begin = new Point3(firstPart[0] - 48, firstPart[2] - 48, ParseInt(firstPart[4..]));
            var end = new Point3(secondPart[0] - 48, secondPart[2] - 48, ParseInt(secondPart[4..]));

            beams[i] = new Beam(begin, end);
        };

        return beams;
    }

    public readonly record struct Point3(in int X, in int Y, in int Z);
    public readonly record struct Beam(in Point3 Begin, in Point3 End);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int ParseInt(string input)
    {
        var result = 0;

        for (var i = 0; i < input.Length; i++)
        {
            result = result * 10 + (input[i] - '0');
        }

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int ParseInt(in ReadOnlySpan<char> input)
    {
        var result = 0;

        for (var i = 0; i < input.Length; i++)
        {
            result = result * 10 + (input[i] - '0');
        }

        return result;
    }
}
#endif