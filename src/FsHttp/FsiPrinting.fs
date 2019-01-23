
namespace FsHttp

module FsiPrinting =

    open System
    open System.Text
    open System.Net.Http

    open FsHttp

    let print (r: Response) =

        let headerToString (r: Response) =
            let sb = StringBuilder()
            let printHeader (headers: Headers.HttpHeaders) =
                let maxHeaderKeyLength = headers |> Seq.map (fun h -> h.Key.Length) |> Seq.max
                // TODO: Table formatting
                for h in headers do
                    let values = String.Join(", ", h.Value)
                    sb.AppendLine (sprintf "%-*s: %s" (maxHeaderKeyLength + 3) h.Key values) |> ignore
            sb.AppendLine() |> ignore
            sb.AppendLine (sprintf "HTTP/%s %d %s" (r.version.ToString()) (int r.statusCode) (string r.statusCode)) |> ignore
            printHeader r.headers
            sb.AppendLine("// CONTENT") |> ignore
            printHeader r.content.Headers
            sb.AppendLine("// CONTENT") |> ignore
            sb.ToString()

        let content =
            // TODO: When Json or XML, pretty print output
            match r.printHint with
            | Show maxLength -> (toFormattedText r).ToCharArray() |> Array.take maxLength |> string
            | Expand -> toFormattedText r
        sprintf "%s\n%s" (headerToString r) content