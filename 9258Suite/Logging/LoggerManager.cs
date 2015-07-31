/// <copyright>
/// Copyright ©  2012 Unisys Corporation. All rights reserved. UNISYS CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;
using log4net.Core;
using YoYoStudio.Resource;
using Log4netLoggerManager = log4net.Core.LoggerManager;

namespace YoYoStudio.Logging
{
	/// <summary>
	/// This class controls the creation of logger.
	/// </summary>
	public sealed class LoggerManager
	{
		/// <summary>
		/// Private constructor to prevent instance to be created.
		/// </summary>
		private LoggerManager()
		{
		}

		/// <summary>
		/// Load the config file. This method should be called before using LoggerManager.
		/// Only need to be call once.
		/// </summary>
		/// <param name="filePath">The path of the config file.</param>
		public static void Initialize(string filePath)
		{
			FileInfo fileInfo = new FileInfo(Environment.CurrentDirectory + filePath);
			XmlConfigurator.ConfigureAndWatch(Log4netLoggerManager.GetRepository(Assembly.GetExecutingAssembly()), fileInfo);
		}

		/// <summary>
		/// Creates a logger with the given logger name.
		/// </summary>
		/// <param name="loggerName">The name of Logger which will be created.</param>
		/// <returns>An instance of <c>Logger</c>.</returns>
		public static Logger GetLogger(string loggerName)
		{
			Logger logger = null;
			if (!string.IsNullOrEmpty(loggerName))
			{
				logger = new Logger(GetLog4netLogger(loggerName));
			}

			return logger;
		}

		/// <summary>
		/// Creates a logger with the given logger name.
		/// </summary>
		/// <param name="loggerName">The name of Logger which will be created.</param>
		/// <returns>An instance of <c>ILogger</c>.</returns>
		private static ILogger GetLog4netLogger(string loggerName)
		{
			return Log4netLoggerManager.GetLogger(Assembly.GetExecutingAssembly(), loggerName);
		}

		/// <summary>
		/// Log the message. The default type is FileLogger and the default level is INFO.
		/// </summary>
		/// <param name="message">The message to be logged.</param>
		public static void Log(string message)
		{
			Log(LoggerType.FileLogger, LogLevel.Info, message, null);
		}

		/// <summary>
		/// Log the message. The default level is information.
		/// </summary>
		/// <param name="type">The type of the logger.</param>
		/// <param name="messageKey">The message to be logged.</param>
		public static void Log(LoggerType type, string message)
		{
			Log(type, LogLevel.Info, message, null);
		}

		/// <summary>
		/// Log the message.
		/// </summary>
		/// <param name="type">The type of the logger.</param>
		/// <param name="level">The level of the log.</param>
		/// <param name="message">The message to be logged.</param>
		public static void Log(LoggerType type, LogLevel level, string message)
		{
			Log(type, level, message, null);
		}

		/// <summary>
		/// Log the message with parameters for the message retrieved from the resource file.
		/// </summary>
		/// <param name="type">The type of the logger.</param>
		/// <param name="level">The level of the log.</param>
		/// <param name="message">The message to be logged.</param>
		/// <param name="parameters">The parameters used to format the message.</param>
		public static void Log(LoggerType type, LogLevel level, string message, object[] parameters)
		{
			Log(type, level, message, parameters, null);
		}

		/// <summary>
		/// Log the message with exception.
		/// </summary>
		/// <param name="type">The type of the logger.</param>
		/// <param name="level">The level of the log.</param>
		/// <param name="message">The message to be logged.</param>
		/// <param name="parameters">The parameters used to format the message.</param>
		/// <param name="rootException">The exception to be logged.</param>
		public static void Log(LoggerType type, LogLevel level, string message, object[] parameters, Exception rootException)
		{
			Logger logger = LoggerPool.GetLogger(type);
			if (logger != null)
			{
				logger.Log(level, message, parameters, rootException);
			}
		}

		#region Internal class -- LoggerPool.

		/// <summary>
		/// Internal class which is actually the pool of the loggers.
		/// </summary>
		private static class LoggerPool
		{
			/// <summary>
			/// It is a dictionary to contain all the loggers needed in databus.
			/// </summary>
			private static Dictionary<LoggerType, Logger> loggerPool = new Dictionary<LoggerType, Logger>();

			/// <summary>
			/// Get the logger based on the type.
			/// </summary>
			/// <param name="type">Logger type.</param>
			/// <returns>The logger.</returns>
			internal static Logger GetLogger(LoggerType type)
			{
				Logger logger = null;
				if (!loggerPool.TryGetValue(type, out logger))
				{
					logger = LoggerManager.GetLogger(type.ToString());
					if (!loggerPool.ContainsKey(type))
					{
						loggerPool.Add(type, logger);
					}
				}

				return logger;
			}
		}

		#endregion
	}

	/// <summary>
	/// The logger type in the logging system.
	/// </summary>
	public enum LoggerType : int
	{
		FileLogger = 1,
		EventLogLogger =2,
		ImportLogger =3,
		ExportLogger = 4,
		CodeGenerationLogger = 5
	}
}
