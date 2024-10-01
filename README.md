# FsHttp [![Build & Tests](https://github.com/fsprojects/FsHttp/actions/workflows/build-and-test.yml/badge.svg?branch=master)](https://github.com/fsprojects/FsHttp/actions/workflows/build-and-test.yml) [![NuGet Badge](https://buildstats.info/nuget/FsHttp)](https://www.nuget.org/packages/FsHttp)

<img align="right" width="200" alt='logo' src='https://raw.githubusercontent.com/fsprojects/FsHttp/master/docs/img/logo_big.png' />

FsHttp ("Full Stack HTTP") is a "hackable HTTP client" that offers a legible style for the basics while still affording full access to the underlying HTTP representations for covering unusual cases. It's the best of both worlds: **Convenience and Flexibility**.

* Use it as a replacement for `.http` files, *VSCode's REST client*, *Postman*, and other tools as an **interactive and programmable playground** for HTTP requests.
* Usable as a **production-ready HTTP client** for applications powered by .NET (C#, VB, F#).

👍 Postman? ❤️ FsHttp! https://youtu.be/F508wQu7ET0

## Documentation

* 📖 Please see [FsHttp Documentation](https://fsprojects.github.io/FsHttp) site for detailed documentation.
* 🧪 In addition, have a look at the [Integration Tests](https://github.com/fsprojects/FsHttp/tree/master/src/Tests) that show various library details.

### F# syntax example

```fsharp
#r "nuget: FsHttp"

open FsHttp

http {
    POST "https://reqres.in/api/users"
    CacheControl "no-cache"
    body
    jsonSerialize
        {|
            name = "morpheus"
            job = "leader"
        |}
}
|> Request.send
```

### C# syntax example

```csharp
#r "nuget: FsHttp"

using FsHttp;

await Http
    .Post("https://reqres.in/api/users")
    .CacheControl("no-cache")
    .Body()
    .JsonSerialize(new
        {
            name = "morpheus",
            job = "leader"
        }
    )
    .SendAsync();
```

### Release Notes / Migrating to new versions

* See https://www.nuget.org/packages/FsHttp#release-body-tab
* For different upgrade paths, please read the [Migrations docs section](https://fsprojects.github.io/FsHttp/Release_Notes.html).

## Building

**.Net SDK:**

You need to have a recent .NET SDK installed, which is specified in `./global.json`.

**Build Tasks**

There is a F# build script (`./build.fsx`) that can be used to perform several build tasks from command line.

For common tasks, there are bash scripts located in the repo root:

* `./test.sh`: Runs all tests (sources in `./src/Tests`).
  * You can pass args to this task. E.g. for executing only some tests:
    `./test.sh --filter Name~'Response Decompression'`
* `./docu.sh`: Rebuilds the FsHttp documentation site (sources in `./src/docs`).
* `./docu-watch.sh`: Run it if you are working on the documentation sources, and want to see the result in a browser.
* `./publish.sh`: Publishes all packages (FsHttp and it's integration packages for Newtonsoft and FSharp.Data) to NuGet.
  * Always have a look at `./src/Directory.Build.props` and keep the file up-to-date.

## Credits

* Parts of the code were taken from the [HTTP utilities of FSharp.Data](https://fsprojects.github.io/FSharp.Data/library/Http.html).
* Credits to all critics, supporters, contributors, promoters, users, and friends.
