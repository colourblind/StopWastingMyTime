/*
 * Business Object to ADO
 * Base.cst
 * Template version: 0.5.0.4
 */
 
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using System.Web.Script.Serialization;

namespace StopWastingMyTime.Models.Base
{
    [XmlType("Base.TimeBlock")]
    public class TimeBlock : IComparable<Base.TimeBlock>, IComparable
    {
        #region Fields

        private Guid _timeBlockId = Guid.Empty;
        private string _userId = String.Empty;
        private string _jobId = String.Empty;
        private DateTime _date = DateTime.Now;
        private decimal _time;
        private string _comment = String.Empty;

        private StopWastingMyTime.Models.Job _job = null;
        private StopWastingMyTime.Models.User _user = null;


        private bool _isNew = true;

        #endregion

        #region Properties

        public Guid TimeBlockId
        {
            get { return _timeBlockId; }
            set { _timeBlockId = value; }
        }
		
        public virtual string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
		
        public virtual string JobId
        {
            get { return _jobId; }
            set { _jobId = value; }
        }
		
        public virtual DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
		
        public virtual decimal Time
        {
            get { return _time; }
            set { _time = value; }
        }
		
        public virtual string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }
		

		[XmlIgnore]
		[ScriptIgnore]
        public virtual StopWastingMyTime.Models.Job Job
        {
            get
            {
                if (_job == null)
                    _job = new StopWastingMyTime.Models.Job(JobId);
                return _job;
            }
        }
        
		[XmlIgnore]
		[ScriptIgnore]
        public virtual StopWastingMyTime.Models.User User
        {
            get
            {
                if (_user == null)
                    _user = new StopWastingMyTime.Models.User(UserId);
                return _user;
            }
        }
                public bool IsNew
        {
            get { return _isNew; }
        }

        #endregion

        #region Constructors

        public TimeBlock()
        {
            _timeBlockId = Guid.Empty;
        }
		
        public TimeBlock(Guid timeBlockId)
        {
            Load(timeBlockId);
        }
		
        internal TimeBlock(Data.TimeBlock dataObject)
        {
            Load(dataObject);
        }

        #endregion

        #region Methods

        internal void Load(Guid timeBlockId)
        {
            Load(Data.TimeBlock.SelectById(timeBlockId));
        }

        internal void Load(Data.TimeBlock dataObject)
        {
            if (dataObject == null)
            {
                _isNew = true;
            }
            else
            {
                _timeBlockId = dataObject.TimeBlockId;
                _userId = dataObject.UserId;
                _jobId = dataObject.JobId;
                _date = dataObject.Date;
                _time = dataObject.Time;
                _comment = dataObject.Comment;

                _isNew = false;
            }

            Refresh();
        }

        public virtual void Save()
        {
            Save(true);
        }
        
        protected virtual void Save(bool refresh)
        {
            Data.TimeBlock dataObject = new Data.TimeBlock();

            dataObject.TimeBlockId = _timeBlockId;
            dataObject.UserId = _userId;
            dataObject.JobId = _jobId;
            dataObject.Date = _date;
            dataObject.Time = _time;
            dataObject.Comment = _comment;

            if (IsNew)
                dataObject.Insert();
            else
                dataObject.Update();
			
            _timeBlockId = dataObject.TimeBlockId;
            if (refresh)
                Refresh();

            _isNew = false;
        }
        
        internal virtual void Restore()
        {
            _isNew = true;
            Save(false);
            
            
            Refresh();
        }

        public virtual void Delete()
        {
            Data.TimeBlock.Delete(TimeBlockId);
            Refresh();
        }
		
        public virtual void Refresh()
        {
            _job = null;
            _user = null;
        }
        
        public override bool Equals(object obj)
        {
            return this.CompareTo(obj) == 0;
        }
		
        public virtual int CompareTo(TimeBlock obj)
        {
            return TimeBlockId.CompareTo(obj.TimeBlockId) * 1;
        }
		
        public virtual int CompareTo(object obj)
        {
            if (obj is TimeBlock)
                return CompareTo((TimeBlock)obj);
            else
                throw new InvalidCastException("Unable to compare TimeBlock and " + obj.GetType().Name);
        }

        #endregion

        #region Statics
        
        protected static List<StopWastingMyTime.Models.TimeBlock> ConvertDataItemList(IEnumerable<Data.TimeBlock> data)
        {
            List<StopWastingMyTime.Models.TimeBlock> result = new List<StopWastingMyTime.Models.TimeBlock>();
            foreach (Data.TimeBlock dataItem in data)
                result.Add(new StopWastingMyTime.Models.TimeBlock(dataItem));
            return result;
        }

        public static List<StopWastingMyTime.Models.TimeBlock> SelectAll()
        {
            return ConvertDataItemList(Data.TimeBlock.Select());
        }

        public static List<StopWastingMyTime.Models.TimeBlock> SelectByJobId(string jobId)
        {
			return ConvertDataItemList(Data.TimeBlock.SelectByJobId(jobId));
		}
		
        public static List<StopWastingMyTime.Models.TimeBlock> SelectByUserId(string userId)
        {
			return ConvertDataItemList(Data.TimeBlock.SelectByUserId(userId));
		}
		
		#endregion
	}
}

