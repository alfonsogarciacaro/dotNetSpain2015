namespace Functional

open System.IO
open System.Text
open System.Net

module Seq =
    let ``return`` f =
        seq { yield f }

    let (>>=) xs f =            // This is the usual operator for bind
        seq { for x in xs do
                yield! f x }

    // Quick test
//    let dbl2Times x = [for _ in 1..2 do yield x + x]
//    let sqr3Times x = [for _ in 1..3 do yield x * x]
//   
//    [1;2] >>= dbl2Times >>= sqr3Times
//    |> Seq.iter (fun x -> printf "%i " x)


module Option =
    let ``return`` f = Some f

    // Already implemented in FShap.Core
    let (>>=) a f =
        match a with
        | Some a -> f a
        | None -> None

// From http://learnyouahaskell.com/a-fistful-of-monads#walk-the-line
//    type Birds = int
//    type Pole = Birds*Birds
//
//    let landLeft (n: Birds) ((left,right): Pole) =
//        if abs ((left + n) - right) < 4
//        then Some(left + n, right)
//        else None
//
//    let landRight(n: Birds) ((left,right): Pole) =
//        if abs (left - (right + n)) < 4
//        then Some(left, right + n)
//        else None
//
//    Some(0,0) >>= landRight 2 >>= landLeft 2 >>= landRight 2
//    |> printfn "%A"
//
//    Some(0,0) >>= landLeft 1 >>= landRight 4 >>= landLeft (-1) >>= landRight (-2)
//    |> printfn "%A"


module Async =
    // Already implemented in FShap.Core
    let ``return`` = async.Return
    let (>>=) a f = async.Bind(a, f)

    // Quick test 1
//    let addWithEase x y = async {
//        printfn "Will add %i after the break..." x
//        do! Async.Sleep 1000
//        return x + y }
//    (async.Return 0) >>= addWithEase 2 >>= addWithEase (-1)
//    |> Async.RunSynchronously
//    |> printfn "%A"

    // Quick test 2
//    let fetchLines (url: string) i = async {
//        let rec append i (s: StreamReader) (b: StringBuilder) =
//            match i with
//            | 0 -> b.ToString()
//            | _ -> s.ReadLine()
//                   |> b.AppendLine
//                   |> append (i-1) s
//         
//        let req = WebRequest.Create(url) 
//        use! resp = req.AsyncGetResponse()
//        use stream = resp.GetResponseStream() 
//        use reader = new StreamReader(stream) 
//        return append i reader (StringBuilder())
//    }
//
//    let urls = [ "http://microsoft.github.io"
//                 "http://fsharp.org"
//                 "http://funscript.info" ]
//    let res1 =
//        async.Bind(fetchLines urls.[0] 10, fun x ->
//            async.Bind(fetchLines urls.[1] 10, fun y ->
//                async.Bind(fetchLines urls.[2] 10, fun z ->
//                    async.Return(x + y + z))))
//        |> Async.RunSynchronously
//
//    let res2 =
//      async {
//        let! x = fetchLines urls.[0] 10
//        let! y = fetchLines urls.[1] 10
//        let! z = fetchLines urls.[2] 10
//        return x + y + z }
//      |> Async.RunSynchronously
//        
//    printfn "Chars fetched with verbose syntax: %i" res1.Length
//    printfn "Chars fetched with computation expression: %i" res2.Length








