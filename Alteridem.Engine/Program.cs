using System;

namespace Alteridem.Engine
{
   class Program
   {
      static void Main( string[] args )
      {
         Board board = new Board( BoardInitialization.Standard );
         Console.WriteLine( board.ToString() );

         Console.ReadLine();
      }
   }
}
