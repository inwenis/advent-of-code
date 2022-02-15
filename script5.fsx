open System
open System.IO

let lines = File.ReadAllLines("input51.txt") |> Seq.toList

let hor_vert_only x1 y1 x2 y2 =
    if x1 = x2 then
        [ for y in [min y1 y2..max y1 y2] do yield x1, y ]
    else
        [ for x in [min x1 x2..max x1 x2] do yield x, y1 ]

lines
|> List.map     (fun x -> x.Split([|" -> " ; ","|], StringSplitOptions.None) |> Array.map int)
|> List.map     (fun x -> x.[0], x.[1], x.[2], x.[3])
|> List.filter  (fun (x1,y1,x2,y2) -> x1 = x2 || y1 = y2)
|> List.collect (fun (x1,y1,x2,y2) -> hor_vert_only x1 y1 x2 y2)
|> List.groupBy id
|> List.filter  (fun (p,g) -> g.Length >= 2)
|> List.length
|> printfn "%A"

let hor_vert_diag x1 y1 x2 y2 =
    if x1 = x2 then
        [ for y in [min y1 y2..max y1 y2] do yield x1, y ]
    elif y1 = y2 then
        [ for x in [min x1 x2..max x1 x2] do yield x, y1 ]
    else
        let xb = (x2-x1) / abs (x2-x1)
        let yb = (y2-y1) / abs (y2-y1)
        let xs = [x1..xb..x2]
        let ys = [y1..yb..y2]
        List.zip xs ys

lines
|> List.map     (fun x -> x.Split([|" -> " ; ","|], StringSplitOptions.None) |> Array.map int)
|> List.map     (fun x -> x.[0], x.[1], x.[2], x.[3])
|> List.collect (fun (x1,y1,x2,y2) -> hor_vert_diag x1 y1 x2 y2)
|> List.groupBy id
|> List.map     (fun (p,g) -> p, g.Length)
|> List.filter  (fun (p,g) -> g >= 2)
|> List.length
|> printfn "%A"
