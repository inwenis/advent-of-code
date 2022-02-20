using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

var fishi =
    File.ReadAllText(@"C:\git\advent-of-code\input61.txt")
    .Split(",")
    .Select(int.Parse)
    .GroupBy(x => x)
    .ToDictionary(x => x.Key, x => x.LongCount());

var dict =
    Enumerable
    .Range(0, 9)
    .ToDictionary(x => x, x => 0L);

foreach(var f in fishi)
{
    dict[f.Key] = f.Value;
}

Dictionary<int, long> day(Dictionary<int, long> fish)
{
    var newDict =
        Enumerable
        .Range(0, 9)
        .ToDictionary(x => x, x => 0L);
    foreach(var kvp in fish)
    {
        if (kvp.Key == 0) {
            newDict[6] += kvp.Value;
            newDict[8] += kvp.Value;
        } else {
            newDict[kvp.Key - 1] += kvp.Value;
        }
    }
    return newDict;
}

for (int i = 0; i < 256; i++)
{
    dict = day(dict);
}

System.Console.WriteLine(dict.Values.Sum());
