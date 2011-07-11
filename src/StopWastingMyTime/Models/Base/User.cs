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
    [XmlType("Base.User")]
    public class User : IComparable<Base.User>, IComparable
    {
        #region Fields

        private string _userId = String.Empty;
        private string _password = String.Empty;
        private string _name = String.Empty;
        private bool _active;


        private List<StopWastingMyTime.Models.TimeBlock> _timeBlocks = null;

        private bool _isNew = true;

        #endregion

        #region Properties

        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
		
        public virtual string Password
        {
            get { return _password; }
            set { _password = value; }
        }
		
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }
		
        public virtual bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
		

        public virtual IList<StopWastingMyTime.Models.TimeBlock> TimeBlocks
        {
            get
            {
                if (_timeBlocks == null)
                    _timeBlocks = TimeBlock.SelectByUserId(UserId);
                return _timeBlocks;
            }
        }
        public bool IsNew
        {
            get { return _isNew; }
        }

        #endregion

        #region Constructors

        public User()
        {
            _userId = String.Empty;
        }
		
        public User(string userId)
        {
            Load(userId);
        }
		
        internal User(Data.User dataObject)
        {
            Load(dataObject);
        }

        #endregion

        #region Methods

        internal void Load(string userId)
        {
            Load(Data.User.SelectById(userId));
        }

        internal void Load(Data.User dataObject)
        {
            if (dataObject == null)
            {
                _isNew = true;
            }
            else
            {
                _userId = dataObject.UserId;
                _password = dataObject.Password;
                _name = dataObject.Name;
                _active = dataObject.Active;

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
            Data.User dataObject = new Data.User();

            dataObject.UserId = _userId;
            dataObject.Password = _password;
            dataObject.Name = _name;
            dataObject.Active = _active;

            if (IsNew)
                dataObject.Insert();
            else
                dataObject.Update();
			
            _userId = dataObject.UserId;
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
                timeBlock.UserId = UserId;
                timeBlock.Restore();
            }
            
            Refresh();
        }

        public virtual void Delete()
        {
            Data.User.Delete(UserId);
            Refresh();
        }
		
        public virtual void Refresh()
        {
            _timeBlocks = null;
        }
        
        public override bool Equals(object obj)
        {
            return this.CompareTo(obj) == 0;
        }
		
        public virtual int CompareTo(User obj)
        {
            return UserId.CompareTo(obj.UserId) * 1;
        }
		
        public virtual int CompareTo(object obj)
        {
            if (obj is User)
                return CompareTo((User)obj);
            else
                throw new InvalidCastException("Unable to compare User and " + obj.GetType().Name);
        }

        #endregion

        #region Statics
        
        protected static List<StopWastingMyTime.Models.User> ConvertDataItemList(IEnumerable<Data.User> data)
        {
            List<StopWastingMyTime.Models.User> result = new List<StopWastingMyTime.Models.User>();
            foreach (Data.User dataItem in data)
                result.Add(new StopWastingMyTime.Models.User(dataItem));
            return result;
        }

        public static List<StopWastingMyTime.Models.User> SelectAll()
        {
            return ConvertDataItemList(Data.User.Select());
        }

		#endregion
	}
}

