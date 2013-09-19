using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Delegates
{
	public delegate void SyslogDelegate( string mesg );

	public class ConsoleClass
	{
		public void LogToConsole( string mesg )
		{
			Console.WriteLine( "Writing to console" );
			Console.WriteLine( mesg );
			Console.WriteLine( "Written to console" );
			Console.WriteLine();
		}
	}

	public class FileClass
	{
		public void LogToFile( string mesg )
		{
			string file = "syslog.txt";
			Console.WriteLine( "Writing to file " + file );
			using ( TextWriter f = File.CreateText( file ) ) {
				Console.WriteLine( "\t..." );
				f.WriteLine( mesg );
			}
			Console.WriteLine( "Written to file " + file );
			Console.WriteLine();
		}
	}

	public class AnonymousDelegateClass
	{
		public SyslogDelegate ReturnAnonymousDelegate()
		{
			int outerCount = 0;
			Console.WriteLine( "Writing to console" );
			return delegate( string s ) {
				int innerCount = 0;
				outerCount++;
				innerCount++;
				Console.WriteLine( s );
				Console.Write( "Written to console. " + "Local scope counter: " + innerCount.ToString() + ". Outer scope counter: " + outerCount.ToString() );
				Console.WriteLine();
			};
		}
	}
}
