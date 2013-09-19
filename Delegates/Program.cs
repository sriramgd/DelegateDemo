using System;
using System.Collections.Generic;
using System.Linq;

namespace Delegates
{
	class Program
	{
		//Simulate some log message
		static string LogMesg
		{
			get { return "\t" + DateTime.Now.ToString(); } 
		}

		static void Main( string[] args )
		{
			TestDelegates();
			TestMyDelegates();
			TestClosureWithAnonymousDelegate();

			Console.ReadLine();
		}

		private static void TestDelegates()
		{
			ConsoleClass console = new ConsoleClass();
			//Add a method pointer to the delegate
			SyslogDelegate d = console.LogToConsole;

			FileClass file = new FileClass();
			//Add second pointer to the delegate (multicast).
			d += file.LogToFile;

			//Call delegate. This invokes all the method pointers the delegate refers to.
			d.Invoke( LogMesg ); //Shorthand: d( mesg );

			int i = 1;
			Console.WriteLine( "Methods which were referred to by " + d.GetType().Name + ": " );
			foreach ( var delegated in d.GetInvocationList() ) {
				Console.Write( i.ToString() + ". " );
				Console.WriteLine( delegated.Target.GetType().Name + "->" + delegated.Method.Name );
				i++;
			}

			Console.WriteLine( "---" );
		}

		private static void TestMyDelegates()
		{
			MyMulticastDelegate m = new MyMulticastDelegate();
			//Add method 'references' to our delegate
			m.Add( new ConsoleClass(), "LogToConsole" );
			m.Add( new FileClass(), "LogToFile" );			

			//Call delegate
			m.Invoke( new object[] { LogMesg } );

			Console.WriteLine("---");
		}

		private static void TestClosureWithAnonymousDelegate()
		{
			AnonymousDelegateClass anonDelegateClass = new AnonymousDelegateClass();
			SyslogDelegate a = anonDelegateClass.ReturnAnonymousDelegate();

			//Repeatedly invoke anonymous method. Let us see what happen to the scoped variables inside the method.
			a.Invoke( LogMesg );
			a.Invoke( LogMesg );
			a.Invoke( LogMesg );
			a.Invoke( LogMesg );

			Console.WriteLine( "---" );
		}

	}
}
