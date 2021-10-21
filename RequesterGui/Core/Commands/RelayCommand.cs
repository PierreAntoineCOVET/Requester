using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RequesterGui.Core.Commands
{
	class RelayCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		private readonly Action Command;
		private readonly Func<bool> CanExecute;

		public RelayCommand(Action command, Func<bool> canExecute = null)
		{
			Command = command;
			CanExecute = canExecute;
		}

		bool ICommand.CanExecute(object parameter)
		{
			return CanExecute?.Invoke() ?? true;
		}

		void ICommand.Execute(object parameter)
		{
			Command();
		}
	}
}
