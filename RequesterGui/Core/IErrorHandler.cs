﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterGui.Core
{
	public interface IErrorHandler
	{
		void HandleError(Exception ex);
	}
}
