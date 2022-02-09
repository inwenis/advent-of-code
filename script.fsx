open System
open System.IO

let lines = File.ReadAllLines("input31.txt") |> List.ofArray

let gamma_b, epsilon_b =
    lines
    |> List.map (fun x -> x |> List.ofSeq)
    |> List.transpose
    |> List.map (fun col -> col |> List.countBy id)
    |> List.map (fun counts -> counts |> List.sortByDescending snd )
    |> List.map (fun bits -> fst bits.[0], fst bits.[1])
    |> List.toArray
    |> Array.unzip
let gamma_b2   = new String(gamma_b)   |> fun x -> Convert.ToInt32(x, 2)
let epsilon_b2 = new String(epsilon_b) |> fun x -> Convert.ToInt32(x, 2)
gamma_b2 * epsilon_b2 |> printfn "%A"

let oxy_gen_rat =
    let mutable lines2 = File.ReadAllLines("input32.txt") |> List.ofArray
    let mutable pos = 0
    while lines2.Length > 1 do
        let counts =
            lines2
            |> List.map (fun x -> x.[pos])
            |> List.countBy id
            |> Map.ofSeq
        let to_keep =
            match counts.TryFind '1', counts.TryFind '0' with
            | Some o, Some z when o > z -> '1'
            | Some o, Some z when o < z -> '0'
            | Some o, Some z when o = z -> '1'
            | Some _, None              -> '1'
            | None  , Some _            -> '0'
        lines2 <- lines2 |> List.filter (fun x -> x.[pos] = to_keep)
        pos <- pos + 1
    lines2 |> List.exactlyOne |> fun x -> Convert.ToInt32(x, 2)
let co2_scu_rat =
    let mutable lines2 = File.ReadAllLines("input32.txt") |> List.ofArray
    let mutable pos = 0
    while lines2.Length > 1 do
        let counts =
            lines2
            |> List.map (fun x -> x.[pos])
            |> List.countBy id
            |> Map.ofSeq
        let to_keep =
            match counts.TryFind '1', counts.TryFind '0' with
            | Some o, Some z when o > z -> '0'
            | Some o, Some z when o < z -> '1'
            | Some o, Some z when o = z -> '0'
            | Some _, None              -> '0'
            | None  , Some _            -> '1'
        lines2 <- lines2 |> List.filter (fun x -> x.[pos] = to_keep)
        pos <- pos + 1
    lines2 |> List.exactlyOne |> fun x -> Convert.ToInt32(x, 2)
oxy_gen_rat * co2_scu_rat |> printfn "%A"