﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClient.Vos
{
	public class HttpResponse
	{
		public string Content { get; set; }

		public string Status { get; set; }

		public long ResponseTimeMs { get; set; }
	}
}
