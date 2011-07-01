using System;
using System.Collections.Generic;
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

        #region Methods

        public static IEnumerable<TimeBlock> SelectByUserAndDateRange(string userId, DateTime? from, DateTime? to)
        {
            from = from == null ? DateTime.MinValue : from;
            to = to == null ? DateTime.MaxValue : to;
            IEnumerable<Models.TimeBlock> data = Models.TimeBlock.SelectByUserId(userId);
            data = data.Where(x => x.Date >= from && x.Date <= to);
            return data.OrderBy(x => x.Date);
        }

        #endregion
    }
}

