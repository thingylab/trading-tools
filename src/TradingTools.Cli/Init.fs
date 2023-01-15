namespace TradingTools.Cli

open System.IO
open Spectre.Console

module Init =
    let checkDatabase path =
        if not <| File.Exists(path) then
            AnsiConsole.Confirm("Specified database does not exist. Create?")
        else true
