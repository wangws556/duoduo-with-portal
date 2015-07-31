using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using YoYoStudio.Resource;

namespace YoYoStudio.Common
{
    public class PasswordRule:ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string password = value as string;
            if (password.Length < 6 || password.Length > 10)
            {
                return new ValidationResult(false, Text.PasswordLength);
            }
            bool hasCapital = false;
            char[] charArray = password.ToCharArray();
            foreach (char c in charArray)
            {
                if (Char.IsUpper(c))
                {
                    hasCapital = true;
                    break;
                }

            }
            if (!hasCapital)
                return new ValidationResult(false, Text.PasswordRequireCapital);
            return new ValidationResult(true, null);
        }
    }
}
