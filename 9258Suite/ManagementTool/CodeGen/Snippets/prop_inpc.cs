
using System;

namespace Snippets
{
    
    /// <summary>
    /// Code snippet for a property which raises INotifyPropertyChanged
    /// </summary>
    [AttributeUsage(AttributeTargets.Class , AllowMultiple = true)]
    public class SnippetPropertyINPC  : Attribute
    {
    
        /// <summary>
        /// Property Type
        /// </summary>
        public string type = "string";
  
        /// <summary>
        /// Property Name
        /// </summary>
        public string property = "MyProperty";
  
        /// <summary>
        /// Backing Field
        /// </summary>
        public string field = "_myproperty";
  
        /// <summary>
        /// Field default value
        /// </summary>
        public string defaultValue = "null";
  
        /// <summary>
        /// private or protected or public
        /// </summary>
        public string fieldAccessScope = "private";
  
        /// <summary>
        /// virtual or absctract
        /// </summary>
        public string propertyVirtualOrAbstract = "";
  
        /// <summary>
        /// sample: [Browsable(true), ReadOnly(true), GlobalizedProperty("Name", "NameDesc")]
        /// </summary>
        public string attribute = "[Browsable(false)]";
  
        /// <summary>
        /// 
        /// </summary>
        public string History = "false";
  
    /// <summary>
    /// Gets the code snippet
    /// </summary>
    public string GetSnippet()
    {
    return @"

		/// <summary>
		/// Field which backs the $property$ property
		/// </summary>
		$fieldAccessScope$ HistoryableProperty<$type$> $field$ = new HistoryableProperty<$type$>($defaultValue$);

		/// <summary>
		/// Gets / sets the $property$ value
		/// </summary>
		$attribute$
		public $propertyVirtualOrAbstract$ $type$ $property$
		{
			get { return $field$.GetValue(); }
			set { ChangeAndNotifyHistory<$type$>($field$, value, () => $property$ ); }
		}
$end$";
    }
  
    }
  
}
  
