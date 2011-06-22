using System;
using System.Linq;

namespace StopWastingMyTime.Models
{
	public class TimeBlock : Base.TimeBlock
	{
		#region Constructors
		
		public TimeBlock() : base()
		{
			
		}

        public TimeBlock(Guid timeBlockId) : base(timeBlockId)
		{
			
		}
		
		internal TimeBlock(Data.TimeBlock dataObject) : base(dataObject)
		{
			
		}
		
		#endregion
	}
}

