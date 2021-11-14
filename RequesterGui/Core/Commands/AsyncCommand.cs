using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RequesterGui.Core.Commands
{
	/// <summary>
	/// 
	/// </summary>
	public interface IAsyncCommand : ICommand
	{
		Task ExecuteAsync();

		bool CanExecute();
	}

	public class AsyncCommand : IAsyncCommand
	{
		public event EventHandler CanExecuteChanged;

		private bool IsExecuting;
		private readonly Func<Task> ExecuteFunc;
		private readonly Func<bool> CanExecuteFunc;
		private readonly IErrorHandler ErrorHandler;

		public AsyncCommand(Func<Task> executeFunc,
			Func<bool> canExecuteFunc = null,
			IErrorHandler errorHandler = null)
		{
			ExecuteFunc = executeFunc;
			CanExecuteFunc = canExecuteFunc;
			ErrorHandler = errorHandler;
		}

		public bool CanExecute()
		{
			return !IsExecuting && (CanExecuteFunc?.Invoke() ?? true);
		}

		public async Task ExecuteAsync()
		{
			if (CanExecute())
			{
				try
				{
					IsExecuting = true;
					await ExecuteFunc();
				}
				finally
				{
					IsExecuting = false;
				}
			}

			RaiseCanExecuteChanged();
		}

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		bool ICommand.CanExecute(object parameter)
		{
			return CanExecute();
		}

		void ICommand.Execute(object parameter)
		{
			ExecuteAsync().FireAndForgetSafeAsync(ErrorHandler);
		}
	}
}
