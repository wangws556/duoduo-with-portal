/// <copyright>
/// Copyright ©  2012 Unisys Corporation. All rights reserved. UNISYS CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;

namespace YoYoStudio.Logging
{
	/// <summary>
	/// A wrapped class for Log4Net's <c>Level</c>.
	/// This class defines custom levels. it predefines seven filter levels,
	/// the relationships are Off > Fatal > Error > Warn > Info > Debug > All.
	/// </summary>
	public class LogLevel
	{
		/// <summary>
		/// Constructs a instance of <c>Level</c> object.
		/// The private modifier prevents client to create new instance.
		/// </summary>
		private LogLevel(Level level)
		{
			this.WrappedLevel = level;
		}

		/// <summary>
		/// Gets the wrapped <c>log4net.Core.Level</c> object.
		/// </summary>
		public Level WrappedLevel
		{
			get;
			private set;
		}

		/// <summary>
		/// The lowest level. Mapping to the Log4Net's Level.All.
		/// </summary>
		public static readonly LogLevel All = new LogLevel(Level.All);

		/// <summary>
		/// Higher than level All. Mapping to the Log4Net's Level.Debug.
		/// </summary>
		public static readonly LogLevel Debug = new LogLevel(Level.Debug);

		/// <summary>
		///  Higher than level Debug. Mapping to the Log4Net's Level.Info.
		/// </summary>
		public static readonly LogLevel Info = new LogLevel(Level.Info);

		/// <summary>
		/// Higher than level Info. Mapping to the Log4Net's Level.Warn.
		/// </summary>
		public static readonly LogLevel Warn = new LogLevel(Level.Warn);

		/// <summary>
		///  Higher than level Warn. Mapping to the Log4Net's Level.Error.
		/// </summary>
		public static readonly LogLevel Error = new LogLevel(Level.Error);

		/// <summary>
		///  Higher than level Error. Mapping to the Log4Net's Level.Fatal.
		/// </summary>
		public static readonly LogLevel Fatal = new LogLevel(Level.Fatal);

		/// <summary>
		/// The highest level. Mapping to the Log4Net's Level.Off.
		/// </summary>
		public static readonly LogLevel Off = new LogLevel(Level.Off);

		/// <summary>
		/// Overrides the object's ToString method to display the level information.
		/// </summary>
		/// <returns>The information of the Level.</returns>
		public override string ToString()
		{
			string levelName = "Level Name: " + WrappedLevel.Name;
			string levelValue = "Level Value: " + WrappedLevel.Value.ToString();
			string result = levelName + "; " + levelValue;

			return result;
		}
	}
}