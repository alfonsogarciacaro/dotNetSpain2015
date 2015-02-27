namespace Functional

open System.IO
open System.Net
open System.Text

// In F#, thanks to type inference and other features the syntax is much terser
// Also, many of the functions we need are already implemented
// We can define our custom operators for an even lighter syntax too
module Seq =
    let ``pure`` f = seq { yield f }    // pure is a keyword in F#

    let (<*>) f a =                     // <*> is often used for applying
        seq { for f in f do
                for a in a do
                    yield f a }
        
    let (<!>) f a = ``pure`` f <*> a    // Convenience operator

//    let f x y = x + y                   // Quick tests
//    printfn "%A" (f <!> [3;4] <*> [2;1])
//    printfn "%A" (f <!> [3;4] <*> [])


module Option =
    let (<*>) f a =
        match f with
        | Some f -> Option.map f a
        | None -> None

    let ``pure`` f = Some f
    let (<!>) f a = ``pure`` f <*> a
    
//    printfn "%A" ((+) <!> Some 5 <*> Some 4)    // Quick tests
//    printfn "%A" ((+) <!> Some 5 <*> None  )

module Async =
    let (<*>) f a =
      async { let! f = f
              let! a = a
              return f a }

    let ``pure`` f = async { return f }
    let (<!>) f a = ``pure`` f <*> a

    // Quick test
//    let delay x = async { do! Async.Sleep 1000
//                          return x }
//    ((+) <!> delay 5 <*> delay 4)
//    |> Async.RunSynchronously
//    |> printfn "%A"

    // Quick test 2
    let fetchLines (url: string) i = async {
        let rec append i (s: StreamReader) (b: StringBuilder) =
            match i with
            | 0 -> b.ToString()
            | _ -> s.ReadLine() |> b.AppendLine |> append (i-1) s
         
        let req = WebRequest.Create(url) 
        use! resp = req.AsyncGetResponse()
        use stream = resp.GetResponseStream() 
        use reader = new StreamReader(stream) 
        return append i reader (StringBuilder())
    }

    // Downloading content from the internet
    (fun x y z -> String.length x + String.length y + String.length z)
        <!> fetchLines "http://microsoft.github.io" 10
        <*> fetchLines "http://fsharp.org" 10
        <*> fetchLines "http://funscript.info" 10
    |> Async.RunSynchronously
    |> printfn "Chars fetched: %i" 




