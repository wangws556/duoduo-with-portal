/// <copyright>
/// Copyright ©  2012 Unisys Corporation. All rights reserved. UNISYS CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net.Core;

namespace YoYoStudio.Logging
{
	/// <summary>
	/// This class provides the function of Logging.
	/// </summary>
	public class Logger
	{
		private ILogger logger;

		/// <summary>
		/// Initializes an new instance of <c>Logger</c> with wrapped 
		/// <c>log4net.Cor.ILogger</c> and resouce name.
		/// </summary>
		/// <param name="logger">The wrapped log4net logger.</param>
		internal Logger(ILogger logger)
		{
			this.logger = logger;
		}

		/// <summary>
		/// Log a message with specified level and message key.
		/// </summary>
		/// <param name="level">Logging level.</param>
		/// <param name="message">The message to be logged.</param>
		internal void Log(LogLevel level, string message)
		{
			Log(level, message, null);
		}

		/// <summary>
		/// Log a message with specified level, message key and format parameter.
		/// </summary>
		/// <param name="level">Logging level.</param>
		/// <param name="message">The message to be logged.</param>
		/// <param name="parameters">Parameters which are used to format the logging message.</param>
		internal void Log(LogLevel level, string message, object[] parameters)
		{
			Log(level, message, parameters, null);
		}

		/// <summary>
		/// Log a message with specified level, message key, format parameter,
		/// and exception which causes the error.
		/// </summary>
		/// <param name="level">Logging level.</param>
		/// <param name="message">The message to be logged.</param>
		/// <param name="parameters">Parameters which are used to format the logging message.</param>
		/// <param name="rootException">Exception which causes this log.</param>
		internal void Log(LogLevel level, string message, object[] parameters, Exception rootException)
		{
			DoLogging(level, message, parameters, rootException);
		}

		/// <summary>
		/// Logs the message with specified parameters.
		/// </summary>
		/// <param name="level">Logging level.</param>
		/// <param name="message">The message to be logged.</param>
		/// <param name="parameters">Parameters which are used to format logging message.</param>
		/// <param name="rootException">Exception which causes this log.</param>
		private void DoLogging(LogLevel level, string message, object[] parameters, Exception rootException)
		{
			if (logger.IsEnabledFor(level.WrappedLevel))
			{
				if (null != parameters && !string.IsNullOrEmpty(message))
				{
					message = string.Format(message, parameters);
				}
				LogMessage loggerMessage = new LogMessage(message, rootException);
				logger.Log(this.GetType(), level.WrappedLevel, loggerMessage, rootException);
			}
		}
	}
}
