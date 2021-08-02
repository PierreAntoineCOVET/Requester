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
			DataContext = this;
			InitializeComponent();
			AddHostEndpoint();
		}

		public static readonly DependencyProperty HostAliasProperty =
			DependencyProperty.Register(
				"HostAlias",
				typeof(string),
				typeof(HostControl),
				new PropertyMetadata(string.Empty));

		public string HostAlias
		{
			get => (string)GetValue(HostAliasProperty);
			set => SetValue(HostAliasProperty, value);
		}

		public static readonly DependencyProperty HostUrlProperty =
			DependencyProperty.Register(
				"HostUrl",
				typeof(string),
				typeof(HostControl),
				new PropertyMetadata(string.Empty));

		public string HostUrl
		{
			get => (string)GetValue(HostUrlProperty);
			set => SetValue(HostUrlProperty, value);
		}

		public static readonly DependencyProperty HostWindowsAuthProperty =
			DependencyProperty.Register(
				"HostWindowsAuth",
				typeof(bool),
				typeof(HostControl),
				new PropertyMetadata(false));

		public bool HostWindowsAuth
		{
			get => (bool)GetValue(HostWindowsAuthProperty);
			set => SetValue(HostWindowsAuthProperty, value);
		}

		public static readonly DependencyProperty HostJwtAuthProperty =
			DependencyProperty.Register(
				"HostJwtAuth",
				typeof(bool),
				typeof(HostControl),
				new PropertyMetadata(false));

		public bool HostJwtAuth
		{
			get => (bool)GetValue(HostJwtAuthProperty);
			set => SetValue(HostJwtAuthProperty, value);
		}

		private void AddEndpoint_Click(object sender, RoutedEventArgs e)
		{
			AddHostEndpoint();
		}

		private void AddHostEndpoint()
		{
			_ = HostEndpointControl.Items.Add(new TabItem
			{
				Header = "test",
				Name = "test",
				Content = new EndpointControl(),
				IsSelected = true
			});
		}
	}
}
