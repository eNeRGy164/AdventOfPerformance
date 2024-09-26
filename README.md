# A Season for Speed: Turning Puzzles into C# Performance Win

Welcome to the repository accompanying my conference talk "[A Season for Speed: Turning Puzzles into C# Performance Win](https://sessionize.com/s/michael-hompus/a-season-for-speed-turning-puzzles-into-c-performa/85804)".
This project showcases my journey of solving [Advent of Code](https://adventofcode.com/) puzzles while learning and applying C# performance optimization techniques.

## Getting started

### Prerequisites

Ensure you have the following installed:

For most benchmarks:

- .NET 9.0 SDK

For the cross-framework comparison benchmark:

- .NET 9.0 SDK
- .NET 8.0 SDK
- .NET 7.0 SDK
- .NET 6.0 SDK
- .NET 5.0 SDK
- .NET Framework 4.8.1 SDK

### Cloning the repository

```bash
git clone https://github.com/eNeRGy164/AdventOfPerformance
cd AdventOfPerformance
```

### Running the benchmarks

#### Native AOT

For running with the Native AOT, as I did for my talk, use:

```bash
dotnet run -c Release -f net9.0 -- --runtimes nativeaot90 --filter *
```

This takes about 16 minutes on an AMD Ryzen 5 7600.

#### Without specifying a framework

To run all the benchmarks, use the following command:

```bash
dotnet run -c Release -f net8.0 --filter *
```

This takes about 16 minutes on an AMD Ryzen 5 7600.

#### Only the puzzle benchmarks

To run all the benchmarks, use the following command:

```bash
dotnet run -c Release -f net9.0 -- --runtimes nativeaot90 --filter AdventOfPerformance.Puzzle*
```

This takes about 16 minutes on an AMD Ryzen 5 7600.

#### Only the framework benchmarks

```bash
dotnet run -c Release -f net9.0 --filter AdventOfPerformance.Framework*
```

This takes about 1.5 minutes on an AMD Ryzen 5 7600.

## Benchmark results

### Puzzle part 1

[PuzzlePart1Benchmarks.cs](./AdventOfPerformance/Puzzle/PuzzlePart1Benchmarks.cs)

```plain
BenchmarkDotNet v0.13.13-nightly.20240601.156, Windows 11 (10.0.22631.3737/23H2/2023Update/SunValley3)
AMD Ryzen 5 7600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.302
  [Host]     : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-MUAWXP : .NET 8.0.6, X64 NativeAOT AVX-512F+CD+BW+DQ+VL+VBMI

Runtime=NativeAOT 8.0  Toolchain=Latest ILCompiler  
```

| Method                | Median       | Ratio | Gen0    | Gen1   | Allocated | Alloc Ratio |
|---------------------- |-------------:|------:|--------:|-------:|----------:|------------:|
| LinqQuery             | 528,551.5 ns | 1.000 | 62.5000 |      - | 1048328 B |       1.000 |
| LinqQueryIndex        | 420,219.8 ns | 0.792 | 49.3164 |      - |  830576 B |       0.792 |
| Foreach               | 185,389.9 ns | 0.349 |  0.4883 |      - |   10472 B |       0.010 |
| ForeachInnerRange     |  13,049.5 ns | 0.025 |  5.1575 | 0.0153 |   86472 B |       0.082 |
| For                   |  10,302.7 ns | 0.019 |  0.0458 |      - |     872 B |       0.001 |
| Pointers              |   6,196.6 ns | 0.012 |       - |      - |         - |       0.000 |
| IntermediateVariables |   6,055.8 ns | 0.011 |  0.0458 |      - |     872 B |       0.001 |
| ForGoto               |   5,920.1 ns | 0.011 |  0.0458 |      - |     872 B |       0.001 |
| Refs                  |   5,567.1 ns | 0.011 |  0.0458 |      - |     824 B |       0.001 |
| InitializeArray       |   4,394.5 ns | 0.008 |  0.0458 |      - |     824 B |       0.001 |
| CustomIntParser       |   3,519.3 ns | 0.007 |  0.0458 |      - |     824 B |       0.001 |
| LowerMemory           |   3,494.3 ns | 0.007 |  0.0229 |      - |     424 B |       0.000 |
| TwoPointer            |   3,010.2 ns | 0.006 |  0.0496 |      - |     872 B |       0.001 |
| Hashing               |   1,923.7 ns | 0.004 |  0.2327 |      - |    3944 B |       0.004 |
| Bits                  |     245.4 ns | 0.000 |  0.0186 |      - |     312 B |       0.000 |

### Puzzle part 2

[PuzzlePart2Benchmarks.cs](./AdventOfPerformance/Puzzle/PuzzlePart2Benchmarks.cs)

```plain
BenchmarkDotNet v0.13.13-nightly.20240601.156, Windows 11 (10.0.22631.3737/23H2/2023Update/SunValley3)
AMD Ryzen 5 7600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.302
  [Host]     : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-MUAWXP : .NET 8.0.6, X64 NativeAOT AVX-512F+CD+BW+DQ+VL+VBMI

Runtime=NativeAOT 8.0  Toolchain=Latest ILCompiler  
```

| Method          | Median        | Ratio | Gen0      | Allocated  | Alloc Ratio |
|---------------- |--------------:|------:|----------:|-----------:|------------:|
| LinqQuery       | 18,929.987 μs | 1.000 | 2156.2500 | 36410868 B |       1.000 |
| CustomIntParser |     63.120 μs | 0.003 |         - |      824 B |       0.000 |
| TwoPointer      |      3.020 μs | 0.000 |    0.0496 |      872 B |       0.000 |

### Compare frameworks

[FrameworksTestBenchmark.cs](./AdventOfPerformance/OtherStuff/FrameworksTestBenchmark.cs)

```plain
BenchmarkDotNet v0.13.13-nightly.20240601.156, Windows 11 (10.0.22631.3737/23H2/2023Update/SunValley3)
AMD Ryzen 5 7600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.302
  [Host]               : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET Framework 4.8.1 : .NET Framework 4.8.1 (4.8.9241.0), X64 RyuJIT VectorSize=256
  .NET 5.0             : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT AVX2
  .NET 7.0             : .NET 7.0.20 (7.0.2024.26716), X64 RyuJIT AVX2
  .NET 6.0             : .NET 6.0.31 (6.0.3124.26714), X64 RyuJIT AVX2
  .NET 8.0             : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
```

| Method    | Job                  | Runtime              | Median   | Gen0     | Allocated  |
|---------- |--------------------- |--------------------- |---------:|---------:|-----------:|
| LinqQuery | .NET Framework 4.8.1 | .NET Framework 4.8.1 | 994.5 μs | 166.0156 | 1026.81 KB |
| LinqQuery | .NET 5.0             | .NET 5.0             | 491.0 μs |  62.5000 | 1023.76 KB |
| LinqQuery | .NET 7.0             | .NET 7.0             | 440.3 μs |  62.5000 | 1023.76 KB |
| LinqQuery | .NET 6.0             | .NET 6.0             | 437.1 μs |  62.5000 | 1023.76 KB |
| LinqQuery | .NET 8.0             | .NET 8.0             | 294.1 μs |  62.5000 | 1023.76 KB |

### Walking a 2D `Array`

[WalkThroughMultiDimArraysBenchmarks.cs](./AdventOfPerformance/OtherStuff/WalkThroughMultiDimArraysBenchmarks.cs)

```plain
BenchmarkDotNet v0.13.13-nightly.20240601.156, Windows 11 (10.0.22631.3737/23H2/2023Update/SunValley3)
AMD Ryzen 5 7600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.302
  [Host]     : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-MUAWXP : .NET 8.0.6, X64 NativeAOT AVX-512F+CD+BW+DQ+VL+VBMI

Runtime=NativeAOT 8.0  Toolchain=Latest ILCompiler  
```

| Method                                    | Median   | Allocated |
|------------------------------------------ |---------:|----------:|
| 'Walk through 2D Array, column by column' | 233.8 μs |         - |
| 'Walk through 2D Array, row by row'       | 138.7 μs |         - |
| 'Use enumerator of the 2D Array'          | 122.9 μs |         - |

### Writing a 2D `Array`

[SerializeMultiDimArraysBenchmarks.cs](./AdventOfPerformance/OtherStuff/SerializeMultiDimArraysBenchmarks.cs)

```plain
BenchmarkDotNet v0.13.13-nightly.20240601.156, Windows 11 (10.0.22631.3737/23H2/2023Update/SunValley3)
AMD Ryzen 5 7600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.302
  [Host]     : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-MUAWXP : .NET 8.0.6, X64 NativeAOT AVX-512F+CD+BW+DQ+VL+VBMI

Runtime=NativeAOT 8.0  Toolchain=Latest ILCompiler  
```

| Method                | Median   | Gen0     | Gen1     | Gen2     | Allocated  |
|---------------------- |---------:|---------:|---------:|---------:|-----------:|
| WriteCharYX           | 636.8 μs | 166.0156 | 166.0156 | 166.0156 | 1031.03 KB |
| WriteCharForeach      | 584.4 μs | 166.0156 | 166.0156 | 166.0156 | 1031.03 KB |
| WriteCharStringCreate | 403.1 μs | 166.5039 | 166.5039 | 166.5039 |  512.08 KB |
| WriteCharSpan         | 195.2 μs | 166.5039 | 166.5039 | 166.5039 |  512.08 KB |

### Parsing input

[InputParseBenchmarks.cs](./AdventOfPerformance/OtherStuff/InputParseBenchmarks.cs)

```plain
BenchmarkDotNet v0.13.13-nightly.20240601.156, Windows 11 (10.0.22631.3737/23H2/2023Update/SunValley3)
AMD Ryzen 5 7600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.302
  [Host]     : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-MUAWXP : .NET 8.0.6, X64 NativeAOT AVX-512F+CD+BW+DQ+VL+VBMI

Runtime=NativeAOT 8.0  Toolchain=Latest ILCompiler  
```

| Method            | Median    | Ratio | Gen0     | Gen1    | Allocated | Alloc Ratio |
|------------------ |----------:|------:|---------:|--------:|----------:|------------:|
| Regex             | 484.23 μs |  1.00 | 103.0273 | 12.6953 | 1683.3 KB |        1.00 |
| RegexGenerated    | 343.51 μs |  0.71 | 103.0273 | 12.6953 | 1683.3 KB |        1.00 |
| CharSplit         | 236.39 μs |  0.49 |  51.2695 |  6.3477 | 840.38 KB |        0.50 |
| CharsSplit        | 137.35 μs |  0.28 |  34.1797 |  4.1504 | 560.55 KB |        0.33 |
| Spans             |  32.04 μs |  0.07 |   2.1362 |       - |  35.11 KB |        0.02 |
| SpansAnalyzeInput |  10.69 μs |  0.02 |   2.1362 |       - |  35.11 KB |        0.02 |

### Creating objects and storing in a `HashSet`

[CreateObjectsInHashSetBenchmark.cs](./AdventOfPerformance/OtherStuff/CreateObjectsInHashSetBenchmark.cs)

```plain
BenchmarkDotNet v0.13.13-nightly.20240601.156, Windows 11 (10.0.22631.3737/23H2/2023Update/SunValley3)
AMD Ryzen 5 7600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.302
  [Host]     : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-MUAWXP : .NET 8.0.6, X64 NativeAOT AVX-512F+CD+BW+DQ+VL+VBMI

Runtime=NativeAOT 8.0  Toolchain=Latest ILCompiler  
```

| Method                                                                 | Median   | Ratio | Gen0    | Gen1    | Gen2    | Allocated | Alloc Ratio |
|----------------------------------------------------------------------- |---------:|------:|--------:|--------:|--------:|----------:|------------:|
| 'Class w/ properties assigned in ctor'                                 | 139.1 μs |  1.00 | 49.8047 | 49.8047 | 49.8047 | 314.64 KB |        1.00 |
| 'Struct w/ readonly fields assigned in ctor'                           | 136.5 μs |  0.98 | 49.8047 | 49.8047 | 49.8047 | 314.92 KB |        1.00 |
| 'Readonly Struct w/ properties assigned in ctor'                       | 135.6 μs |  0.97 | 49.8047 | 49.8047 | 49.8047 | 314.92 KB |        1.00 |
| 'Struct w/ properties assigned in the ctor'                            | 135.1 μs |  0.97 | 49.8047 | 49.8047 | 49.8047 | 314.92 KB |        1.00 |
| 'Sealed Class w/ properties assigned in ctor'                          | 134.5 μs |  0.96 | 49.8047 | 49.8047 | 49.8047 | 314.64 KB |        1.00 |
| 'Class w/ fields assigned in the ctor'                                 | 133.6 μs |  0.96 | 49.8047 | 49.8047 | 49.8047 | 314.64 KB |        1.00 |
| 'Readonly Struct w/ '[in]' properties assigned in ctor'                | 133.7 μs |  0.96 | 49.8047 | 49.8047 | 49.8047 | 314.92 KB |        1.00 |
| 'Record Class w/ properties assigned via primary ctor'                 | 127.3 μs |  0.91 | 49.8047 | 49.8047 | 49.8047 | 314.64 KB |        1.00 |
| 'Sealed Record Class w/ properties assigned via the primary ctor'      | 127.0 μs |  0.91 | 49.8047 | 49.8047 | 49.8047 | 314.64 KB |        1.00 |
| 'Readonly Record Class w/ '[in]' properties assigned via primary ctor' | 104.3 μs |  0.75 | 49.9268 | 49.9268 | 49.9268 | 197.45 KB |        0.63 |
| 'Readonly Record Struct w/ properties assigned via primary ctor'       | 103.6 μs |  0.74 | 49.9268 | 49.9268 | 49.9268 | 197.45 KB |        0.63 |
| 'Record Struct w/ properties assigned via primary ctor'                | 103.6 μs |  0.74 | 49.9268 | 49.9268 | 49.9268 | 197.45 KB |        0.63 |


## Contributing

Feel free to submit issues or pull requests if you have suggestions or improvements.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
