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

        #region Properties

        private decimal _totalHours = -1;
        public decimal TotalHours
        {
            get
            {
                if (_totalHours < 0)
                    _totalHours = TimeBlocks.Sum(o => o.Time);
                return _totalHours;
            }
        }

        #endregion
    }
}

