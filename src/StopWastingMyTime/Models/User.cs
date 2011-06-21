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

        #region Methods

        public static User Validate(string username, string password)
        {
            User user = new User(username);
            if (Colourblind.Core.Security.CheckHash(password, user.Password))
                return user;
            else
                return null;
        }

        #endregion
    }
}

