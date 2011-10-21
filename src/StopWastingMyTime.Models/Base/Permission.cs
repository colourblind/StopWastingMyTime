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
    [XmlType("Base.Permission")]
    public class Permission : IComparable<Base.Permission>, IComparable
    {
        #region Fields

        private string _permissionId = String.Empty;
        private string _description = String.Empty;

        

        private List<StopWastingMyTime.Models.User> _users = null; // m2m

        private bool _isNew = true;

        #endregion

        #region Properties

        public string PermissionId
        {
            get { return _permissionId; }
            set { _permissionId = value; }
        }
		
        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }
		

        // Many to many
        public virtual IList<StopWastingMyTime.Models.User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new List<StopWastingMyTime.Models.User>();
                    foreach (Data.UserPermissionJoin d in Data.UserPermissionJoin.SelectByPermissionId(PermissionId))
                        _users.Add(new StopWastingMyTime.Models.User(d.UserId));
                }
                return _users;
            }
        }
        public bool IsNew
        {
            get { return _isNew; }
        }

        #endregion

        #region Constructors

        public Permission()
        {
            _permissionId = String.Empty;
        }
		
        public Permission(string permissionId)
        {
            Load(permissionId);
        }
		
        internal Permission(Data.Permission dataObject)
        {
            Load(dataObject);
        }

        #endregion

        #region Methods

        internal void Load(string permissionId)
        {
            Load(Data.Permission.SelectById(permissionId));
        }

        internal void Load(Data.Permission dataObject)
        {
            if (dataObject == null)
            {
                _isNew = true;
            }
            else
            {
                _permissionId = dataObject.PermissionId;
                _description = dataObject.Description;

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
            Data.Permission dataObject = new Data.Permission();

            dataObject.PermissionId = _permissionId;
            dataObject.Description = _description;

            if (IsNew)
                dataObject.Insert();
            else
                dataObject.Update();
			
            _permissionId = dataObject.PermissionId;
            if (refresh)
                Refresh();

            _isNew = false;
        }
        
        internal virtual void Restore()
        {
            _isNew = true;
            Save(false);
            

            // TODO: fix m2m relationships in template
            
            Refresh();
        }

        public virtual void Delete()
        {
            Data.Permission.Delete(PermissionId);
            Refresh();
        }
		
        public virtual void Refresh()
        {

            _users = null;
        }
        
        public override bool Equals(object obj)
        {
            return this.CompareTo(obj) == 0;
        }
		
        public virtual int CompareTo(Permission obj)
        {
            return PermissionId.CompareTo(obj.PermissionId) * 1;
        }
		
        public virtual int CompareTo(object obj)
        {
            if (obj is Permission)
                return CompareTo((Permission)obj);
            else
                throw new InvalidCastException("Unable to compare Permission and " + obj.GetType().Name);
        }

        #endregion

        #region Statics
        
        protected static List<StopWastingMyTime.Models.Permission> ConvertDataItemList(IEnumerable<Data.Permission> data)
        {
            List<StopWastingMyTime.Models.Permission> result = new List<StopWastingMyTime.Models.Permission>();
            foreach (Data.Permission dataItem in data)
                result.Add(new StopWastingMyTime.Models.Permission(dataItem));
            return result;
        }

        public static List<StopWastingMyTime.Models.Permission> SelectAll()
        {
            return ConvertDataItemList(Data.Permission.Select());
        }

		#endregion
	}
}

