using System;
using System.Linq;

namespace StopWastingMyTime.Models
{
	public class Client : Base.Client
	{
		#region Constructors
		
		public Client() : base()
		{
			
		}
		
		public Client(Guid clientId) : base(clientId)
		{
			
		}
		
		internal Client(Data.Client dataObject) : base(dataObject)
		{
			
		}
		
		#endregion
	}
}

