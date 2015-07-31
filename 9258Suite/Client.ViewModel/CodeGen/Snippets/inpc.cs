
using System;

namespace Snippets
{
    
    /// <summary>
    /// Implementation of INotifyPropertyChanged
    /// </summary>
    [AttributeUsage(AttributeTargets.Class , AllowMultiple = true)]
    public class SnippetINotifyPropertyChanged  : Attribute
    {
    
    /// <summary>
    /// Gets the code snippet
    /// </summary>
    public string GetSnippet()
    {
    return @"
    #region INotifyPropertyChanged Members
    #endregion
    $end$";
    }
  
    }
  
}
  
