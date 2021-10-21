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
	/// <typeparam name="T"></typeparam>
	interface IAsyncCommand<T> : ICommand
	{
		Task ExecuteAsync(T parameter);

		bool CanExecute(T parameter);
	}

	class AsyncCommand<T> : IAsyncCommand<T>
	{
		public event EventHandler CanExecuteChanged;

		private bool IsExecuting;
		private readonly Func<T, Task> ExecuteFunc;
		private readonly Func<T, bool> CanExecuteFunc;
		private readonly IErrorHandler ErrorHandler;

		public AsyncCommand(Func<T, Task> executeFunc,
			Func<T, bool> canExecuteFunc = null,
			IErrorHandler errorHandler = null)
		{
			ExecuteFunc = executeFunc;
			CanExecuteFunc = canExecuteFunc;
			ErrorHandler = errorHandler;
		}

		public bool CanExecute(T parameter)
		{
			return IsExecuting && (CanExecuteFunc?.Invoke((T)parameter) ?? true);
		}

		public async Task ExecuteAsync(T parameter)
		{
			if (CanExecute(parameter))
			{
				try
				{
					IsExecuting = true;
					await ExecuteFunc(parameter);
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
			return CanExecute((T)parameter);
		}

		void ICommand.Execute(object parameter)
		{
			ExecuteAsync((T)parameter).FireAndForgetSafeAsync(ErrorHandler);
		}
	}
}
