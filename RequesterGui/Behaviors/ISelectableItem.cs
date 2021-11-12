using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterGui.Behaviors
{
	public interface ISelectableItem
	{
		public bool IsSelected { get; set; }
	}
}
