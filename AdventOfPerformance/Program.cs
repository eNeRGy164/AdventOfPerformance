using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

var config = DefaultConfig.Instance
    .AddAnalyser(EnvironmentAnalyser.Default)
    .WithSummaryStyle(SummaryStyle.Default)
    .WithOrderer(new DefaultOrderer(SummaryOrderPolicy.SlowestToFastest, MethodOrderPolicy.Declared))
    .AddColumn(StatisticColumn.Median)
    .HideColumns(Column.StdDev, Column.Error)
    .AddDiagnoser(MemoryDiagnoser.Default);

BenchmarkSwitcher
    .FromAssembly(typeof(Program).Assembly)
    .Run(args, config);
