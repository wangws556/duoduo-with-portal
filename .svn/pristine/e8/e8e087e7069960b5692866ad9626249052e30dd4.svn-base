using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using YoYoStudio.Resource;

namespace YoYoStudio.ManagementPortal.Models
{

	public class RegisterExternalLoginModel 
	{
		[Required]
		[Display(Name = ConstStrings.UserName)]
		public string UserName { get; set; }

		public string ExternalLoginData { get; set; }
	}

	public class LocalPasswordModel
	{
		[Required]
		[DataType(DataType.Password)]
        [Display(Name = ConstStrings.Password)]
		public string OldPassword { get; set; }

		[Required]
		[StringLength(100, ErrorMessageResourceName="MinimalLength",ErrorMessageResourceType=typeof(Text), MinimumLength = 6)]
		[DataType(DataType.Password)]
        [Display(Name = ConstStrings.NewPassword)]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
        [Display(Name = ConstStrings.ConfirmNewPassword)]
		[Compare("NewPassword", ErrorMessageResourceName = "PasswordMismatch", ErrorMessageResourceType = typeof(Text))]
		public string ConfirmPassword { get; set; }
	}

	public class LoginModel 
	{
		[Required(ErrorMessageResourceName="UserNameIsRequired",ErrorMessageResourceType=typeof(Text))]
        [Display(Name = ConstStrings.UserName)]
		public string UserName { get; set; }

		[Required(ErrorMessageResourceName = "PasswordIsRequired", ErrorMessageResourceType = typeof(Text))]
		[DataType(DataType.Password)]
        [Display(Name = ConstStrings.Password)]
		public string Password { get; set; }

        [Display(Name = ConstStrings.RememberMe)]
		public bool RememberMe { get; set; }

		public string LoginLabel { get; set; }

		public LoginModel()
		{
			LoginLabel = Text.Login;
		}
	}
		
}
