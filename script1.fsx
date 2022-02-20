open System

open System.IO

File.ReadAllLines("input1.1.txt")
|> List.ofArray
|> List.map int
|> List.pairwise
|> List.filter (fun (p, n) -> n - p > 0 )
|> List.length
|> printfn "%A"

File.ReadAllLines("input1.2.txt")
|> List.ofArray
|> List.map int
|> List.windowed 3
|> List.map (fun win -> List.sum win)
|> List.pairwise
|> List.filter (fun (p, n) -> n - p > 0)
|> List.length
|> printfn "%A"

let ds = 
    File.ReadAllLines("input2.1.txt")
    |> List.ofArray
    |> List.map (fun l -> l.Split(" "))
    |> List.map (fun x -> x.[0], x.[1] |> int)
    |> List.groupBy fst
    |> List.map (fun (k, g) -> k, g |> List.sumBy snd)
    |> Map.ofList
(ds.["down"] - ds.["up"]) * ds.["forward"] |> printfn "%A"

let move (h, d, a) (c, v) =
    match c with
    | "down"    -> h, d, a + v
    | "up"      -> h, d, a - v
    | "forward" -> h+v, d+a*v, a

let h, d, a =
    File.ReadAllLines("input2.2.txt")
    |> List.ofArray
    |> List.map (fun l -> l.Split(" "))
    |> List.map (fun x -> x.[0], x.[1] |> int)
    |> List.fold (fun s m -> move s m) (0, 0, 0) // horizontal, depth, aim

h * d |> printfn "%A"




