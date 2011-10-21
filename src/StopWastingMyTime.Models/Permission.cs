using System;
using System.Linq;

namespace StopWastingMyTime.Models
{
	public class Permission : Base.Permission
	{
		#region Constructors
		
		public Permission() : base()
		{
			
		}
		
		public Permission(string permissionId) : base(permissionId)
		{
			
		}
		
		internal Permission(Data.Permission dataObject) : base(dataObject)
		{
			
		}
		
		#endregion
	}
}

