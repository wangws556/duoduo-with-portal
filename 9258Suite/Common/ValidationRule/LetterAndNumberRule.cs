using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using YoYoStudio.Resource;

namespace YoYoStudio.Common
{
    public class LetterAndNumberRule:ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string account = value as string;

            char[] charArray = account.ToCharArray();
            foreach (char c in charArray)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    return new ValidationResult(false, Text.AccountNameDescription);
                }
            }
            return new ValidationResult(true, null);
        }
    }
}
