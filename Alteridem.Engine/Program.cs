using System;
using Alteridem.Engine.Commands;

namespace Alteridem.Engine
{
   class Program
   {
      static void Main( string[] args )
      {
          ICommandParser parser = CommandFactory.GetParser();
          if ( parser != null )
          {
              parser.Run();
          }
      }
   }
}
