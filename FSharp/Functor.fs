namespace Functional

module Seq =
    // Already implemented in FSharp.Core
    let map f xs = seq {
        for x in xs do yield f x
    }

    // Quick test
//    map (fun x -> x + x) [1;2]
//    |> printfn "%A"


module Option =
    // Already implemented in FSharp.Core
    let map f a =
        match a with
        | Some a -> Some(f a)     // Var a in higher scope is being shadowed
        | None -> None
    
    // Alternative syntax
//    let map f = function
//        | Some x -> f x
//        | None -> None

    // Quick test
//    map (fun x -> x + x) (Some 1)
//    |> printfn "%A"

module Async =
    let map f a = async {
        let! a = a
        return f a
    }

    // Quick test
//    async { return 1 }
//    |> map (fun x -> x + x) 
//    |> Async.RunSynchronously
//    |> printfn "%A"

