using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RequesterGui.Components
{
	/// <summary>
	/// Interaction logic for HostControl.xaml
	/// </summary>
	public partial class HostControl : UserControl
	{
		public HostControl()
		{
			InitializeComponent();
		}

		public bool IsUsingWindowsAuthentication { get; set; }
		public static readonly DependencyProperty IsUsingWindowsAuthenticationProperty =
			DependencyProperty.Register("IsUsingWindowsAuthentication", typeof(bool), typeof(HostControl));

		public bool IsUsingJwtAuthentication { get; set; }
		public static readonly DependencyProperty IsUsingJwtAuthenticationProperty =
			DependencyProperty.Register("IsUsingJwtAuthentication", typeof(bool), typeof(HostControl));

		public string HostBaseUri { get; set; }
		public static readonly DependencyProperty HostBaseUriProperty =
			DependencyProperty.Register("HostBaseUri", typeof(string), typeof(HostControl));

		public string HostNameAlias { get; set; }
		public static readonly DependencyProperty HostNameAliasProperty =
			DependencyProperty.Register("HostNameAlias", typeof(string), typeof(HostControl));
	}
}
