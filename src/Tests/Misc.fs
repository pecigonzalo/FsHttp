﻿module FsHttp.Tests.Misc

open System
open FsUnit
open FsHttp
open FsHttp.Tests
open FsHttp.Tests.Server

open NUnit.Framework

open Suave
open Suave.Operators
open Suave.Filters
open Suave.Successful


[<TestCase>]
let ``Custom HTTP method`` () =
    use server =
        ``method`` (HttpMethod.parse "FLY") >=> request (fun r -> OK "flying") |> serve

    http { Method "FLY" (url @"") }
    |> Request.send
    |> Response.toText
    |> should equal "flying"


[<TestCase>]
let ``Custom Header`` () =
    let customHeaderKey = "X-Custom-Value"

    use server =
        GET
        >=> request (fun r ->
            r.header customHeaderKey
            |> function
                | Choice1Of2 v -> v
                | Choice2Of2 e -> raise (assertionExn $"Failed {e}")
            |> OK
        )
        |> serve

    http {
        GET(url @"")
        header customHeaderKey "hello world"
    }
    |> Request.send
    |> Response.toText
    |> should equal "hello world"

[<TestCase>]
let ``Custom Headers`` () =
    let headersToString =
        List.sort
        >> List.map (fun (key, value) -> $"{key}={value}".ToLower())
        >> (fun h -> String.Join("&", h))

    let headerPrefix = "X-Custom-Value"
    let customHeaders = [ for i in 1..10 -> $"{headerPrefix}{i}", $"{i}" ]
    let expected = headersToString customHeaders

    use server =
        GET
        >=> request (fun r ->
            r.headers
            |> List.filter (fun (k, _) -> k.StartsWith(headerPrefix, StringComparison.InvariantCultureIgnoreCase))
            |> headersToString
            |> OK
        )
        |> serve

    http {
        GET(url @"")
        headers customHeaders
    }
    |> Request.send
    |> Response.toText
    |> should equal expected

//let [<TestCase>] ``Auto Redirects``() =
//    http {
//        GET (url @"")
//        config_transformHttpClientHandler (fun handler ->
//            handler.AllowAutoRedirect <- false
//            handler
//        )
//    }
//    |> Response.toText
//    |> should equal "hello world"



// TODO:

// let [<TestCase>] ``Http reauest message can be modified``() =
//     use server = GET >=> request (header "accept-language" >> OK) |> serve

//     let lang = "fr"
//     http {
//         GET (url @"")
//         transformHttpRequestMessage (fun httpRequestMessage ->
//             httpRequestMessage
//         )
//     }
//     |> toText
//     |> should equal lang

// TODO: Timeout
// TODO: ToFormattedText
// TODO: transformHttpRequestMessage
// TODO: transformHttpClient
// TODO: Cookie tests (test the overloads)
