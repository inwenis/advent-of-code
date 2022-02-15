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

let parseBoard (lstlst:string list) =
    lstlst
    |> List.map (fun x -> x.Split(" ", StringSplitOptions.RemoveEmptyEntries) |> Seq.map int |> Seq.toList)

open System.Collections.Generic

let lines4 = File.ReadAllLines("input41.txt") |> Seq.toList
let numbers = lines4.[0].Split(",") |> Array.map int
let boards =
    lines4
    |> List.skip 2 // skip numbers and empty line
    |> List.chunkBySize 6
    |> List.map (fun x -> x[0..4]) // remove empty line
    |> List.map parseBoard

let test (h:HashSet<int*int>) =
    let win1 = h |> List.ofSeq |> List.groupBy fst |> List.filter (fun (k,g) -> g.Length = 5) |> List.length > 0
    let win2 = h |> List.ofSeq |> List.groupBy snd |> List.filter (fun (k,g) -> g.Length = 5) |> List.length > 0
    win1 || win2


let play_till_first_win (boards:int list list list) =
    let boards = boards |> List.map (fun x -> x, new HashSet<int*int>())
    let mutable c = true
    let mutable index = 0
    let mutable winner = [],null,0
    while c do
        let n = numbers.[index]
        index <- index + 1
        for b,h in boards do
            for i in [0..4] do
                for j in [0..4] do
                    if b.[i].[j] = n then h.Add((i,j)) |> ignore
        // check if any winning board
        let winners =
            boards
            |> List.filter (fun (b,h) -> test h)
        if winners.Length > 0 then
            c <- false
            let b,h = winners |> List.exactlyOne
            winner <- b,h,n
    winner

let play_till_last_win (boards:int list list list) =
    let mutable boards = boards |> List.map (fun x -> x, new HashSet<int*int>())
    let mutable winners = []
    let mutable c = true
    let mutable index = 0
    let mutable last_winner = [],null,0
    while c do
        let n = numbers.[index]
        index <- index + 1
        for b,h in boards do
            for i in [0..4] do
                for j in [0..4] do
                    if b.[i].[j] = n then h.Add((i,j)) |> ignore
        // check if any winning board
        let winners',remaining = boards |> List.partition (fun (b,h) -> test h)
        winners <- winners @ winners'
        boards <- remaining
        if remaining.Length = 0 then
            c <- false
            let b,h = winners' |> List.exactlyOne
            last_winner <- b,h,n
    last_winner


let w,h,n = play_till_first_win boards
let sum =
    w
    |> List.mapi (fun i x -> x |> List.mapi (fun j y -> (i,j),y))
    |> List.concat
    |> List.filter (fun ((i,j),v) -> h.Contains(i,j) |> not)
    |> List.sumBy snd
sum * n


let wl,hl,nl = play_till_last_win boards
let suml =
    wl
    |> List.mapi (fun i x -> x |> List.mapi (fun j y -> (i,j),y))
    |> List.concat
    |> List.filter (fun ((i,j),v) -> hl.Contains(i,j) |> not)
    |> List.sumBy snd
suml * nl

