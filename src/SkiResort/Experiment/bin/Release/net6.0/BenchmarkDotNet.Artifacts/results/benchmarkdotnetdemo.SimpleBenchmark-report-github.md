``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1706 (21H1/May2021Update)
Intel Core i5-8259U CPU 2.30GHz (Coffee Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.201
  [Host] : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT  [AttachedDebugger]

Job=InProcess  Toolchain=InProcessEmitToolchain  

```
|       Method |      Mean |     Error |    StdDev |    StdErr |    Median |    Min |       Max |        Q1 |        Q3 |             Op/s | Rank | Allocated |
|------------- |----------:|----------:|----------:|----------:|----------:|-------:|----------:|----------:|----------:|-----------------:|-----:|----------:|
|     NoopTest | 0.0227 ns | 0.0240 ns | 0.0224 ns | 0.0058 ns | 0.0201 ns | 0.0 ns | 0.0760 ns | 0.0015 ns | 0.0342 ns | 44,059,357,820.7 |    1 |         - |
|      AddTest | 0.0151 ns | 0.0247 ns | 0.0231 ns | 0.0060 ns | 0.0033 ns | 0.0 ns | 0.0809 ns | 0.0000 ns | 0.0271 ns | 66,199,666,743.7 |    1 |         - |
| MultiplyTest | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |         Infinity |    1 |         - |
