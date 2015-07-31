/// <copyright>
/// Copyright ©  2012 Unisys Corporation. All rights reserved. UNISYS CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoYoStudio.Logging
{
	/// <summary>
	/// This class contains the message for the log and also the root exception.
	/// </summary>
	public class LogMessage
	{
		/// <summary>
		/// Construct the log message with the message data and root exception.
		/// </summary>
		/// <param name="message">The message data retrieved from resource file.</param>
		/// <param name="rootException">The root exception to be logged.</param>
		public LogMessage(string message, Exception rootException)
		{
			this.Message = message;
			this.RootException = rootException;
		}

		/// <summary>
		/// The message data of the log message.
		/// </summary>
		public string Message
		{
			get;
			set;
		}

		/// <summary>
		/// The exception which need to be included in the log message.
		/// </summary>
		public Exception RootException
		{
			get;
			set;
		}

		/// <summary>
		/// Override of Object.ToString().
		/// </summary>
		/// <returns>The string representation of the logging message.</returns>
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			if (!string.IsNullOrEmpty(Message))
			{
				builder.Append(LoggingConstants.MessagePrefix).Append(Message).Append(Environment.NewLine);
			}

			if (RootException != null)
			{
				builder.Append(LoggingConstants.ExceptionPrefix).Append(RootException).Append(Environment.NewLine);
			}

			return builder.ToString();
		}
	}
}