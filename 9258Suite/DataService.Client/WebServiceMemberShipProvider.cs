using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using WebMatrix.WebData;
using YoYoStudio.Common;
using YoYoStudio.Common.Web;
using YoYoStudio.DataService.Client.Generated;
using YoYoStudio.Model;
using YoYoStudio.Model.Core;

namespace YoYoStudio.DataService.Client
{
    public class WebServiceMemberShipProvider : ExtendedMembershipProvider
    {
		private DSClient client = new DSClient(BuiltIns.BackendApplication.Id);
				
		private bool GetToken(out int userId, out string token)
		{
			userId = -1;
			token = string.Empty;
			IPrincipal principal = HttpContext.Current.User;
			if (principal != null)
			{
				TokenIdentity identity = principal.Identity as TokenIdentity;
				if (identity != null && identity.IsAuthenticated)
				{
					token = identity.Token;
					if (int.TryParse(identity.Name, out userId))
					{
						return true;
					}
				}
			}
			return false;
		}

        public override bool ConfirmAccount(string accountConfirmationToken)
		{
			throw new NotImplementedException();
		}

		public override bool ConfirmAccount(string userName, string accountConfirmationToken)
		{
			throw new NotImplementedException();
		}

		public override string CreateAccount(string userName, string password, bool requireConfirmationToken)
		{
			throw new NotImplementedException();
		}

		public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteAccount(string userName)
		{
			throw new NotImplementedException();
		}

		public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
		{
			throw new NotImplementedException();
		}

		public override ICollection<OAuthAccountData> GetAccountsForUser(string userName)
		{
			throw new NotImplementedException();
		}

		public override DateTime GetCreateDate(string userName)
		{
			throw new NotImplementedException();
		}

		public override DateTime GetLastPasswordFailureDate(string userName)
		{
			throw new NotImplementedException();
		}

		public override DateTime GetPasswordChangedDate(string userName)
		{
			throw new NotImplementedException();
		}

		public override int GetPasswordFailuresSinceLastSuccess(string userName)
		{
			throw new NotImplementedException();
		}

		public override int GetUserIdFromPasswordResetToken(string token)
		{
			throw new NotImplementedException();
		}

		public override bool IsConfirmed(string userName)
		{
			throw new NotImplementedException();
		}

		public override bool ResetPasswordWithToken(string token, string newPassword)
		{
			throw new NotImplementedException();
		}

		public override string ApplicationName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
            int userId = -1;
            if (int.TryParse(username, out userId) && !string.IsNullOrEmpty(oldPassword) && !string.IsNullOrEmpty(newPassword))
            {
                int uid = -1;
                string token = "";
                if (GetToken(out uid, out token))
                {
                    User user = client.GetUser(uid, token, userId);
                    if (Utility.GetMD5String(oldPassword) == user.Password)
                    {
                        user.Password = Utility.GetMD5String(newPassword);
                        client.UpdateUser(uid,token,user);
                        return true;
                    }
                }
            }
            return false;
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new NotImplementedException();
		}

		public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new NotImplementedException();
		}

		public override bool EnablePasswordReset
		{
			get { throw new NotImplementedException(); }
		}

		public override bool EnablePasswordRetrieval
		{
			get { throw new NotImplementedException(); }
		}

		public override System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override int GetNumberOfUsersOnline()
		{
			throw new NotImplementedException();
		}

		public override string GetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
		{
            int uid = -1;
            if (int.TryParse(username, out uid))
            {
                int userId = -1;
                string token="";
                if (GetToken(out userId, out token))
                {
                    return new TokenMemberShipUser(username, null, string.Empty, string.Empty, string.Empty, true, false, DateTime.Now,
                        DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, token);
                }
            }
            return null;
		}

		public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			return GetUser(providerUserKey.ToString(), userIsOnline);
		}

		public override string GetUserNameByEmail(string email)
		{
			throw new NotImplementedException();
		}

		public override int MaxInvalidPasswordAttempts
		{
			get { return 5; }
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get { return 1; }
		}

		public override int MinRequiredPasswordLength
		{
			get { return 1; }
		}

		public override int PasswordAttemptWindow
		{
			get { return 5; }
		}

		public override System.Web.Security.MembershipPasswordFormat PasswordFormat
		{
			get { return System.Web.Security.MembershipPasswordFormat.Encrypted; }
		}

		public override string PasswordStrengthRegularExpression
		{
			get { throw new NotImplementedException(); }
		}

		public override bool RequiresQuestionAndAnswer
		{
			get { return false; }
		}

		public override bool RequiresUniqueEmail
		{
			get { return true; }
		}

		public override string ResetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override bool UnlockUser(string userName)
		{
			throw new NotImplementedException();
		}

		public override void UpdateUser(System.Web.Security.MembershipUser user)
		{
			throw new NotImplementedException();
		}

		public override bool ValidateUser(string username, string password)
		{			
			int userId = -1;
			if (int.TryParse(username, out userId))
			{
				string token = (client.Login(userId, password));
				HttpContext.Current.Session[username] = token;
				return true;
			}
			return false;
		}
	}
}
