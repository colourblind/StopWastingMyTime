using System;
using System.Linq;

namespace StopWastingMyTime.Models
{
	public class User : Base.User
	{
		#region Constructors
		
		public User() : base()
		{
			
		}
		
		public User(string userId) : base(userId)
		{
			
		}
		
		internal User(Data.User dataObject) : base(dataObject)
		{
			
		}
		
		#endregion

        #region Properties

        public override string Password
        {
            set
            {
                // Ignore blank passwords (an UpdateModel hack disguised as a security feature!)
                if (!String.IsNullOrEmpty(value))
                    base.Password = Colourblind.Core.Security.GenerateHash(value);
            }
        }

        #endregion

        #region Methods

        public override void Delete()
        {
            ClearPermissions();
            base.Delete();
        }

        public static User Validate(string username, string password)
        {
            User user = new User(username);
            if (!user.IsNew && user.Active && Colourblind.Core.Security.CheckHash(password, user.Password))
                return user;
            else
                return null;
        }

        public bool HasPermission(string permissionId)
        {
            return Permissions.Where(o => o.PermissionId == permissionId).Count() > 0;
        }

        public void ClearPermissions()
        {
            foreach (Permission p in Permissions)
                Data.UserPermissionJoin.Delete(UserId, p.PermissionId);

            Refresh();
        }

        public void AddPermission(string permissionId)
        {
            Data.UserPermissionJoin j = new Data.UserPermissionJoin();
            j.UserId = UserId;
            j.PermissionId = permissionId;
            j.Insert();

            Refresh();
        }

        #endregion
    }
}

