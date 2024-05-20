// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<EnumeratedCollectionBenchmarks>();

Console.WriteLine("Press any key to exit...");
Console.ReadKey();