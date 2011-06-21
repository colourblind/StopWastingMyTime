using System;
using System.Linq;

namespace StopWastingMyTime.Models
{
	public class Job : Base.Job
	{
		#region Constructors
		
		public Job() : base()
		{
			
		}
		
		public Job(string jobId) : base(jobId)
		{
			
		}
		
		internal Job(Data.Job dataObject) : base(dataObject)
		{
			
		}
		
		#endregion
	}
}

