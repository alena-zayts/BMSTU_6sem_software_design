``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1706 (21H1/May2021Update)
Intel Core i5-8259U CPU 2.30GHz (Coffee Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT


```
| Method |     Mean |    Error |   StdDev |
|------- |---------:|---------:|---------:|
| Sha256 | 47.22 μs | 0.448 μs | 0.419 μs |
|    Md5 | 20.21 μs | 0.135 μs | 0.127 μs |
