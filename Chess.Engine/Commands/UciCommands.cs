using System;
using System.Reflection;
using Chess.Engine.IO;

namespace Chess.Engine.Commands;

public class UciCommands : ICommandParser
{
    private readonly IConsole _console;
    internal Board _board;

    public UciCommands(IConsole console)
    {
        _console = console;
        _board = BoardFactory.Create(BoardInitialization.Standard);
        SendId();
        SendSupportedOptions();
        SendReady();
    }

    public void Run()
    {
        while (true)
        {
            string command = _console.ReadLine();
            if (!ParseCommand(command?.Trim()))
            {
                return;
            }
        }
    }

    private void SendId()
    {
        Version version = Assembly.GetExecutingAssembly().GetName().Version;
        _console.WriteLine($"id name Alteridem {version.Major}.{version.Minor}");
        _console.WriteLine("id author Rob Prouse");
    }

    private void SendSupportedOptions()
    {
        // TODO: Send any supported options here
    }

    private void SendReady()
    {
        _console.WriteLine("uciok");
    }

    /// <summary>
    /// Parses the command and executes the appropriate action.
    /// </summary>
    /// <param name="command"></param>
    /// <returns>False will exit the engine.</returns>
    private bool ParseCommand(string command)
    {
        if (command == null)
        {
            _console.WriteLine("Null command");
            return false; // Invalid
        }
        string[] parts = command.Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
        {
            _console.WriteLine($"Empty command");
            return false; // Invalid
        }

        switch (parts[0])
        {
            case "quit":
                return false;    // Causes the engine to exit
            case "debug":
                Debug(parts);
                break;
            case "isready":
                IsReady();
                break;
            case "setoption":
                SetOption(parts);
                break;
            case "register":
                Register(parts);
                break;
            case "ucinewgame":
                UciNewGame();
                break;
            case "position":
                if (!Position(parts))
                    _console.WriteLine("Invalid position command. Expected 'position fen <fen>' or 'position startpos [moves...]'");
                break;
            case "go":
                Go(parts);
                break;
            case "stop":
                Stop();
                break;
            case "ponderhit":
                PonderHit();
                break;
        }
        _console.WriteLine($"Unknown command: {command}");
        return true; // Ignore unknown commands, but keep running
    }

    private void Debug(string[] parts)
    {
        // TODO: Set debugging on | off in the engine
    }

    private void IsReady()
    {
        // TODO: Determine if any background actions are in progress
        _console.WriteLine("readyok");
    }

    private void SetOption(string[] parts)
    {
        // We don't support options yet
    }

    private void Register(string[] parts)
    {
        // We don't support registration
    }

    private void UciNewGame()
    {
        // TODO: Anything we need to do here?
    }

    private bool Position(string[] parts)
    {
        if (parts.Length < 4)
        {
            return false;
        }
        if (parts[1] == "fen" && parts.Length >= 9)
        {
            string fen = string.Join(" ", parts, 2, 6);
            _board = BoardFactory.Create(fen);
            PlaybackMoves(parts, 8);
            return true;
        }
        else if (parts[1] == "startpos" && parts[2] == "moves")
        {
            _board = BoardFactory.Create(BoardInitialization.Standard);
            PlaybackMoves(parts, 2);
            return true;
        }
        return false;
    }

    private void PlaybackMoves(string[] parts, int start)
    {
        for (int i = start; i < parts.Length; i++)
        {
            PlaybackMove(parts[i]);
        }
    }

    private void PlaybackMove(string move)
    {
        if (move.Length == 4)
        {
            string from = move.Substring(0, 2);
            string to = move.Substring(2, 2);
            _board.MakeMove(from, to);
        }
    }

    private void Go(string[] parts)
    {

    }

    private void Stop()
    {

    }

    private void PonderHit()
    {

    }
}
