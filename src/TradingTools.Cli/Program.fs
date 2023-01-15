open System
open Argu
open TradingTools.Cli

type InitPath =
    | [<AltCommandLine("-p")>] Path of string

    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Path _ -> "location of the environment"
and TopLevelCommand =
    | [<CliPrefix(CliPrefix.None)>] Init of ParseResults<InitPath>
    | [<Inherit; AltCommandLine("--db")>] Database of string
    | Version
    | [<AltCommandLine("-v")>] Verbose

    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Init _ -> "create a new data repository"
            | Database _ -> "location of the environment folder"
            | Version _ -> "print the program version"
            | Verbose -> "print additional information"

let args = Environment.GetCommandLineArgs() |> Array.skip 1
let parser = ArgumentParser.Create<TopLevelCommand>(programName = "trading-tools")

try
    let results = parser.ParseCommandLine(inputs = args, raiseOnUsage = true)

    printfn $"%O{results.GetAllResults()}"

    results.TryGetResult(Database)
    |> Option.defaultValue "trading-tools.db"
    |> Init.checkDatabase
    |> ignore
with e ->
    printfn $"%s{e.Message}"
