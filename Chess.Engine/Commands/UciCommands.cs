using System;
using System.Reflection;

namespace Chess.Engine.Commands;

public class UciCommands : ICommandParser
{
    Board _board;

    public UciCommands()
    {
        _board = new Board(BoardInitialization.Standard);
        SendId();
        SendSupportedOptions();
        SendReady();
    }

    public void Run()
    {
        while (true)
        {
            string command = Console.ReadLine();
            if (command != null)
            {
                if (ParseCommand(command.Trim().ToLowerInvariant())) ;
            }
        }
    }

    private void SendId()
    {
        Version version = Assembly.GetExecutingAssembly().GetName().Version;
        Console.WriteLine("id name Alteridem {0}.{1}", version.Major, version.Minor);
        Console.WriteLine("id author Rob Prouse");
    }

    private void SendSupportedOptions()
    {
        // TODO: Send any supported options here
    }

    private static void SendReady()
    {
        Console.WriteLine("uciok");
    }

    private bool ParseCommand(string command)
    {
        if (command == null)
        {
            return false; // Invalid
        }
        string[] parts = command.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
        {
            return false; // Invalid
        }
        command = parts[0];

        switch (command)
        {
            case "quit":
                return true;    // Causes the engine to exit
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
                Position(parts);
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
        return false;
    }

    private void Debug(string[] parts)
    {
        // TODO: Set debugging on | off in the engine
    }

    private void IsReady()
    {
        // TODO: Determine if any background actions are in progress
        Console.WriteLine("readyok");
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

    private void Position(string[] parts)
    {
        if (parts.Length < 2)
        {
            return; // Invalid
        }
        if (parts[1] == "fen" && parts.Length >= 3)
        {
            _board = new Board(parts[2]);
            PlaybackMoves(parts, 3);
        }
        else if (parts[1] == "startpos")
        {
            _board = new Board(BoardInitialization.Standard);
            PlaybackMoves(parts, 2);
        }
        else
        {
            return; // Invalid    
        }
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
