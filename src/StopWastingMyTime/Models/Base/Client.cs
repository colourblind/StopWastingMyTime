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
    [XmlType("Base.Client")]
    public class Client : IComparable<Base.Client>, IComparable
    {
        #region Fields

        private Guid _clientId = Guid.Empty;
        private string _name = String.Empty;
        private decimal _maintenancePerMonth;


        private List<StopWastingMyTime.Models.Job> _jobs = null;

        private bool _isNew = true;

        #endregion

        #region Properties

        public Guid ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
        }
		
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }
		
        public virtual decimal MaintenancePerMonth
        {
            get { return _maintenancePerMonth; }
            set { _maintenancePerMonth = value; }
        }
		

        public virtual IList<StopWastingMyTime.Models.Job> Jobs
        {
            get
            {
                if (_jobs == null)
                    _jobs = Job.SelectByClientId(ClientId);
                return _jobs;
            }
        }
        public bool IsNew
        {
            get { return _isNew; }
        }

        #endregion

        #region Constructors

        public Client()
        {
            _clientId = Guid.Empty;
        }
		
        public Client(Guid clientId)
        {
            Load(clientId);
        }
		
        internal Client(Data.Client dataObject)
        {
            Load(dataObject);
        }

        #endregion

        #region Methods

        internal void Load(Guid clientId)
        {
            Load(Data.Client.SelectById(clientId));
        }

        internal void Load(Data.Client dataObject)
        {
            if (dataObject == null)
            {
                _isNew = true;
            }
            else
            {
                _clientId = dataObject.ClientId;
                _name = dataObject.Name;
                _maintenancePerMonth = dataObject.MaintenancePerMonth;

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
            Data.Client dataObject = new Data.Client();

            dataObject.ClientId = _clientId;
            dataObject.Name = _name;
            dataObject.MaintenancePerMonth = _maintenancePerMonth;

            if (IsNew)
                dataObject.Insert();
            else
                dataObject.Update();
			
            _clientId = dataObject.ClientId;
            if (refresh)
                Refresh();

            _isNew = false;
        }
        
        internal virtual void Restore()
        {
            _isNew = true;
            Save(false);
            
            foreach (Job job in Jobs)
            {
                job.ClientId = ClientId;
                job.Restore();
            }
            
            Refresh();
        }

        public virtual void Delete()
        {
            Data.Client.Delete(ClientId);
            Refresh();
        }
		
        public virtual void Refresh()
        {
            _jobs = null;
        }
        
        public override bool Equals(object obj)
        {
            return this.CompareTo(obj) == 0;
        }
		
        public virtual int CompareTo(Client obj)
        {
            return ClientId.CompareTo(obj.ClientId) * 1;
        }
		
        public virtual int CompareTo(object obj)
        {
            if (obj is Client)
                return CompareTo((Client)obj);
            else
                throw new InvalidCastException("Unable to compare Client and " + obj.GetType().Name);
        }

        #endregion

        #region Statics
        
        protected static List<StopWastingMyTime.Models.Client> ConvertDataItemList(IEnumerable<Data.Client> data)
        {
            List<StopWastingMyTime.Models.Client> result = new List<StopWastingMyTime.Models.Client>();
            foreach (Data.Client dataItem in data)
                result.Add(new StopWastingMyTime.Models.Client(dataItem));
            return result;
        }

        public static List<StopWastingMyTime.Models.Client> SelectAll()
        {
            return ConvertDataItemList(Data.Client.Select());
        }

		#endregion
	}
}

