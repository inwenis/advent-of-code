let mutable fish =
    // "3,4,3,1,2".Split(",")
    System.IO.File.ReadAllText("input61.txt").Split(",")
    |> List.ofArray
    |> List.map int

let day (fish:int list) = [
    for f in fish do
        match f with
        | 0 -> yield! [6;8]
        | _ -> yield f - 1
    ]

for _ in [1..80] do
    fish <- day fish

printfn  "%A" fish.Length

for _ in [1..256] do
    fish <- day fish