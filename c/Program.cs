using System;
using System.IO;
using System.Linq;

Console.WriteLine("hello");

var fish =
    File.ReadAllText("../input61.txt")
    .Split(",")
    .Select(int.Parse)
    .ToList();

void day(List<int> fish)
{
    var fc = fish.Count;
    for (int j = 0; j < fc; j++)
    {
        if (fish[j] == 0) {
            fish[j] = 6;
            fish.Add(8);
        } else {
            fish[j]--;
        }
    }
}

void day2(List<byte> fish)
{
    var fc = fish.Count;
    for (int j = 0; j < fc; j++)
    {
        if (fish[j] == 0) {
            fish[j] = 6;
            fish.Add(8);
        } else {
            fish[j]--;
        }
    }
}

// for (int i = 0; i < 80; i++)
//     day(fish);

// System.Console.WriteLine(fish.Count);

// var fish2 =
//     File.ReadAllText("../input61.txt")
//     .Split(",")
//     .Select(int.Parse)
//     .ToList();

// var d = 80;

// var sum = 0;
// for (int i = 0; i < 1; i++)
// {
//     var s = fish2[i];
//     System.Console.WriteLine((6 - fish2[i] + d ) / 7);
//     sum += (6 - fish2[i] + d ) / 7;
//     int j = 0;
//     int to_add;
//     do
//     {
//         to_add = (d - 2 - s - 7 * j)/7;
//         System.Console.WriteLine(to_add);
//         if (to_add > 0) sum += to_add;
//         j++;
//     } while(to_add > 0);
// }

// // var total =
// //     fish2
// //     .Select(x => Math.Pow(2, (6-x+d)/7 ))
// //     .Sum();

// System.Console.WriteLine(sum);

var fish2 =
    File.ReadAllText("../input61.txt")
    .Split(",")
    .Select(byte.Parse)
    .ToList();

var sum = 0;
var z = 0;
foreach (var item in fish2)
{
    var singleflist = new List<byte>(15000000);
    singleflist.Add(item);
    for (int i = 0; i < 256; i++) {
        day2(singleflist);
        if (i%10==0) System.Console.WriteLine($"day={i}");
    }
    sum += singleflist.Count;
    System.Console.WriteLine($"{z++} / {fish2.Count}");
}

System.Console.WriteLine(sum);

