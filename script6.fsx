let day (fish:Map<int, int64>) =
    fish
    |> Seq.toList
    |> List.map (fun kvp -> kvp.Key, kvp.Value)
    |> List.map (fun (k, c) ->
        match k with
        | 0 -> [6, c; 8,c]
        | x -> [x-1, c]
        )
    |> List.concat
    |> List.groupBy (fun (k, c) -> k)
    |> List.map     (fun (k, g) -> k, g |> List.map snd |> List.sum)
    |> Map.ofList

let fishi =
    // "3,4,3,1,2".Split(",")
    System.IO.File.ReadAllText("input61.txt").Split(",")
    |> List.ofArray
    |> List.map int
    |> List.groupBy id
    |> List.map (fun (k, l) -> k, l.Length |> int64)
    |> Map.ofList

let getOrDefault (m:Map<int,int64>) (k:int) (d:int64) =
    match m.TryFind k with
    | Some v -> v
    | None   -> d

let mutable fish =
    [0..8]
    |> List.map (fun x -> x, getOrDefault fishi x 0)
    |> Map.ofList

for _ in [1..80] do
    fish <- day fish

printfn  "%A" (Map.values fish |> Seq.sum )

let mutable fish2 =
    [0..8]
    |> List.map (fun x -> x, getOrDefault fishi x 0)
    |> Map.ofList

for _ in [1..256] do
    fish2 <- day fish2

printfn  "%A" (Map.values fish2 |> Seq.sum )