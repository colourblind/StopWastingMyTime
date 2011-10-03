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
    [XmlType("Base.Job")]
    public class Job : IComparable<Base.Job>, IComparable
    {
        #region Fields

        private string _jobId = String.Empty;
        private Guid _clientId = Guid.Empty;
        private bool _billable;
        private decimal? _quotedHours = null;
        private string _description = String.Empty;
        private bool _isActive;

        private StopWastingMyTime.Models.Client _client = null;

        private List<StopWastingMyTime.Models.TimeBlock> _timeBlocks = null;

        private bool _isNew = true;

        #endregion

        #region Properties

        public string JobId
        {
            get { return _jobId; }
            set { _jobId = value; }
        }
		
        public virtual Guid ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
        }
		
        public virtual bool Billable
        {
            get { return _billable; }
            set { _billable = value; }
        }
		
        public virtual decimal? QuotedHours
        {
            get { return _quotedHours; }
            set { _quotedHours = value; }
        }
		
        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }
		
        public virtual bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
		

		[XmlIgnore]
		[ScriptIgnore]
        public virtual StopWastingMyTime.Models.Client Client
        {
            get
            {
                if (_client == null)
                    _client = new StopWastingMyTime.Models.Client(ClientId);
                return _client;
            }
        }
        
        public virtual IList<StopWastingMyTime.Models.TimeBlock> TimeBlocks
        {
            get
            {
                if (_timeBlocks == null)
                    _timeBlocks = TimeBlock.SelectByJobId(JobId);
                return _timeBlocks;
            }
        }
        public bool IsNew
        {
            get { return _isNew; }
        }

        #endregion

        #region Constructors

        public Job()
        {
            _jobId = String.Empty;
        }
		
        public Job(string jobId)
        {
            Load(jobId);
        }
		
        internal Job(Data.Job dataObject)
        {
            Load(dataObject);
        }

        #endregion

        #region Methods

        internal void Load(string jobId)
        {
            Load(Data.Job.SelectById(jobId));
        }

        internal void Load(Data.Job dataObject)
        {
            if (dataObject == null)
            {
                _isNew = true;
            }
            else
            {
                _jobId = dataObject.JobId;
                _clientId = dataObject.ClientId;
                _billable = dataObject.Billable;
                _quotedHours = dataObject.QuotedHours;
                _description = dataObject.Description;
                _isActive = dataObject.IsActive;

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
            Data.Job dataObject = new Data.Job();

            dataObject.JobId = _jobId;
            dataObject.ClientId = _clientId;
            dataObject.Billable = _billable;
            dataObject.QuotedHours = _quotedHours;
            dataObject.Description = _description;
            dataObject.IsActive = _isActive;

            if (IsNew)
                dataObject.Insert();
            else
                dataObject.Update();
			
            _jobId = dataObject.JobId;
            if (refresh)
                Refresh();

            _isNew = false;
        }
        
        internal virtual void Restore()
        {
            _isNew = true;
            Save(false);
            
            foreach (TimeBlock timeBlock in TimeBlocks)
            {
                timeBlock.JobId = JobId;
                timeBlock.Restore();
            }
            
            Refresh();
        }

        public virtual void Delete()
        {
            Data.Job.Delete(JobId);
            Refresh();
        }
		
        public virtual void Refresh()
        {
            _client = null;
            _timeBlocks = null;
        }
        
        public override bool Equals(object obj)
        {
            return this.CompareTo(obj) == 0;
        }
		
        public virtual int CompareTo(Job obj)
        {
            return JobId.CompareTo(obj.JobId) * 1;
        }
		
        public virtual int CompareTo(object obj)
        {
            if (obj is Job)
                return CompareTo((Job)obj);
            else
                throw new InvalidCastException("Unable to compare Job and " + obj.GetType().Name);
        }

        #endregion

        #region Statics
        
        protected static List<StopWastingMyTime.Models.Job> ConvertDataItemList(IEnumerable<Data.Job> data)
        {
            List<StopWastingMyTime.Models.Job> result = new List<StopWastingMyTime.Models.Job>();
            foreach (Data.Job dataItem in data)
                result.Add(new StopWastingMyTime.Models.Job(dataItem));
            return result;
        }

        public static List<StopWastingMyTime.Models.Job> SelectAll()
        {
            return ConvertDataItemList(Data.Job.Select());
        }

        public static List<StopWastingMyTime.Models.Job> SelectByClientId(Guid clientId)
        {
			return ConvertDataItemList(Data.Job.SelectByClientId(clientId));
		}
		
		#endregion
	}
}

