namespace Functional

module Seq =
    // Already implemented in FSharp.Core
    let map f xs = seq {
        for x in xs do yield f x
    }

module Option =
    // Already implemented in FSharp.Core
    let map f a =
        match a with
        | Some a -> f a     // Var a in higher scope is being shadowed
        | None -> None
    
    // Alternative syntax
//    let map f = function
//        | Some x -> f x
//        | None -> None

module Async =
    let map f a = async {
        let! a = a
        return f a
    }


