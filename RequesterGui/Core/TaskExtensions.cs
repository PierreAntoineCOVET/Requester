using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterGui.Core
{
	/// <summary>
	/// Tasks extensions class.
	/// </summary>
	static class TaskExtensions
	{
		/// <summary>
		/// Fire and forget with error handling.
		/// </summary>
		/// <param name="task">Task to execute.</param>
		/// <param name="errorHandler">Error handler, facultative.</param>
		public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler errorHandler = null)
		{
			try
			{
				await task;
			}
			catch(Exception e)
			{
				errorHandler?.HandleError(e);
			}
		}
	}
}
