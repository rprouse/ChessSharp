ChessSharp
==========

A UCI chess engine in C#.

[![Build and Test](https://github.com/rprouse/ChessSharp/actions/workflows/build.yml/badge.svg)](https://github.com/rprouse/ChessSharp/actions/workflows/build.yml) [![codecov](https://codecov.io/gh/rprouse/ChessSharp/graph/badge.svg?token=QQ3I7HIOCB)](https://codecov.io/gh/rprouse/ChessSharp)

This project is a work in progress and is not yet complete. It is
intended to be a chess engine that can be used with
[UCI-compatible](https://en.wikipedia.org/wiki/Universal_Chess_Interface)
chess interfaces.

- [Description of the universal chess interface (UCI)](./Documents/uci-engine-interface.txt) April 2006
- [Chess Programming Wiki](https://www.chessprogramming.org/Main_Page)
- [Chess Programming News](https://www.chessprogramming.net/)
- [Athena Chess Engine](https://github.com/NicolasSegl/Athena) includes opening book.
- [15 Million Games Chess Database](https://sourceforge.net/projects/codekiddy-chess/)
- [Arena Chess GUI](http://www.playwitharena.com/)
- [Stockfish](https://stockfishchess.org/) - The strongest open-source chess engine.
- [Winboard](https://www.winboard.org/) - A graphical chess interface that supports UCI engines.]

## Board Representation

The board is represented as an array of 64 `Piece` structs which are effectively bytes.

```txt
     a  b  c  d  e  f  g  h
   -------------------------
8 | 56 57 58 59 60 61 62 63 | 8
7 | 48 49 50 51 52 53 54 55 | 7
6 | 40 41 42 43 44 45 46 47 | 6
5 | 32 33 34 35 36 37 38 39 | 5
4 | 24 25 26 27 28 29 30 31 | 4
3 | 16 17 18 19 20 21 22 23 | 3
2 | 08 09 10 11 12 13 14 15 | 2
1 | 00 01 02 03 04 05 06 07 | 1
   -------------------------
     a  b  c  d  e  f  g  h
```
