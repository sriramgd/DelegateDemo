using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Delegates
{
	public class MyDelegate
	{
		public MyDelegate(object target, string method) {
			this.Target = target;
			this.Method = method;
		}
		public object Target { get; set; }
		public string Method { get; set; }
		public void Invoke(Object[] paramArray) {
			MethodInfo m = Target.GetType().GetMethod( Method );
			m.Invoke( Target, paramArray );
		}
	}

	public class MyMulticastDelegate
	{
		public List<MyDelegate> InvocationList { get; set; }
		public MyMulticastDelegate()
		{
			InvocationList = new List<MyDelegate>();
		}
		public void Add( object target, string method )
		{
			InvocationList.Add( new MyDelegate( target, method ) );
		}
		public void Remove()
		{
			if ( InvocationList.Count > 0 ) {
				InvocationList.RemoveAt( InvocationList.Count - 1 );
			}
		}
		public void Invoke( object[] paramArray )
		{
			for ( int i = 0; i < InvocationList.Count(); i++ ) {
				InvocationList[i].Invoke( paramArray );
			}
		}
	}
}
