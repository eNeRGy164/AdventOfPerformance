#if NET5_0_OR_GREATER
using BenchmarkDotNet.Attributes;

namespace AdventOfPerformance;

public class CreateObjectsInHashSetBenchmark
{
    const int Capacity = 10_000;

    static readonly Random random = new Random(42);
    static readonly int[] ints = new int[2 * Capacity];

    static CreateObjectsInHashSetBenchmark()
    {
        for (var i = 0; i < 2 * Capacity; i++)
        {
            ints[i] = random.Next(-1_000_000, 1_000_000);
        }
    }

    [Benchmark(Baseline = true, Description = "Class w/ properties assigned in ctor")]
    public int Class()
    {
        var set = new HashSet<Class>(Capacity);

        for (var i = 0; i < Capacity; i += 2)
        {
            var o = new Class(ints[i], ints[i + 1]);
            set.Add(o);
        }

        return set.Count;
    }

    [Benchmark(Description = "Sealed Class w/ properties assigned in ctor")]
    public int ClassSealed()
    {
        var set = new HashSet<ClassSealed>(Capacity);
        
        for (var i = 0; i < Capacity; i += 2)
        {
            var o = new ClassSealed(ints[i], ints[i + 1]);
            set.Add(o);
        }

        return set.Count;
    }

    [Benchmark(Description = "Class w/ fields assigned in the ctor")]
    public int ClassFields()
    {
        var set = new HashSet<ClassFields>(Capacity);

        for (var i = 0; i < Capacity; i += 2)
        {
            var o = new ClassFields(ints[i], ints[i + 1]);
            set.Add(o);
        }

        return set.Count;
    }

    [Benchmark(Description = "Record Class w/ properties assigned via primary ctor")]
    public int RecordClass()
    {
        var set = new HashSet<RecordClass>(Capacity);

        for (var i = 0; i < Capacity; i += 2)
        {
            var o = new RecordClass(ints[i], ints[i + 1]);
            set.Add(o);
        }

        return set.Count;
    }

    [Benchmark(Description = "Sealed Record Class w/ properties assigned via the primary ctor")]
    public int RecordClassSealed()
    {
        var set = new HashSet<RecordClassSealed>(Capacity);

        for (var i = 0; i < Capacity; i += 2)
        {
            var o = new RecordClassSealed(ints[i], ints[i + 1]);
            set.Add(o);
        }

        return set.Count;
    }

    [Benchmark(Description = "Readonly Record Struct w/ properties assigned via primary ctor")]
    public int RecordStructReadOnly()
    {
        var set = new HashSet<RecordStructReadOnly>(Capacity);

        for (var i = 0; i < Capacity; i += 2)
        {
            var o = new RecordStructReadOnly(ints[i], ints[i + 1]);
            set.Add(o);
        }

        return set.Count;
    }

    [Benchmark(Description = "Readonly Record Class w/ '[in]' properties assigned via primary ctor")]
    public int RecordStructReadOnlyIn()
    {
        var set = new HashSet<RecordStructReadOnlyIn>(Capacity);

        for (var i = 0; i < Capacity; i += 2)
        {
            var o = new RecordStructReadOnlyIn(ints[i], ints[i + 1]);
            set.Add(o);
        }

        return set.Count;
    }

    [Benchmark(Description = "Record Struct w/ properties assigned via primary ctor")]
    public int RecordStruct()
    {
        var set = new HashSet<RecordStruct>(Capacity);

        for (var i = 0; i < Capacity; i += 2)
        {
            var o = new RecordStruct(ints[i], ints[i + 1]);
            set.Add(o);
        }

        return set.Count;
    }

    [Benchmark(Description = "Struct w/ properties assigned in the ctor")]
    public int Struct()
    {
        var set = new HashSet<Struct>(Capacity);

        for (var i = 0; i < Capacity; i += 2)
        {
            var o = new Struct(ints[i], ints[i + 1]);
            set.Add(o);
        }

        return set.Count;
    }

    [Benchmark(Description = "Readonly Struct w/ properties assigned in ctor")]
    public int StructReadOnly()
    {
        var set = new HashSet<StructReadOnly>(Capacity);

        for (var i = 0; i < Capacity; i += 2)
        {
            var o = new StructReadOnly(ints[i], ints[i + 1]);
            set.Add(o);
        }

        return set.Count;
    }

    [Benchmark(Description = "Struct w/ readonly fields assigned in ctor")]
    public int StructReadOnlyFields()
    {
        var set = new HashSet<StructReadOnlyFields>(Capacity);

        for (var i = 0; i < Capacity; i += 2)
        {
            var o = new StructReadOnlyFields(ints[i], ints[i + 1]);
            set.Add(o);
        }

        return set.Count;
    }

    [Benchmark(Description = "Readonly Struct w/ '[in]' properties assigned in ctor")]
    public int StructReadOnlyFieldsIn()
    {
        var set = new HashSet<StructReadOnlyFieldsIn>(Capacity);

        for (var i = 0; i < Capacity; i += 2)
        {
            var o = new StructReadOnlyFieldsIn(ints[i], ints[i + 1]);
            set.Add(o);
        }

        return set.Count;
    }
}

public class Class
{
    public int X { get; init; }
    public int Y { get; init; }

    public Class(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class ClassFields
{
    public int X;
    public int Y;

    public ClassFields(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public sealed class ClassSealed
{
    public int X { get; init; }
    public int Y { get; init; }

    public ClassSealed(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public record class RecordClass(int X, int Y);

public sealed record class RecordClassSealed(int X, int Y);

public record struct RecordStruct(int X, int Y);

public readonly record struct RecordStructReadOnly(int X, int Y);

public readonly record struct RecordStructReadOnlyIn(in int X, in int Y);

public struct Struct
{
    public int X { get; init; }
    public int Y { get; init; }

    public Struct(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public readonly struct StructReadOnly
{
    public int X { get; init; }
    public int Y { get; init; }

    public StructReadOnly(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public readonly struct StructReadOnlyFields
{
    public readonly int X;
    public readonly int Y;

    public StructReadOnlyFields(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public readonly struct StructReadOnlyFieldsIn
{
    public readonly int X;
    public readonly int Y;

    public StructReadOnlyFieldsIn(in int x, in int y)
    {
        X = x;
        Y = y;
    }
}
#endif